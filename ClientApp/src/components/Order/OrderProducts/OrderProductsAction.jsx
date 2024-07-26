import axios from "axios";
import { SET_ORDER_ID, SET_ORDER_PRODUCTS, SET_ORDER_PRODUCTS_MODAL } from "./OrderProductsReducer";
import { SetDateTimeFormat } from "../../MainAction";

export const GetOrderProducts = (orderId) => {
    return async(dispatch) => {
        const result = await axios.get(`order/products`, {params: {orderId: orderId}});
        result.data.data.map((element) => {element.createdDateTime = SetDateTimeFormat(element.createdDateTime)});
        dispatch({type: SET_ORDER_ID, orderId: orderId});
        dispatch({type: SET_ORDER_PRODUCTS, value: result.data.data});
    }
}

export const SetOrderProductsModal = (open) => {
    return (dispatch) => {
        dispatch({type: SET_ORDER_PRODUCTS_MODAL, open: open});
    }
}