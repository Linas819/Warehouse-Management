import React, { Component } from 'react';
import { connect } from 'react-redux';
import { Button, Form, FormField, Input, Modal, ModalActions, ModalContent, ModalHeader } from 'semantic-ui-react';
import { SetProductCreateModal, PostWarehouseProduct } from './WarehouseAction';
import { withRouter } from 'react-router-dom';
import { SetErrorModal } from '../MainAction';

class ProductCreateModal extends Component {
    constructor(props) {
        super(props);
        this.state = {
            productId: "",
            name: "",
            ean: "",
            type: "",
            weight: 0,
            price: 0,
            quantity: 0
        }
    }
    onModalClose = () => {
        this.props.SetProductCreateModal(false);
    }
    onClickHandler = () => {
        const state = this.state;
        if(state.productId === "")
        {
            this.props.SetErrorModal(true, "Product ID is required");
            return;
        }
        if(state.name === "")
        {
            this.props.SetErrorModal(true, "Product name is required");
            return;
        }
        if(state.ean === "")
        {
            this.props.SetErrorModal(true, "Product EAN is required");
            return;
        }
        if(state.type === "")
        {
            this.props.SetErrorModal(true, "Product type is required");
            return;
        }
        if(state.weight === 0)
        {
            this.props.SetErrorModal(true, "Product weight must be more than 0");
            return;
        }
        if(state.price === 0)
        {
            this.props.SetErrorModal(true, "Product price must be more than 0");
            return;
        }
        if(state.quantity === 0)
        {
            this.props.SetErrorModal(true, "Product quantity must be more than 0");
            return;
        }
        this.props.PostWarehouseProduct(this.state);
    }
    onChangeHandler = (event, data) => {
        switch(data.name){
            case "productId":
            case "name":
            case "ean":
            case "type":
                this.setState({
                    ...this.state,
                    [data.name]: data.value
                });
                break;
            case "weight":
            case "price":
            case "quantity":
                this.setState({
                    ...this.state,
                    [data.name]: parseFloat(data.value)
                });
                break;
            default:
                break;
        }
    }
    render() {
        return (
            <Modal size='mini' open={this.props.warehouse.productCreateModal} onClose={this.onModalClose}>
                <ModalHeader>Product Creation</ModalHeader>
                <ModalContent>
                    <Form>
                        <FormField>
                            <Input placeholder='Product Id' name='productId' onChange={this.onChangeHandler}></Input>
                        </FormField>
                        <FormField>
                            <Input placeholder='Product Name' name='name' onChange={this.onChangeHandler}></Input>
                        </FormField>
                        <FormField>
                            <Input placeholder='Product EAN' name='ean' onChange={this.onChangeHandler}></Input>
                        </FormField>
                        <FormField>
                            <Input placeholder='Product Type' name='type' onChange={this.onChangeHandler}></Input>
                        </FormField>
                        <FormField>
                            <Input placeholder='Product Weight (kg)' onChange={this.onChangeHandler} type='number' name='weight' min='0' step='0.01'></Input>
                        </FormField>
                        <FormField>
                            <Input placeholder='Product Price ($)' onChange={this.onChangeHandler} type='number' name='price' min='0' step='0.01'></Input>
                        </FormField>
                        <FormField>
                            <Input placeholder='Product Quantity' onChange={this.onChangeHandler} type='number' name='quantity' min='0' step='1'></Input>
                        </FormField>
                    </Form>
                </ModalContent>
                <ModalActions>
                    <Button onClick={this.onClickHandler} loading={this.props.main.isButtonLoading} color='green'>Confirm</Button>
                </ModalActions>
            </Modal>
        );
    }
}

function MapStateToProps(state) {
    return {
        warehouse: state.warehouse,
        main: state.main
    };
}

export default withRouter(
    connect( MapStateToProps, {SetProductCreateModal, PostWarehouseProduct, SetErrorModal})
    (ProductCreateModal));