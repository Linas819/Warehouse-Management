import React, { Component } from 'react';
import { connect } from 'react-redux';
import { Divider, Modal, ModalContent, ModalHeader, Header, Input, Dropdown, ModalActions, Button } from 'semantic-ui-react';
import { withRouter } from 'react-router-dom';
import { SetOrderProductsModal, SetNewProductToOrder } from './OrderProductsAction';
import { AgGridReact } from 'ag-grid-react';
import { orderProductsColumnDefs } from '../OrdersUtils';

class OrderProductsModal extends Component {
    constructor(props)
    {
        super(props);
        this.state = {
            productId: "",
            productQuantity: 0
        }
    }
    onModalClose = () => {
        this.props.SetOrderProductsModal(false);
    }
    onChangeHandler = (even, data) => {
        if(data.name === "productsDropdown")
        {
            this.setState({
                ...this.state,
                productId: data.value
            });
        } else if(data.name === "newProductQuantity") {
            this.setState({
                ...this.state,
                productQuantity: parseInt(data.value)
            });
        }
    }
    onClickHandler = () => {
        const orderId = this.props.orderProducts.orderId;
        this.props.SetNewProductToOrder(orderId, this.state.productId, this.state.productQuantity)
    }
    render(){
        const windowHeight = window.innerHeight;
        return(
            <Modal open={this.props.orderProducts.orderProductsModalOpen} onClose={this.onModalClose}>
                <ModalHeader>{this.props.orderProducts.orderId}</ModalHeader>
                <ModalContent>
                    <Header as='h2'>Add product</Header>
                    <Dropdown placeholder='Products' name='productsDropdown' onChange={this.onChangeHandler}
                        options={this.props.orderProducts.dropdownProducts}/>
                    <Input placeholder='Quantity' type='number' min='1' step='1' onChange={this.onChangeHandler} name='newProductQuantity'/>
                    <Divider/>
                    <div className='ag-theme-quartz' style={{width: '100%', height: windowHeight/2}}>
                        <AgGridReact
                            rowData={this.props.orderProducts.orderProducts}
                            columnDefs={orderProductsColumnDefs}
                        />
                    </div>
                </ModalContent>
                <ModalActions>
                    <Button color='green' onClick={this.onClickHandler} loading={this.props.main.isButtonLoading}>Add product</Button>
                </ModalActions>
            </Modal>
        );
    }
}

function MapStateToProps(state) {
    return {
        orderProducts: state.orderProducts,
        main: state.main
    };
}

export default withRouter(
    connect( MapStateToProps, {SetOrderProductsModal, SetNewProductToOrder})
    (OrderProductsModal));