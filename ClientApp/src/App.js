import React, { Component } from 'react';
import { Routes, Route } from 'react-router-dom';
import './custom.css';
import WarehouseInventory from './components/Warehouse/WarehouseInventory';
import Login from './components/Login/Login';

export default class App extends Component {
  render() {
    return (
      <Routes>
        <Route exact path="/" element={<Login/>}/>
        <Route exact path="/warehouseinventory" element={<WarehouseInventory/>}/>
      </Routes>
    );
  }
}
