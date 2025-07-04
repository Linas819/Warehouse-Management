import axios from "axios";
import { SET_ORDER_ID, SET_ORDER_PRODUCTS, SET_ORDER_PRODUCTS_MODAL, SET_DROPDOWN_PRODUCT_OPTIONS } from "./OrderProductsReducer";
import { SetButtonLoading, SetDateTimeFormat, SetErrorModal } from "../../MainAction";
import { GetOrdersData } from "../OrderAction";

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
                text: element.productId + " " + element.name,
                value: element.productId
            }
        });
        dispatch({type: SET_DROPDOWN_PRODUCT_OPTIONS, value: options});
    }
}

export const SetNewProductToOrder = (orderId, productId, productQuantity) => {
    return async(dispatch) => {
        dispatch(SetButtonLoading(true));
        const payload = {
            orderId: orderId,
            productId: productId,
            productQuantity: productQuantity
        }
        const result = await axios.post(`order/product`, payload);
        if(result.data.success)
        {
            dispatch(GetOrderProducts(orderId));
        } else {
            dispatch(SetErrorModal(true, result.data.message));
        }
        dispatch(SetButtonLoading(false));
    }
}

export const DeleteOrderProduct = (orderId, productId) => {
    return async(dispatch) => {
        dispatch(SetButtonLoading(true));
        const result = await axios.delete(`order/products`, {params: {orderId: orderId, productId: productId}});
        if(result.data.success)
        {
            dispatch(GetOrderProducts(orderId));
        } else {
            dispatch(SetErrorModal(true, result.data.message));
        }   
        dispatch(SetButtonLoading(false));
    }
}

export const GetPayslip = (orderId) => {
    return async(dispatch) => {
        dispatch(SetButtonLoading(true));
        const result = await axios.get(`order/complete`, {params: {orderId: orderId}});
        if(result.data.success)
        {
            dispatch(GetOrdersData());
            dispatch(SetOrderProductsModal(false));
        } else {
            dispatch(SetErrorModal(true, result.data.message));
        }   
        dispatch(SetButtonLoading(false));
    }
}