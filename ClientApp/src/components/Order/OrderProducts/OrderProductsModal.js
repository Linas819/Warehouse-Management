import React, { Component } from 'react';
import { connect } from 'react-redux';
import { Modal, ModalContent, ModalHeader } from 'semantic-ui-react';
import { withRouter } from 'react-router-dom';
import { SetOrderProductsModal } from './OrderProductsAction';
import { AgGridReact } from 'ag-grid-react';
import { orderProductsColumnDefs } from '../OrdersUtils';

class OrderProductsModal extends Component {
    onModalClose = () => {
        this.props.SetOrderProductsModal(false);
    }
    render(){
        const windowHeight = window.innerHeight;
        return(
            <Modal open={this.props.orderProducts.orderProductsModalOpen} onClose={this.onModalClose}>
                <ModalHeader>{this.props.orderProducts.orderId}</ModalHeader>
                <ModalContent>
                    <div className='ag-theme-quartz' style={{width: '100%', height: windowHeight/2}}>
                        <AgGridReact
                            rowData={this.props.orderProducts.orderProducts}
                            columnDefs={orderProductsColumnDefs}
                        />
                    </div>
                </ModalContent>
            </Modal>
        );
    }
}

function MapStateToProps(state) {
    return {
        orderProducts: state.orderProducts
    };
}

export default withRouter(
    connect( MapStateToProps, {SetOrderProductsModal})
    (OrderProductsModal));