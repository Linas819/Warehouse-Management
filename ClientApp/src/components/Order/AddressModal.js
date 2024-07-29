import React, { Component } from 'react';
import { connect } from 'react-redux';
import { withRouter } from 'react-router-dom';
import { Button, Input, Modal, ModalActions, ModalContent, ModalHeader, Header, Divider } from 'semantic-ui-react';
import { SetAddressModal, PostAddress } from './OrderAction';
import { AgGridReact } from 'ag-grid-react';
import { addressesColumnDefs } from './OrdersUtils';
import { SetErrorModal } from '../MainAction';

class AddressModal extends Component {
    constructor(props)
    {
        super(props);
        this.state = {
            addressCountry: "",
            addressZipCode: "",
            addressRegion: "",
            addressCity: "",
            addressStreet: "",
            addressHouse: "",
            addressApartment: ""
        }
    }
    onModalClose = () => {
        this.props.SetAddressModal(false);
    }
    onClickHandler = () => {
        const { addressCountry, addressZipCode, addressRegion, addressCity, addressStreet, addressHouse } = this.state;
        if(addressCountry === "")
        {
            this.props.SetErrorModal(true, "Country is required");
            return;
        }
        if(addressZipCode === "")
        {
            this.props.SetErrorModal(true, "Zip is required");
            return;
        }
        if(addressRegion === "")
        {
            this.props.SetErrorModal(true, "Region is required");
            return;
        }
        if(addressCity === "")
        {
            this.props.SetErrorModal(true, "City is required");
            return;
        }
        if(addressStreet === "")
        {
            this.props.SetErrorModal(true, "Street is required");
            return;
        }
        if(addressHouse === "")
        {
            this.props.SetErrorModal(true, "House is required");
            return;
        }
        this.props.PostAddress(this.state);
    }
    onCellValueChangeHandler = (event) => {

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
            <Modal size='fullscreen' open={this.props.orders.addressModalOpen} onClose={this.onModalClose}>
                <ModalHeader>Addresses</ModalHeader>
                <ModalContent>
                    <Header as='h2'>Add address</Header>
                    <Input placeholder='Country' name='addressCountry' onChange={this.onChangeHandler}/>
                    <Input placeholder='ZIP' name='addressZipCode' onChange={this.onChangeHandler}/>
                    <Input placeholder='Region' name='addressRegion' onChange={this.onChangeHandler}/>
                    <Input placeholder='City' name='addressCity' onChange={this.onChangeHandler}/>
                    <Input placeholder='Street' name='addressStreet' onChange={this.onChangeHandler}/>
                    <Input placeholder='House' name='addressHouse' onChange={this.onChangeHandler}/>
                    <Input placeholder='Apartment' name='addressApartment' onChange={this.onChangeHandler}/>
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
    connect( MapStateToProps, {SetAddressModal, PostAddress, SetErrorModal})
    (AddressModal));