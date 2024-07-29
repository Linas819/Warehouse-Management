import React, { Component } from 'react';
import { connect } from 'react-redux';
import { withRouter } from 'react-router-dom';
import { Button, Dropdown, Modal, ModalActions, ModalContent, ModalHeader } from 'semantic-ui-react';
import { SetErrorModal } from '../MainAction';
import { SetOrderAddressChangeModal, UpdateOrderAddress } from './OrderAction';

class OrderAddressChangeModal extends Component {
    constructor(props)
    {
        super(props);
        this.state = {
            addressFrom: "",
            addressTo: "",
        }
    }
    onModalClose = () => {
        this.props.SetOrderAddressChangeModal(false, "");
    }
    onChangeHandler = (event, data) => {
        this.setState({
            ...this.state,
            [data.name]: data.value
        });
    }
    onClickHandler = () => {
        if(this.state.addressFrom === "")
        {
            this.props.SetErrorModal(true, "Address from required");
            return;
        }  
        if(this.state.addressTo === "")
        {
            this.props.SetErrorModal(true, "Address to required");
            return;
        }
        if(this.state.addressFrom === this.state.addressTo)
        {
            this.props.SetErrorModal(true, "Address to cannot be the same as address from");
            return;
        }
        const orderId = this.props.orders.orderAddressChangeModalOrderId;
        this.props.UpdateOrderAddress(this.state, orderId);
    }
    render(){
        return(
            <Modal size='mini' open={this.props.orders.orderAddressChangeModal} onClose={this.onModalClose}>
                <ModalHeader>{this.props.orders.orderAddressChangeModalOrderId} change address</ModalHeader>
                <ModalContent >
                    <Dropdown placeholder='Address from' name='addressFrom' options={this.props.orders.addressOptions} onChange={this.onChangeHandler}/><br/>
                    <Dropdown placeholder='Address to' name='addressTo' options={this.props.orders.addressOptions} onChange={this.onChangeHandler}/>
                </ModalContent>
                <ModalActions>
                    <Button color='green' onClick={this.onClickHandler}>Confirm change</Button>
                </ModalActions>
            </Modal>
        );
    }
}

function MapStateToProps(state) {
    return {
        orders: state.orders,
        main: state.main
    };
}

export default withRouter(
    connect( MapStateToProps, {SetErrorModal, SetOrderAddressChangeModal, UpdateOrderAddress})
    (OrderAddressChangeModal));