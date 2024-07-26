export const SET_ORDER_PRODUCTS = "SET_ORDER_PRODUCTS";
export const SET_ORDER_PRODUCTS_MODAL = "SET_ORDER_PRODUCTS_MODAL";
export const SET_ORDER_ID = "SET_ORDER_ID";

const initialstate = {
    orderProducts: [],
    orderProductsModalOpen: false,
    orderId: ""
}

export const orderProductsModalReducer = (state, action) => {
    state = state || initialstate;
    switch(action.type){
        case SET_ORDER_PRODUCTS:
            state = {
                ...state,
                orderProducts: action.value
            };
            break;
        case SET_ORDER_PRODUCTS_MODAL:
            state = {
                ...state,
                orderProductsModalOpen: action.open
            };
            break;
        case SET_ORDER_ID:
            state = {
                ...state,
                orderId: action.orderId
            };
            break;
        default:
            break;
    }
    return state;
}