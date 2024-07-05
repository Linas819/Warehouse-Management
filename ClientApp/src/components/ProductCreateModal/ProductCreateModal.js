import React, { Component } from 'react';
import { connect } from 'react-redux';
import { Button, Form, FormField, Input, Modal, ModalActions, ModalContent, ModalHeader } from 'semantic-ui-react';
import { SetProductCreateModal } from './ProductCreateModalAction';

class ProductCreateModal extends Component {
    constructor(props) {
        super(props);
        this.state = {
            productName: "",
            productEan: "",
            productType: "",
            productWeight: "",
            productPrice: "",
            productQuantity: "",
        }
    }
    onModalClose = () => {
        this.props.SetProductCreateModal(false);
    }
    onClickHandler = () => {
        
    }
    onChangeHandler = (event, data) => {
        this.setState({
            ...this.state,
            [data.name]: data.value
        });
    }
    render() {
        return (
            <Modal open={this.props.productCreate.productCreateModalOpen} onClose={this.onModalClose}>
                <ModalHeader>Product Creation</ModalHeader>
                <ModalContent>
                    <Form>
                        <FormField>
                            <Input placeholder='Product Name' name='productName'></Input>
                        </FormField>
                        <FormField>
                            <Input placeholder='Product EAN' name='productEan'></Input>
                        </FormField>
                        <FormField>
                            <Input placeholder='Product Type' name='productType'></Input>
                        </FormField>
                        <FormField>
                            <Input placeholder='Product Weight' type='number' name='productWeight' min='0' step='0.01'></Input>
                        </FormField>
                        <FormField>
                            <Input placeholder='Product Price' type='number' name='productPrice' min='0' step='0.01'></Input>
                        </FormField>
                        <FormField>
                            <Input placeholder='Product Quantity' type='number' name='productQuantity' min='0' step='0.01'></Input>
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

export default connect( MapStateToProps, {SetProductCreateModal})(ProductCreateModal);