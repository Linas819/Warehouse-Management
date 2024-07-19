import React, { Component } from 'react';
import { Link } from 'react-router-dom';
import { Button } from 'semantic-ui-react';
import AppHeader from './AppHeader';

export default class Menu extends Component {
    render() {
        return(
            <div className='container'>
                <AppHeader/>
                <div className='containerItems'>
                    <Link to="/warehouseinventory">
                        <Button color='green' style={{marginTop: '20px'}}>Inventory</Button>
                    </Link>
                    <Link to="/orders">
                        <Button color='blue' style={{marginTop: '20px'}}>Orders</Button>
                    </Link>
                    <Link to="/register">
                        <Button color='yellow' style={{marginTop: '20px'}}>Register</Button>
                    </Link>
                </div>
            </div>
        );
    }
}