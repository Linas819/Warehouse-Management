import React, { Component } from 'react';
import { connect } from 'react-redux';
import { Button } from 'semantic-ui-react';
import { withRouter } from 'react-router-dom';
import { DeleteAddress } from './OrderAction';

class AddressDeleteButton extends Component{
    onClickHandler = (event, data) => {
        const { addressId } = this.props.data;
        this.props.DeleteAddress(addressId);
    }
    render() {
        return(
            <Button color='red' onClick={this.onClickHandler} loading={this.props.main.isButtonLoading}>Delete</Button>
        );
    }
}

function MapStateToProps(state) {
    return {
        main: state.main
    };
}

export default withRouter(
    connect( MapStateToProps, {DeleteAddress})
(AddressDeleteButton));