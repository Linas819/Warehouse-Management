import axios from 'axios';
import { SET_PRODUCT_CREATE_MODAL } from './ProductCreateModalReducer'
import { GetWarehouseProducts } from '../WarehouseAction';

export const SetProductCreateModal = (open) => {
    return (dispatch) => {
        dispatch({type: SET_PRODUCT_CREATE_MODAL, open: open});
    }
}

export const PostInventoryProduct = (product) => {
    return async (dispatch) => {
        await axios.post(`warehouse`, product)
        dispatch(GetWarehouseProducts());
        dispatch(SetProductCreateModal(false));
    }
}