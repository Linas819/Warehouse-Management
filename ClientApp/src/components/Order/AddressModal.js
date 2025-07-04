import React, { Component } from 'react';
import { connect } from 'react-redux';
import { withRouter } from 'react-router-dom';
import { Button, Input, Modal, ModalActions, ModalContent, ModalHeader, Header, Divider } from 'semantic-ui-react';
import { SetAddressModal, PostAddress } from './OrderAction';
import { AgGridReact } from 'ag-grid-react';
import { addressesColumnDefs } from './OrdersUtils';
import { SetErrorModal } from '../MainAction';
import { UpdateAddress } from './OrderAction';

class AddressModal extends Component {
    constructor(props)
    {
        super(props);
        this.state = {
            country: "",
            zip: "",
            region: "",
            city: "",
            street: "",
            house: "",
            apartment: ""
        }
    }
    onModalClose = () => {
        this.props.SetAddressModal(false);
    }
    onClickHandler = () => {
        const { country, zip, region, city, street, house } = this.state;
        if(country === "")
        {
            this.props.SetErrorModal(true, "Country is required");
            return;
        }
        if(zip === "")
        {
            this.props.SetErrorModal(true, "Zip is required");
            return;
        }
        if(region === "")
        {
            this.props.SetErrorModal(true, "Region is required");
            return;
        }
        if(city === "")
        {
            this.props.SetErrorModal(true, "City is required");
            return;
        }
        if(street === "")
        {
            this.props.SetErrorModal(true, "Street is required");
            return;
        }
        if(house === "")
        {
            this.props.SetErrorModal(true, "House is required");
            return;
        }
        this.props.PostAddress(this.state);
    }
    onCellValueChangeHandler = (event) => {
        event.data[event.colDef.field] = event.oldValue; //Prevent error in change of data value from AGGrid before redux dispatch
        const addressUpdate = {
            addressId: event.data.addressId,
            fieldName: event.colDef.field,
            newValue: event.newValue.toString()
        };
        this.props.UpdateAddress(addressUpdate);
    }
    onChangeHandler = (event, data) => {
        this.setState({
            ...this.state,
            [data.name]: data.value
        });
    }
    render(){
        const windowHeight = window.innerHeight;
        return(
            <Modal size='fullscreen' open={this.props.orders.addressModal} onClose={this.onModalClose}>
                <ModalHeader>Addresses</ModalHeader>
                <ModalContent>
                    <Header as='h2'>Add address</Header>
                    <Input placeholder='Country' name='country' onChange={this.onChangeHandler}/>
                    <Input placeholder='ZIP' name='zip' onChange={this.onChangeHandler}/>
                    <Input placeholder='Region' name='region' onChange={this.onChangeHandler}/>
                    <Input placeholder='City' name='city' onChange={this.onChangeHandler}/>
                    <Input placeholder='Street' name='street' onChange={this.onChangeHandler}/>
                    <Input placeholder='House' name='house' onChange={this.onChangeHandler}/>
                    <Input placeholder='Apartment' name='apartment' onChange={this.onChangeHandler}/>
                    <Divider/>
                    <div className='ag-theme-quartz' style={{width: '100%', height: windowHeight/2}}>
                        <AgGridReact
                            rowData={this.props.orders.addressData}
                            columnDefs={addressesColumnDefs}
                            onCellValueChanged={this.onCellValueChangeHandler}
                        />
                    </div>
                </ModalContent>
                <ModalActions>
                    <Button color='green' onClick={this.onClickHandler} loading={this.props.main.isButtonLoading}>Add address</Button>
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
    connect( MapStateToProps, {SetAddressModal, PostAddress, SetErrorModal, UpdateAddress})
    (AddressModal));