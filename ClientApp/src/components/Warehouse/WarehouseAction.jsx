import axios from 'axios';
import { SET_WAREHOUSE_DATA } from './WarehouseReducer';
import { SetProductCreateModal } from '../ProductCreateModal/ProductCreateModalAction';

export const GetWarehouseInventory = () => {
    return async (dispatch) => {
        let result = await axios.get(`warehouse`);
        dispatch({type: SET_WAREHOUSE_DATA, warehouseData: result.data.data})
    }
}

export const DeleteInventoryItem = (productId) => {
    return async (dispatch) => {
        await axios.delete(`warehouse/${productId}`);
        dispatch(GetWarehouseInventory());
    }
}

export const AddInventoryProduct = (product) => {
    return async (dispatch) => {
        await axios.post(`warehouse`, product)
        dispatch(GetWarehouseInventory());
        dispatch(SetProductCreateModal(false));
    }
}

export const UpdateInventoryProduct = (productUpdate) => {
    return async(dispatch) => {
        await axios.put(`warehouse`, productUpdate)
        dispatch(GetWarehouseInventory());
    }
}

export const UpdateProductPrice = (productUpdate) => {
    return async(dispatch) => {
        await axios.put(`warehouse/priceChange`, productUpdate);
        dispatch(GetWarehouseInventory());
    }
}

export const UpdateProductQuantity = (productUpdate) => {
    return async(dispatch) => {
        await axios.put(`warehouse/quantityChange`, productUpdate);
        dispatch(GetWarehouseInventory());
    }
}