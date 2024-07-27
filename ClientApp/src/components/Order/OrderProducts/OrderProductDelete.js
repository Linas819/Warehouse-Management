import React, { Component } from 'react';
import { connect } from 'react-redux';
import { Button } from 'semantic-ui-react';
import { withRouter } from 'react-router-dom';
import { DeleteOrderProduct } from './OrderProductsAction';

class OrderProductDelete extends Component {
    onClickHandler = () => {
        const orderId = this.props.data.orderId;
        const productId = this.props.data.productId;
        this.props.DeleteOrderProduct(orderId, productId);
    }
    render(){
        return(
            <Button color='red' loading={this.props.main.isButtonLoading} onClick={this.onClickHandler}>Delete</Button>
        );
    }
}

function MapStateToProps(state) {
    return {
        main: state.main
    };
}

export default withRouter(
    connect( MapStateToProps, {DeleteOrderProduct})
(OrderProductDelete));