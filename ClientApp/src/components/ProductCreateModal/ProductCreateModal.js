import React, { Component } from 'react';
import { connect } from 'react-redux';
import { Button, Form, FormField, Input, Modal, ModalActions, ModalContent, ModalHeader } from 'semantic-ui-react';
import { SetProductCreateModal } from './ProductCreateModalAction';
import { AddInventoryProduct } from '../Warehouse/WarehouseAction';

class ProductCreateModal extends Component {
    constructor(props) {
        super(props);
        this.state = {
            productId: "",
            productName: "",
            productEan: "",
            productType: "",
            productWeight: 0,
            productPrice: 0,
            productQuantity: 0
        }
    }
    onModalClose = () => {
        this.props.SetProductCreateModal(false);
    }
    onClickHandler = () => {
        this.props.AddInventoryProduct(this.state);
    }
    onChangeHandler = (event, data) => {
        switch(data.name){
            case "productId":
            case "productName":
            case "productEan":
            case "productType":
                this.setState({
                    ...this.state,
                    [data.name]: data.value
                });
                break;
            case "productWeight":
            case "productPrice":
            case "productQuantity":
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
            <Modal open={this.props.productCreate.productCreateModalOpen} onClose={this.onModalClose}>
                <ModalHeader>Product Creation</ModalHeader>
                <ModalContent>
                    <Form>
                        <FormField>
                            <Input placeholder='Product Id' name='productId' onChange={this.onChangeHandler}></Input>
                        </FormField>
                        <FormField>
                            <Input placeholder='Product Name' name='productName' onChange={this.onChangeHandler}></Input>
                        </FormField>
                        <FormField>
                            <Input placeholder='Product EAN' name='productEan' onChange={this.onChangeHandler}></Input>
                        </FormField>
                        <FormField>
                            <Input placeholder='Product Type' name='productType' onChange={this.onChangeHandler}></Input>
                        </FormField>
                        <FormField>
                            <Input placeholder='Product Weight' onChange={this.onChangeHandler} type='number' name='productWeight' min='0' step='0.01'></Input>
                        </FormField>
                        <FormField>
                            <Input placeholder='Product Price' onChange={this.onChangeHandler} type='number' name='productPrice' min='0' step='0.01'></Input>
                        </FormField>
                        <FormField>
                            <Input placeholder='Product Quantity' onChange={this.onChangeHandler} type='number' name='productQuantity' min='0' step='1'></Input>
                        </FormField>
                    </Form>
                </ModalContent>
                <ModalActions>
                    <Button onClick={this.onClickHandler} color='green'>Confirm</Button>
                </ModalActions>
            </Modal>
        );
    }
}

function MapStateToProps(state) {
    return {
        productCreate: state.productCreateModal
    };
}

export default connect( MapStateToProps, {SetProductCreateModal, AddInventoryProduct})(ProductCreateModal);