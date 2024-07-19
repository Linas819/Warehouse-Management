import React, { Component } from 'react';
import { Switch, Route } from 'react-router-dom';
import './custom.css';
import WarehouseInventory from './components/Warehouse/WarehouseInventory';
import Login from './components/User/Login';
import Menu from './components/Menu';
import { withRouter } from 'react-router-dom';
import { connect } from 'react-redux';
import { LoginAuthentication } from './components/User/UserAction';

class App extends Component {
  componentDidMount = () => {
    this.props.LoginAuthentication(this.props.history)
  }
  render() {
    return (
      <Switch>
        <Route exact path="/" component={Login}/>
        <Route exact path="/warehouseinventory" component={WarehouseInventory}/>
        <Route exact path="/menu" component={Menu}/>
      </Switch>
    );
  }
}

function MapStateToProps(state) {
  return {
      warehouse: state.warehouse
  };
}

export default withRouter(
  connect(MapStateToProps, {LoginAuthentication})
  (App));
