import axios from 'axios';
import { SET_WAREHOUSE_DATA } from './WarehouseReducer';

export const GetWarehouseProducts = () => {
    return async (dispatch) => {
        let result = await axios.get(`warehouse`);
        dispatch({type: SET_WAREHOUSE_DATA, warehouseData: result.data.data})
    }
}

export const DeleteWarehouseProduct = (productId) => {
    return async (dispatch) => {
        await axios.delete(`warehouse/${productId}`);
        dispatch(GetWarehouseProducts());
    }
}

export const UpdateWarehouseProduct = (productUpdate) => {
    return async(dispatch) => {
        await axios.put(`warehouse`, productUpdate)
        dispatch(GetWarehouseProducts());
    }
}

export const UpdateWarehouseProductPrice = (productUpdate) => {
    return async(dispatch) => {
        await axios.put(`warehouse/priceChange`, productUpdate);
        dispatch(GetWarehouseProducts());
    }
}

export const UpdateWarehouseProductQuantity = (productUpdate) => {
    return async(dispatch) => {
        await axios.put(`warehouse/quantityChange`, productUpdate);
        dispatch(GetWarehouseProducts());
    }
}