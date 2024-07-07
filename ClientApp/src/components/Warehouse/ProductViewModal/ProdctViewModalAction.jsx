import { SET_PRODUCT_VIEW_MODAL, SET_PRODUCT_VIEW_MODAL_HEADER, SET_PRODUCT_VIEW_MODAL_CONTENT, SET_INITIAL_STATE, SET_PRODUCT_VIEW_PRICE_HISTORY } from "./ProductViewModalReducer";
import axios from 'axios';

export const SetProductViewModal = (open) => {
    return (dispatch) => {
        dispatch({type: SET_PRODUCT_VIEW_MODAL, open: open});
    }
}

export const SetProductViewModalContentHeader = (productId) => {
    return (dispatch, getstate) => {
        const products = getstate().warehouse.warehouseData; 
        const product = products.find(x => x.productId == productId);
        dispatch({type: SET_PRODUCT_VIEW_MODAL_HEADER, header: product.productId+"|"+product.productName});
        dispatch({type: SET_PRODUCT_VIEW_MODAL_CONTENT, product: product});
        dispatch(GetProductViewPriceHistory(productId, 0));
    }
}

export const GetProductViewPriceHistory = (productId, limit) => {
    return async(dispatch) => {
        let result = await axios.get(`warehouse/priceHistory`, {params: {
            productId: productId,
            limit: limit
        }});
        dispatch({type: SET_PRODUCT_VIEW_PRICE_HISTORY, priceHistory: result.data.data});
    }
}

export const SetInitialState = () => {
    return (dispatch) => {
        dispatch({type: SET_INITIAL_STATE});
    }
}