import axios from "axios";
import { SET_ORDERS_DATA } from "./OrderReducer";

export const GetOrdersData = () => {
    return async(dispatch) => {
        let result = await axios.get(`order`);
        dispatch({type: SET_ORDERS_DATA, ordersData: result.data.data});
    }
}