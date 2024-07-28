import axios from "axios";
import { SET_ORDERS_DATA, SET_ORDER_CREATE_MODAL, SET_ADDRESS_OPTIONS, SET_ADDRESS_CHANGE_MODAL, SET_ADDRESS_CHANGE_MODAL_ORDER_ID } from "./OrderReducer";
import { SetButtonLoading, SetDateTimeFormat, SetErrorModal } from "../MainAction";

export const GetOrdersData = () => {
    return async(dispatch) => {
        let result = await axios.get(`order`);
        result.data.data.map((element) => {
            return element.createdDateTime = SetDateTimeFormat(element.createdDateTime), 
                element.updateDateTime = SetDateTimeFormat(element.updateDateTime);
        });
        dispatch({type: SET_ORDERS_DATA, ordersData: result.data.data});
    }
}

export const DeleteOrderData = (orderId) => {
    return async(dispatch) => {
        dispatch(SetButtonLoading(true));
        let result = await axios.delete(`order/${orderId}`);
        if(result.data.success)
        {
            dispatch(GetOrdersData());
        } else {
            dispatch(SetErrorModal(true, result.data.message));
        }
        dispatch(SetButtonLoading(false));
    }
}

export const PostNewOrder = (order) => {
    return async(dispatch) => {
        dispatch(SetButtonLoading(true));
        const result = await axios.post(`order`, order);
        if(result.data.success)
        {
            dispatch(GetOrdersData());
            dispatch(SetOrderCreateModal(false))
        } else {
            dispatch(SetErrorModal(true, result.data.message));
        }
        dispatch(SetButtonLoading(false));
    }
}

export const SetOrderCreateModal = (open) => {
    return(dispatch) => {
        dispatch({type: SET_ORDER_CREATE_MODAL, open: open});
    }
}

export const GetAddressOptions = () => {
    return async(dispatch) => {
        const result = await axios.get(`order/address`);
        const options = result.data.data.map((element) => {
            return {
                key: element.addressId,
                text: element.addressCountry + " " + element.addressCity + " " +
                    element.addressStreet + " " + element.addressHouse,
                value: element.addressId
            }
        });
        dispatch({type: SET_ADDRESS_OPTIONS, options: options});
    }
}

export const SetOrderAddressChangeModal = (open, orderId) => {
    return(dispatch) => {
        dispatch({type: SET_ADDRESS_CHANGE_MODAL, open: open});
        dispatch({type: SET_ADDRESS_CHANGE_MODAL_ORDER_ID, orderId: orderId});
    }
}

export const UpdateOrderAddress = (addresses, orderId) => {
    return async(dispatch) => {
        dispatch(SetButtonLoading(true));
        const payload = {
            orderId: orderId,
            addressFrom: addresses.addressFrom,
            addressTo: addresses.addressTo
        };
        const result = await axios.put(`order/address`, payload);
        if(result.data.success)
        {
            dispatch(GetOrdersData());
            dispatch(SetOrderAddressChangeModal(false))
        } else {
            dispatch(SetErrorModal(true, result.data.message));
        }
        dispatch(SetButtonLoading(false));
    }
}