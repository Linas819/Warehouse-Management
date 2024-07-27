import React, { Component } from 'react';
import { connect } from 'react-redux';
import { Button } from 'semantic-ui-react';
import { withRouter } from 'react-router-dom';
import { DeleteOrderData } from './OrderAction';
import { SetOrderProductsModal, GetOrderProducts } from './OrderProducts/OrderProductsAction';

class OrdersActionButtons extends Component {
    onClickHandler = (event, data) => {
        const orderId = this.props.data.orderId;
        switch(data.name) {
            case "view":
                this.props.GetOrderProducts(orderId);
                this.props.SetOrderProductsModal(true);
                break;
            case "changeAddress":
                break;
            case "delete":
                this.props.DeleteOrderData(orderId);
                break;
            default:
                break;
        }
    }
    render() {
        return(
            <div>
                <Button color='blue' name='view' onClick={this.onClickHandler}>View</Button>
                <Button color='green' name='changeAddress' onClick={this.onClickHandler}>Change address</Button>
                <Button color='red' name='delete' loading={this.props.main.isButtonLoading} onClick={this.onClickHandler}>Delete</Button>
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
    connect( MapStateToProps, {DeleteOrderData, SetOrderProductsModal, GetOrderProducts})
(OrdersActionButtons));