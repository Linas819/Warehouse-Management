import axios from "axios";
import { SET_ORDERS_DATA } from "./OrderReducer";
import { SetDateTimeFormat, SetErrorModal } from "../MainAction";

export const GetOrdersData = () => {
    return async(dispatch) => {
        let result = await axios.get(`order`);
        result.data.data.map((element) => {element.createdDateTime = SetDateTimeFormat(element.createdDateTime)});
        dispatch({type: SET_ORDERS_DATA, ordersData: result.data.data});
    }
}

export const DeleteOrderData = (orderId) => {
    return async(dispatch) => {
        let result = await axios.delete(`order/${orderId}`);
        if(result.data.success)
            {
                dispatch(GetOrdersData());
            } else {
                dispatch(SetErrorModal(true, result.data.message));
            }
    }
}