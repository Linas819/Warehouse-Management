import axios from "axios";
import { SET_ORDERS_DATA } from "./OrderReducer";
import { SetErrorModal } from "../MainAction";

export const GetOrdersData = () => {
    return async(dispatch) => {
        let result = await axios.get(`order`);
        result.data.data.map((element) => {
            const changeDate = new Date(element.createdDateTime);
            const formatedUpdateDate = changeDate.getFullYear() + "-" + (changeDate.getMonth()+1) + "-" + changeDate.getDate() 
                + " " + changeDate.getHours() + ":" + changeDate.getMinutes() + ":" + changeDate.getSeconds();
            element.createdDateTime = formatedUpdateDate;
        })
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