import React, { Component } from 'react';
import { connect } from 'react-redux';
import { Button } from 'semantic-ui-react';
import { DeleteWarehouseProduct } from './WarehouseAction';
import { SetProductViewModal, SetProductViewModalContentHeader } from './ProductViewModal/ProdctViewModalAction';
import { withRouter } from 'react-router-dom/cjs/react-router-dom';

class WarehouseActionButtons extends Component
{
    onClickHandler = (event, data) => {
        const productId = this.props.data.productId;
        switch(data.name) {
            case "delete":
                this.props.DeleteWarehouseProduct(productId);
                break;
            case "view":
                this.props.SetProductViewModalContentHeader(productId);
                this.props.SetProductViewModal(true);
                break;
            default:
                break;
        }
    }
    render() {
        return (
            <div>
                <Button color='grey' onClick={this.onClickHandler} name = 'view'>VIEW</Button>
                <Button color='red' onClick={this.onClickHandler} name = 'delete'>DELETE</Button>
            </div>
        );
    }
}

function MapStateToProps(state) {
    return {
        main: state.main
    };
}

export default withRouter(
    connect( MapStateToProps, {DeleteWarehouseProduct, SetProductViewModal, SetProductViewModalContentHeader})
(WarehouseActionButtons));