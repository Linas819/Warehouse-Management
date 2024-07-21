import React, { Component } from 'react';
import { connect } from 'react-redux';
import AppHeader from '../AppHeader';
import { Button, Checkbox, Input, Header } from 'semantic-ui-react';
import { withRouter } from 'react-router-dom';
import ErrorModal from '../ErrorModal';
import { accessRights } from '../MainAction';
import { LoginAuthentication, RegisterUser } from '../MainAction';

class Register extends Component {
    constructor(props)
    {
        super(props);
        this.state = {
            username: "",
            password: "",
            firstName: "",
            lastName: "",
            userRights: {
                inventory: false,
                orders: false,
                register: false
            }
        }
    }
    componentDidMount = () => {
        this.props.LoginAuthentication(this.props.history);
    }
    onChangehandler = (event, data) => {
        switch(data.name){
            case "username":
            case "password":
            case "firstName":
            case "lastName":
                this.setState({
                    ...this.state,
                    [data.name]: data.value
                });
                break;
            case "rights":
                this.setState({
                    ...this.state,
                    userRights: {
                        ...this.state.userRights,
                        [data.label.toLowerCase()]: data.checked
                    }
                });
                break;
            default:
                break;
        }
    }
    onClickHandler = () => {
        this.props.RegisterUser(this.state, this.props.history);
    }
    render() {
        return(
            <div className='container'>
                <AppHeader/>
                <div className='containerItems'>
                    <Input placeholder='First Name' name='firstName' style={{marginTop: '20px'}} onChange={this.onChangehandler}/>
                    <Input placeholder='Last Name' name='lastName' style={{marginTop: '20px'}} onChange={this.onChangehandler}/>
                    <Input placeholder='Username' name='username' style={{marginTop: '20px'}} onChange={this.onChangehandler}/>
                    <Input placeholder='Password' name='password' style={{marginTop: '20px'}} onChange={this.onChangehandler} type='password'/>
                    <div className='rightsCheckbox'>
                        <Header as='h3'>User access Rights</Header>
                        {accessRights.map((rights) => {
                            return <Checkbox name='rights' key={rights.accessId} label={rights.accessName} onChange={this.onChangehandler}/>
                        })}
                    </div>
                    <Button color='green' style={{marginTop: '20px'}} onClick={this.onClickHandler}>Register</Button>
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
    connect(MapStateToProps, {LoginAuthentication, RegisterUser})
(Register));