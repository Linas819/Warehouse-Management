import React, { Component } from 'react';
import { connect } from 'react-redux';
import AppHeader from '../AppHeader';
import { Button, Input } from 'semantic-ui-react';
import { LoginUser } from '../MainAction';
import { withRouter } from 'react-router-dom';
import ErrorModal from '../ErrorModal';

class Login extends Component
{
    constructor(props)
    {
        super(props);
        this.state = {
            username: "admin",
            password: "admin"
        }
    }
    onClickHandler = () => {
        this.props.LoginUser(this.state, this.props.history);
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
                    <Input placeholder='Username' name='username' style={{marginTop: '20px'}} onChange={this.onChangehandler}></Input>
                    <Input placeholder='Password' name='password' type='password' style={{marginTop: '20px'}} onChange={this.onChangehandler}></Input>
                    <Button color='green' name='login' loading={this.props.main.isButtonLoading} onClick={this.onClickHandler} style={{marginTop: '20px'}}>Login</Button>
                    <ErrorModal/>
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