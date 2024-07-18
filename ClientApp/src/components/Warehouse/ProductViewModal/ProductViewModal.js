import React, { Component } from 'react';
import { connect } from 'react-redux';
import { Modal, ModalContent, ModalHeader, Tab } from 'semantic-ui-react';
import { SetProductViewModal, SetInitialState } from './ProdctViewModalAction';
import ProductViewModalProductInfoTab from './ProductViewModalProductInfoTab';
import ProductViewModalPriceHistoryTab from './ProductViewModalPriceHistoryTab';
import ProductViewModalQuantityHistoryTab from './ProductViewModalQuantityHistoryTab';
import { withRouter } from 'react-router-dom';

const panes = [
    {menuItem: 'Product info', render: () => <ProductViewModalProductInfoTab/>},
    {menuItem: 'Price history', render: () => <ProductViewModalPriceHistoryTab/>},
    {menuItem: 'Quantity history', render: () => <ProductViewModalQuantityHistoryTab/>}
];

class ProductViewModal extends Component {
    onModalClose = () => {
        this.props.SetInitialState();
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

export default withRouter(
    connect( MapStateToProps, {SetProductViewModal, SetInitialState})
    (ProductViewModal));