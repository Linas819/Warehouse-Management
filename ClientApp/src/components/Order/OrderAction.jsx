import axios from "axios";
import { SET_ORDERS_DATA, SET_ORDER_CREATE_MODAL, SET_ADDRESS_OPTIONS, SET_ADDRESS_CHANGE_MODAL,
    SET_ADDRESS_CHANGE_MODAL_ORDER_ID, SET_ADDRESSES_DATA, SET_ADDRESS_MODAL } from "./OrderReducer";
import { SetButtonLoading, SetDateTimeFormat, SetErrorModal } from "../MainAction";

export const GetOrdersData = () => {
    return async(dispatch) => {
        let result = await axios.get(`order`);
        result.data.data.map((element) => {
            return element.createdDateTime = SetDateTimeFormat(element.createdDateTime), 
                element.updatedDateTime = SetDateTimeFormat(element.updatedDateTime);
        });
        dispatch({type: SET_ORDERS_DATA, ordersData: result.data.data});
    }
}

export const GetAddresses = () => {
    return async(dispatch) => {
        let result = await axios.get(`order/address`);
        result.data.data.map((element) => {
            return element.createdDateTime = SetDateTimeFormat(element.createdDateTime), 
                element.updatedDateTime = SetDateTimeFormat(element.updatedDateTime);
        });
        dispatch({type: SET_ADDRESSES_DATA, addressData: result.data.data});
    }
}

export const SetAddressModal = (open) => {
    return(dispatch) => {
        dispatch({type: SET_ADDRESS_MODAL, open: open});
    }
}

export const DeleteOrderData = (orderId) => {
    return async(dispatch) => {
        dispatch(SetButtonLoading(true));
        let result = await axios.delete(`order`, {params: {orderId: orderId}});
        if(result.data.success)
        {
            dispatch(GetOrdersData());
        } else {
            dispatch(SetErrorModal(true, result.data.message));
        }
        dispatch(SetButtonLoading(false));
    }
}

export const PostOrder = (order) => {
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

export const PostAddress = (address) => {
    return async(dispatch, getstate) => {
        dispatch(SetButtonLoading(true));
        const { userId } = getstate().main;
        address.addressId = address.country.charAt(0) + address.zip.charAt(0) + address.region.charAt(0) + 
            address.city.charAt(0) + address.street.charAt(0) + address.house.charAt(0) + address.apartment.charAt(0);
        address.createdBy = userId;
        address.updatedBy = userId;
        const result = await axios.post(`order/address`, address);
        if(result.data.success)
        {
            dispatch(GetAddresses());
        } else {
            dispatch(SetErrorModal(true, result.data.message));
        }
        dispatch(SetButtonLoading(false));
    }
}
export const DeleteAddress = (addressId) => {
    return async(dispatch) => {
        dispatch(SetButtonLoading(true));
        const result = await axios.delete(`order/address`, {params: {addressId: addressId}});
        if(result.data.success)
        {
            dispatch(GetAddresses());
        } else {
            dispatch(SetErrorModal(true, result.data.message));
        }
        dispatch(SetButtonLoading(false));
    }
}

export const UpdateAddress = (address) => {
    return async(dispatch) => {
        const result = await axios.put(`order/address`, address);
        if(result.data.success)
        {
            dispatch(GetAddresses());
        } else {
            dispatch(SetErrorModal(true, result.data.message));
        }
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
                text: element.country + " " + element.city + " " +
                    element.street + " " + element.house,
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
        const result = await axios.put(`order/orderaddress`, payload);
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