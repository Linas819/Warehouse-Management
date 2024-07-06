import axios from 'axios';
import { SET_MODAL } from './ProductCreateModalReducer'
import { GetWarehouseProducts } from '../WarehouseAction';

export const SetProductCreateModal = (open) => {
    return (dispatch) => {
        dispatch({type: SET_MODAL, open: open});
    }
}

export const PostInventoryProduct = (product) => {
    return async (dispatch) => {
        await axios.post(`warehouse`, product)
        dispatch(GetWarehouseProducts());
        dispatch(SetProductCreateModal(false));
    }
}