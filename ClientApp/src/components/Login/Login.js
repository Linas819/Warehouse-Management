import React, { Component } from 'react';
import { connect } from 'react-redux';
import AppHeader from './../AppHeader';
import { Button, Input } from 'semantic-ui-react';
import { LoginUser } from './LoginAction';
import { withRouter } from 'react-router-dom';

class Login extends Component
{
    constructor(props)
    {
        super(props);
        this.state = {
            username: "",
            password: ""
        }
    }
    onClickHandler = (event, data) => {
        switch(data.name) {
            case "login":
                this.props.LoginUser(this.state, this.props.history);
                break;
            case "register":
                break;
            default:
                break;
        }
    }
    onChangehandler = (event, data) => {
        this.setState({
            ...this.state,
            [data.name]: data.value
        });
    }
    render() {
        return(
            <div className='container'>
                <AppHeader/>
                <div className='containerItems'>
                    <Input placeholder='Username' name='username' style={{marginTop: '20px'}}></Input>
                    <Input placeholder='Password' name='password' type='password' style={{marginTop: '20px'}}></Input>
                    <Button color='green' name='login' onClick={this.onClickHandler} style={{marginTop: '20px'}}>Login</Button>
                    <Button color='blue' name='register' onClick={this.onClickHandler} style={{marginTop: '20px'}}>Register</Button>
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
    connect(MapStateToProps, {LoginUser})
(Login));