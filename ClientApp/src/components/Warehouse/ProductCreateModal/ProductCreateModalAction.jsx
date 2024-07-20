import axios from 'axios';
import { SET_PRODUCT_CREATE_MODAL } from './ProductCreateModalReducer'
import { GetWarehouseProducts } from '../WarehouseAction';

export const SetProductCreateModal = (open) => {
    return (dispatch) => {
        dispatch({type: SET_PRODUCT_CREATE_MODAL, open: open});
    }
}

export const PostInventoryProduct = (product) => {
    return async (dispatch, getstate) => {
        const userId = getstate().main.userId;
        product.createdUserId = userId;
        product.updatedUserId = userId;
        await axios.post(`warehouse`, product)
        dispatch(GetWarehouseProducts());
        dispatch(SetProductCreateModal(false));
    }
}