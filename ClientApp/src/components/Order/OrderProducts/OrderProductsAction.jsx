import axios from "axios";
import { SET_ORDER_ID, SET_ORDER_PRODUCTS, SET_ORDER_PRODUCTS_MODAL } from "./OrderProductsReducer";

export const GetOrderProducts = (orderId) => {
    return async(dispatch) => {
        const result = await axios.get(`order/products`, {params: {orderId: orderId}});
        result.data.data.map((element) => {
            const changeDate = new Date(element.createdDate);
            const formatedUpdateDate = changeDate.getFullYear() + "-" + (changeDate.getMonth()+1) + "-" + changeDate.getDate() 
                + " " + changeDate.getHours() + ":" + changeDate.getMinutes() + ":" + changeDate.getSeconds();
            element.createdDate = formatedUpdateDate;
        });
        dispatch({type: SET_ORDER_ID, orderId: orderId});
        dispatch({type: SET_ORDER_PRODUCTS, value: result.data.data});
    }
}

export const SetOrderProductsModal = (open) => {
    return (dispatch) => {
        dispatch({type: SET_ORDER_PRODUCTS_MODAL, open: open});
    }
}