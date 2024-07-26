import axios from "axios";
import { SET_ORDER_ID, SET_ORDER_PRODUCTS, SET_ORDER_PRODUCTS_MODAL, SET_PRODUCT_OPTIONS } from "./OrderProductsReducer";
import { SetDateTimeFormat, SetErrorModal } from "../../MainAction";

export const GetOrderProducts = (orderId) => {
    return async(dispatch) => {
        const result = await axios.get(`order/products`, {params: {orderId: orderId}});
        result.data.data.map((element) => {element.createdDateTime = SetDateTimeFormat(element.createdDateTime)});
        dispatch(SetNewProductOptions());
        dispatch({type: SET_ORDER_ID, orderId: orderId});
        dispatch({type: SET_ORDER_PRODUCTS, value: result.data.data});
    }
}

export const SetOrderProductsModal = (open) => {
    return (dispatch) => {
        dispatch({type: SET_ORDER_PRODUCTS_MODAL, open: open});
    }
}

export const SetNewProductOptions = () => {
    return async(dispatch) => {
        const result = await axios.get(`warehouse/products`);
        const options = result.data.data.map((element) => {
            return {
                key: element.productId,
                text: element.productId + " " + element.productName,
                value: element.productId
            }
        });
        dispatch({type: SET_PRODUCT_OPTIONS, value: options});
    }
}

export const SetNewProductToOrder = (orderId, productId, productQuantity) => {
    return async(dispatch) => {
        const payload = {
            orderId: orderId,
            productId: productId,
            productQuantity: productQuantity
        }
        const result = await axios.post(`order/neworderproduct`, payload);
        if(result.data.success)
        {
            dispatch(GetOrderProducts(orderId));
        } else {
            dispatch(SetErrorModal(true, result.data.message));
        }  
    }
}

export const DeleteOrderProduct = (orderId, productId) => {
    return async(dispatch) => {
        const result = await axios.delete(`order/products`, {params: {orderId: orderId, productId: productId}});
        if(result.data.success)
        {
            dispatch(GetOrderProducts(orderId));
        } else {
            dispatch(SetErrorModal(true, result.data.message));
        }   
    }
}