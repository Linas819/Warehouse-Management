import React, { Component } from 'react';
import { connect } from 'react-redux';
import { Header } from 'semantic-ui-react';
import { AgGridReact } from 'ag-grid-react';
import { warehouseInventoryColumnDefs } from './WarehouseInventoryUtils';
import { GetWarehouseInventory } from './WarehouseAction';

class WarehouseInventory extends Component
{
    componentDidMount = () => {
        this.props.GetWarehouseInventory();
    }
    render() {
        return(
            <div className='ag-theme-quartz' style={{width: '100%', height: '100%'}}>
                <Header as="h1">Warehouse Inventory</Header>
                <AgGridReact
                    rowData={this.props.warehouse.warehouseData}
                    columnDefs={warehouseInventoryColumnDefs}
                />
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