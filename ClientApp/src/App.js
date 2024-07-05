import React, { Component } from 'react';
import { Routes, Route } from 'react-router-dom';
import './custom.css';
import WarehouseInventory from './components/Warehouse/WarehouseInventory';

export default class App extends Component {
  render() {
    return (
      <Routes>
        <Route path="/" element={<WarehouseInventory/>}/>
      </Routes>
    );
  }
}
