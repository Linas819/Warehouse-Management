import React, { Component } from 'react';
import { connect } from 'react-redux';
import { AgGridReact } from 'ag-grid-react';
import { warehouseInventoryColumnDefs } from './WarehouseInventoryUtils';
import { GetWarehouseProducts, UpdateWarehouseProduct, UpdateWarehouseProductPrice, UpdateWarehouseProductQuantity } from './WarehouseAction';
import AppHeader from './../AppHeader';
import ProductViewModal from './ProductViewModal/ProductViewModal';
import { withRouter } from 'react-router-dom';
import { LoginAuthentication } from '../MainAction';
import ErrorModal from '../ErrorModal';
import ProductCreateModal from './ProductCreateModal';

class WarehouseInventory extends Component
{
    componentDidMount = () => {
        this.props.LoginAuthentication(this.props.history);
        this.props.GetWarehouseProducts();
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
                this.props.UpdateWarehouseProduct(productUpdate)
                break;
            case "productPrice":
                this.props.UpdateWarehouseProductPrice(productUpdate)
                break;
            case "productQuantity":
                this.props.UpdateWarehouseProductQuantity(productUpdate)
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
                </div>
                <ProductCreateModal/>
                <ProductViewModal/>
                <ErrorModal/>
            </div>
        );
    }
}

function MapStateToProps(state) {
    return {
        warehouse: state.warehouse
    };
}

export default withRouter(
    connect(MapStateToProps, {GetWarehouseProducts, UpdateWarehouseProduct, UpdateWarehouseProductPrice, UpdateWarehouseProductQuantity, LoginAuthentication})
    (WarehouseInventory));