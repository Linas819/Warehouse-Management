import axios from 'axios';
import { SET_WAREHOUSE_DATA } from './WarehouseReducer';

export const GetWarehouseInventory = () => {
    return async (dispatch) => {
        let result = await axios.get(`warehouse`);
        dispatch({type: SET_WAREHOUSE_DATA, warehouseData: result.data.data})
    }
}

export const DeleteInventoryItem = (productId) => {
    return async (dispatch) => {
        let result = await axios.delete(`warehouse`);
        dispatch(GetWarehouseInventory());
    }
}