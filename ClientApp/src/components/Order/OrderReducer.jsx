export const SET_ORDERS_DATA = "SET_ORDER_DATA";
export const SET_ADDRESSES_DATA = "SET_ADDRESSES_DATA";
export const SET_ORDER_PRODUCTS_DATA = "SET_ORDER_PRODUCTS_DATA";
export const SET_ORDER_CREATE_MODAL = "SET_ORDER_CREATE_MODAL";
export const SET_ADDRESS_OPTIONS = "SET_ADDRESS_OPTIONS";
export const SET_ADDRESS_CHANGE_MODAL =  "SET_ADDRESS_CHANGE_MODAL";
export const SET_ADDRESS_CHANGE_MODAL_ORDER_ID =  "SET_ADDRESS_CHANGE_MODAL_ORDER_ID";
export const SET_ADDRESS_MODAL =  "SET_ADDRESS_MODAL";

const initialstate = {
    ordersData: [],
    addressData: [],
    orderProductsData: [],
    orderCreateModalOpen: false,
    addressOptions: [],
    orderAddressChangeModalOpen: false,
    orderAddressChangeModalOrderId: false,
    addressModalOpen: false
}

export const ordersReducer = (state, action) => {
    state = state || initialstate;
    switch(action.type){
        case SET_ORDERS_DATA:
            state = {
                ...state,
                ordersData: action.ordersData
            };
            break;
        case SET_ADDRESSES_DATA:
            state = {
                ...state,
                addressData: action.addressData
            };
            break;
        case SET_ORDER_PRODUCTS_DATA:
            state = {
                ...state,
                orderProductsData: action.orderProductsData
            };
            break;
        case SET_ORDER_CREATE_MODAL:
            state = {
                ...state,
                orderCreateModalOpen: action.open
            };
            break;
        case SET_ADDRESS_OPTIONS:
            state = {
                ...state,
                addressOptions: action.options
            };
            break;
        case SET_ADDRESS_CHANGE_MODAL:
            state = {
                ...state,
                orderAddressChangeModalOpen: action.open
            };
            break;
        case SET_ADDRESS_CHANGE_MODAL_ORDER_ID:
            state = {
                ...state,
                orderAddressChangeModalOrderId: action.orderId
            };
            break;
        case SET_ADDRESS_MODAL:
            state = {
                ...state,
                addressModalOpen: action.open
            };
            break;
        default:
            break;
    }
    return state;
}