import axios from 'axios';
import { SET_PRODUCT_CREATE_MODAL } from './ProductCreateModalReducer'
import { GetWarehouseProducts } from '../WarehouseAction';
import { SetButtonLoading, SetErrorModal } from '../../MainAction';

export const SetProductCreateModal = (open) => {
    return (dispatch) => {
        dispatch({type: SET_PRODUCT_CREATE_MODAL, open: open});
    }
}

export const PostInventoryProduct = (product) => {
    return async (dispatch, getstate) => {
        dispatch(SetButtonLoading(true));
        const userId = getstate().main.userId;
        product.createdUserId = userId;
        product.updatedUserId = userId;
        let result = await axios.post(`warehouse`, product);
        if(result.data.success)
        {
            dispatch(GetWarehouseProducts());
            dispatch(SetProductCreateModal(false));
        } else {
            dispatch(SetErrorModal(true, result.data.message));
        }
        dispatch(SetButtonLoading(false));
    }
}