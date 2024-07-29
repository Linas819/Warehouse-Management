import axios from 'axios';
import { SET_PRODUCT_CREATE_MODAL, SET_WAREHOUSE_DATA } from './WarehouseReducer';
import { SetButtonLoading, SetDateTimeFormat, SetErrorModal } from '../MainAction';

export const GetWarehouseProducts = () => {
    return async (dispatch) => {
        let result = await axios.get(`warehouse`);
        if(result.data.success)
        {
            result.data.data.map((element) => {
                element.createdDateTime = SetDateTimeFormat(element.createdDateTime);
                element.updateDateTime = SetDateTimeFormat(element.updateDateTime);
            });
            dispatch({type: SET_WAREHOUSE_DATA, warehouseData: result.data.data})
        } else {
            dispatch(SetErrorModal(true, result.data.message));
        }
    }
}

export const DeleteWarehouseProduct = (productId) => {
    return async (dispatch) => {
        dispatch(SetButtonLoading(true));
        let result = await axios.delete(`warehouse`, {params: {productId: productId}});
        if(result.data.success)
        {
            dispatch(GetWarehouseProducts());
        } else {
            dispatch(SetErrorModal(true, result.data.message));
        }
        dispatch(SetButtonLoading(false));
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

export const SetProductCreateModal = (open) => {
    return (dispatch) => {
        dispatch({type: SET_PRODUCT_CREATE_MODAL, open: open});
    }
}

export const PostWarehouseProduct = (product) => {
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