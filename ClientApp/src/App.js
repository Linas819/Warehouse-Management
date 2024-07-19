import React, { Component } from 'react';
import { Switch, Route } from 'react-router-dom';
import './custom.css';
import WarehouseInventory from './components/Warehouse/WarehouseInventory';
import Login from './components/User/Login';
import Menu from './components/Menu';

export default class App extends Component {
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
