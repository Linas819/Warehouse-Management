import React, { Component } from 'react';
import { Button } from 'semantic-ui-react';
import AppHeader from './AppHeader';
import { withRouter } from 'react-router-dom';
import { connect } from 'react-redux';
import { LoginAuthentication } from './MainAction';
import ErrorModal from './ErrorModal';
import { SetErrorModal } from './MainAction';
import { accessRights } from './MainAction';

class Menu extends Component {
    componentDidMount = () => {
        this.props.LoginAuthentication(this.props.history);
    }
    onClickHandler = (event, data) => {
        const buttonIndex = parseFloat(data.name);
        const { userAccess } = this.props.main;
        const pageAccessRight = accessRights[buttonIndex];
        const userAccessRightIndex = userAccess.findIndex(element => element === pageAccessRight.accessId);
        if(userAccessRightIndex>-1)
            this.props.history.push(pageAccessRight.pathname);
        else
            this.props.SetErrorModal(true, "User does not have access for this page");
    }
    render() {
        return(
            <div className='container'>
                <AppHeader/>
                <div className='containerItems'>
                    <Button color='green' style={{marginTop: '20px'}} name="0" onClick={this.onClickHandler}>Inventory</Button>
                    <Button color='blue' style={{marginTop: '20px'}} name="1" onClick={this.onClickHandler}>Orders</Button>
                    <Button color='yellow' style={{marginTop: '20px'}} name="2" onClick={this.onClickHandler}>Register</Button>
                </div>
                <ErrorModal/>
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
    connect(MapStateToProps, {LoginAuthentication, SetErrorModal})
(Menu));