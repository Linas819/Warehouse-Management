export const SET_ORDERS_DATA = "SET_ORDER_DATA";
export const SET_ADDRESSES_DATA = "SET_ADDRESSES_DATA";
export const SET_ORDER_PRODUCTS_DATA = "SET_ORDER_PRODUCTS_DATA";

const initialstate = {
    ordersData: [],
    addressData: [],
    orderProductsData: []
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
        default:
            break;
    }
    return state;
}