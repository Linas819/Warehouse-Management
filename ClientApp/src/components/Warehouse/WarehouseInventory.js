import React, { Component } from 'react';
import { connect } from 'react-redux';
import { AgGridReact } from 'ag-grid-react';
import { warehouseInventoryColumnDefs } from './WarehouseInventoryUtils';
import { GetWarehouseInventory } from './WarehouseAction';
import AppHeader from './../AppHeader';
import ProductCreateModal from '../ProductCreateModal/ProductCreateModal';


class WarehouseInventory extends Component
{
    componentDidMount = () => {
        this.props.GetWarehouseInventory();
    }
    render() {
        return(
            <div className='container'>
                <AppHeader/>
                <div className='ag-theme-quartz' style={{width: '100%', height: '100%'}}>
                    <AgGridReact
                        rowData={this.props.warehouse.warehouseData}
                        columnDefs={warehouseInventoryColumnDefs}
                    />
                    <ProductCreateModal/>
                </div>
            </div>
        );
    }
}

function MapStateToProps(state) {
    return {
        main: state.main,
        warehouse: state.warehouse
    };
}

export default connect(MapStateToProps, {GetWarehouseInventory})(WarehouseInventory);