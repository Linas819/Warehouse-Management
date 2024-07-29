import { SetDateTimeFormat } from "../../MainAction";
import { SET_PRODUCT_VIEW_MODAL, SET_PRODUCT_VIEW_MODAL_HEADER, SET_PRODUCT_VIEW_MODAL_CONTENT, 
    SET_INITIAL_STATE, SET_PRODUCT_VIEW_PRICE_HISTORY, SET_PRODUCT_VIEW_QUANTITY_HISTORY } from "./ProductViewModalReducer";
import axios from 'axios';

export const SetProductViewModal = (open) => {
    return (dispatch) => {
        dispatch({type: SET_PRODUCT_VIEW_MODAL, open: open});
    }
}

export const SetProductViewModalContentAndHeader = (productId) => {
    return (dispatch, getstate) => {
        const products = getstate().warehouse.warehouseData; 
        const product = products.find(x => x.productId === productId);
        dispatch({type: SET_PRODUCT_VIEW_MODAL_HEADER, header: product.productId+"|"+product.productName});
        dispatch({type: SET_PRODUCT_VIEW_MODAL_CONTENT, product: product});
        dispatch(GetProductPriceHistory(productId, 0));
        dispatch(GetProductQuantityHistory(productId, 0));
    }
}

export const GetProductPriceHistory = (productId, limit) => {
    return async(dispatch) => {
        let result = await axios.get(`warehouse/priceHistory`, {params: {
            productId: productId,
            limit: limit
        }});
        result.data.data.map((element) => {
            element.createdDateTime = SetDateTimeFormat(element.createdDateTime);
        });
        await dispatch({type: SET_PRODUCT_VIEW_PRICE_HISTORY, priceHistory: result.data.data});
        return result.data.data;
    }
}

export const GetProductQuantityHistory = (productId, limit) => {
    return async(dispatch) => {
        let result = await axios.get(`warehouse/quantityHistory`, {params: {
            productId: productId,
            limit: limit
        }});
        result.data.data.map((element) => {
            element.createdDateTime = SetDateTimeFormat(element.createdDateTime);
        });
        dispatch({type: SET_PRODUCT_VIEW_QUANTITY_HISTORY, quantityHistory: result.data.data});
        return result.data.data;
    }
}

export const SetInitialState = () => {
    return (dispatch) => {
        dispatch({type: SET_INITIAL_STATE});
    }
}