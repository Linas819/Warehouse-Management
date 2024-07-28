import React, { Component } from 'react';
import { connect } from 'react-redux';
import { AgGridReact } from 'ag-grid-react';
import { withRouter } from 'react-router-dom';
import ErrorModal from '../ErrorModal';
import { GetOrdersData } from './OrderAction';
import AppHeader from '../AppHeader';
import { LoginAuthentication } from '../MainAction';
import { ordersListColumnDefs } from './OrdersUtils';
import OrderProductsModal from './OrderProducts/OrderProductsModal';
import OrderCreateModal from './OrderCreateModal';
import OrderAddressChangeModal from './OrderAddressChangeModal';

class Orders extends Component
{
    componentDidMount = () => {
        this.props.LoginAuthentication(this.props.history);
        this.props.GetOrdersData();
    }
    render() {
        return(
            <div className='container'>
                <AppHeader/>
                <div className='ag-theme-quartz' style={{width: '100%', height: '100%'}}>
                    <AgGridReact
                        rowData={this.props.orders.ordersData}
                        columnDefs={ordersListColumnDefs}
                    />
                </div>
                <ErrorModal/>
                <OrderProductsModal/>
                <OrderCreateModal/>
                <OrderAddressChangeModal/>
            </div>
        );
    }
}

function MapStateToProps(state) {
    return {
        orders: state.orders
    };
}

export default withRouter(
    connect(MapStateToProps, {GetOrdersData, LoginAuthentication})
    (Orders));