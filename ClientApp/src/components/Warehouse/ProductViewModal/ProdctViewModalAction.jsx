import { SET_PRODUCT_VIEW_MODAL, SET_PRODUCT_VIEW_MODAL_HEADER, SET_PRODUCT_VIEW_MODAL_CONTENT } from "./ProductViewModalReducer";

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
    }
}