import React, { Component } from 'react';
import { connect } from 'react-redux';
import { AgGridReact } from 'ag-grid-react';
import { warehouseInventoryColumnDefs } from './WarehouseInventoryUtils';
import { GetWarehouseInventory, UpdateInventoryProduct, UpdateProductPrice, UpdateProductQuantity } from './WarehouseAction';
import AppHeader from './../AppHeader';
import ProductCreateModal from '../ProductCreateModal/ProductCreateModal';


class WarehouseInventory extends Component
{
    componentDidMount = () => {
        this.props.GetWarehouseInventory();
    }
    onCellValueChanged = (event) => {
        event.data[event.colDef.field] = event.oldValue; //Prevent error in change of data value from AGGrid before redux dispatch
        const productUpdate = {
            productId: event.data.productId,
            fieldName: event.colDef.field,
            newValue: event.newValue.toString()
        }
        switch(event.colDef.field) {
            case "productName":
            case "productEan":
            case "productType":
            case "productWeight":
                this.props.UpdateInventoryProduct(productUpdate)
                break;
            case "productPrice":
                this.props.UpdateProductPrice(productUpdate)
                break;
            case "productQuantity":
                this.props.UpdateProductQuantity(productUpdate)
                break;
            default:
                break;
        }
    }
    render() {
        return(
            <div className='container'>
                <AppHeader/>
                <div className='ag-theme-quartz' style={{width: '100%', height: '100%'}}>
                    <AgGridReact
                        rowData={this.props.warehouse.warehouseData}
                        columnDefs={warehouseInventoryColumnDefs}
                        onCellValueChanged={this.onCellValueChanged}
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

export default connect(MapStateToProps, {GetWarehouseInventory, UpdateInventoryProduct, UpdateProductPrice, UpdateProductQuantity})(WarehouseInventory);