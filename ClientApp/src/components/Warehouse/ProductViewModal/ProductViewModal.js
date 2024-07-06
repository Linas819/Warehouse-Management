import React, { Component } from 'react';
import { connect } from 'react-redux';
import { Modal, ModalContent, ModalHeader, Tab } from 'semantic-ui-react';
import { SetProductViewModal } from './ProdctViewModalAction';
import ProductViewModalProductInfoTab from './ProductViewModalProductInfoTab';

const panes = [
    {menuItem: 'Product info', render: () => <ProductViewModalProductInfoTab/>},
    {menuItem: 'Price history', render: () => <></>},
    {menuItem: 'Quantity history', render: () => <></>}
];

class ProductViewModal extends Component {
    onModalClose = () => {
        this.props.SetProductViewModal(false);
    }
    render() {
        let productView = this.props.productView;
        return (
            <Modal open={productView.productViewModalOpen} onClose={this.onModalClose}>
                <ModalHeader>{productView.productViewModalHeader}</ModalHeader>
                <ModalContent>
                    <Tab panes={panes} renderActiveOnly={true}/>
                </ModalContent>
            </Modal>
        );
    }
}

function MapStateToProps(state) {
    return {
        productView: state.productViewModal
    };
}

export default connect( MapStateToProps, {SetProductViewModal})(ProductViewModal);