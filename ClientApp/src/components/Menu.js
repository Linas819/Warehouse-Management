import React, { Component } from 'react';
import { Button } from 'semantic-ui-react';
import AppHeader from './AppHeader';
import { withRouter } from 'react-router-dom';
import { connect } from 'react-redux';
import { LoginAuthentication } from './User/UserAction';

class Menu extends Component {
    componentDidMount = () => {
        this.props.LoginAuthentication(this.props.history);
    }
    onClickHandler = (event, data) => {
        const names = data.name.split("|");
        const pathname = "/"+names[1];
        const accessId = names[0];
        const { userAccess } = this.props.main;
        if(userAccess.includes(accessId))
            this.props.history.push(pathname);
    }
    render() {
        return(
            <div className='container'>
                <AppHeader/>
                <div className='containerItems'>
                    <Button color='green' style={{marginTop: '20px'}} name="inv001|warehouseinventory" onClick={this.onClickHandler}>Inventory</Button>
                    <Button color='blue' style={{marginTop: '20px'}} name="ord002|orders" onClick={this.onClickHandler}>Orders</Button>
                    <Button color='yellow' style={{marginTop: '20px'}} name="reg003|register" onClick={this.onClickHandler}>Register</Button>
                </div>
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
    connect(MapStateToProps, {LoginAuthentication})
(Menu));