export const SET_ORDER_PRODUCTS = "SET_ORDER_PRODUCTS";
export const SET_ORDER_PRODUCTS_MODAL = "SET_ORDER_PRODUCTS_MODAL";
export const SET_ORDER_ID = "SET_ORDER_ID";
export const SET_DROPDOWN_PRODUCT_OPTIONS = "SET_DROPDOWN_PRODUCT_OPTIONS";

const initialstate = {
    orderProducts: [],
    orderProductsModal: false,
    orderId: "",
    dropdownProductsOptions: []
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
                orderProductsModal: action.open
            };
            break;
        case SET_ORDER_ID:
            state = {
                ...state,
                orderId: action.orderId
            };
            break;
        case SET_DROPDOWN_PRODUCT_OPTIONS:
            state = {
                ...state,
                dropdownProductsOptions: action.value
            };
            break;
        default:
            break;
    }
    return state;
}