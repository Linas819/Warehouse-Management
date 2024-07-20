import axios from 'axios';
import { SET_WAREHOUSE_DATA } from './WarehouseReducer';
import { SetErrorModal } from '../MainAction';

export const GetWarehouseProducts = () => {
    return async (dispatch) => {
        let result = await axios.get(`warehouse`);
        if(result.data.success)
        {
            dispatch({type: SET_WAREHOUSE_DATA, warehouseData: result.data.data})
        } else {
            dispatch(SetErrorModal(true, result.data.message));
        }
    }
}

export const DeleteWarehouseProduct = (productId) => {
    return async (dispatch) => {
        let result = await axios.delete(`warehouse/${productId}`);
        if(result.data.success)
        {
            dispatch(GetWarehouseProducts());
        } else {
            dispatch(SetErrorModal(true, result.data.message));
        }
    }
}

export const UpdateWarehouseProduct = (productUpdate) => {
    return async(dispatch) => {
        let result = await axios.put(`warehouse`, productUpdate)
        if(result.data.success)
        {
            dispatch(GetWarehouseProducts());
        } else {
            dispatch(SetErrorModal(true, result.data.message));
        }
    }
}

export const UpdateWarehouseProductPrice = (productUpdate) => {
    return async(dispatch) => {
        let result = await axios.put(`warehouse/priceChange`, productUpdate);
        if(result.data.success)
        {
            dispatch(GetWarehouseProducts());
        } else {
            dispatch(SetErrorModal(true, result.data.message));
        }
    }
}

export const UpdateWarehouseProductQuantity = (productUpdate) => {
    return async(dispatch) => {
        let result = await axios.put(`warehouse/quantityChange`, productUpdate);
        if(result.data.success)
        {
            dispatch(GetWarehouseProducts());
        } else {
            dispatch(SetErrorModal(true, result.data.message));
        }
    }
}