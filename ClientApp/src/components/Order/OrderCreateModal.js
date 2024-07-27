import React, { Component } from 'react';
import { connect } from 'react-redux';
import { withRouter } from 'react-router-dom';
import { Button, Dropdown, Input, Modal, ModalActions, ModalContent, ModalHeader } from 'semantic-ui-react';
import { SetOrderCreateModal, PostNewOrder } from './OrderAction';

class OrderCreateModal extends Component {
    constructor(props)
    {
        super(props);
        this.state = {
            orderId: "",
            addressFrom: "",
            addressTo: "",
        }
    }
    onModalClose = () => {
        this.props.SetOrderCreateModal(false);
    }
    onChangeHandler = (event, data) => {
        this.setState({
            ...this.state,
            [data.name]: data.value
        });
    }
    onClickHandler = () => {
        this.props.PostNewOrder(this.state);
    }
    render(){
        return(
            <Modal open={this.props.orders.orderCreateModalOpen} onClose={this.onModalClose}>
                <ModalHeader>New order</ModalHeader>
                <ModalContent style={{marginTop: '20px'}}>
                    <Input placeholder='Order ID' name='orderId' onChange={this.onChangeHandler}/><br/>
                    <Dropdown placeholder='Address from' name='addressFrom' onChange={this.onChangeHandler} 
                        options={this.props.orders.addressOptions} style={{marginTop: '20px'}}/><br/>
                    <Dropdown placeholder='Address to' name='addressTo' onChange={this.onChangeHandler} 
                        options={this.props.orders.addressOptions} style={{marginTop: '20px'}}/><br/>
                </ModalContent>
                <ModalActions>
                    <Button color='green' onClick={this.onClickHandler}>Create order</Button>
                </ModalActions>
            </Modal>
        );
    }
}

function MapStateToProps(state) {
    return {
        orders: state.orders
    };
}

export default withRouter(
    connect( MapStateToProps, {SetOrderCreateModal, PostNewOrder})
    (OrderCreateModal));