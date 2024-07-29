export const SET_PRODUCT_VIEW_MODAL = "SET_PRODUCT_VIEW_MODAL";
export const SET_PRODUCT_VIEW_MODAL_HEADER = "SET_MODAL_HEADER";
export const SET_PRODUCT_VIEW_MODAL_CONTENT = "SET_MODAL_PRODUCT_VIEW";
export const SET_PRODUCT_VIEW_PRICE_HISTORY = "SET_PRODUCT_VIEW_PRICE_HISTORY";
export const SET_PRODUCT_VIEW_QUANTITY_HISTORY = "SET_PRODUCT_VIEW_QUANTITY_HISTORY";
export const SET_INITIAL_STATE = "SET_INITIAL_STATE";

const initialState = {
    productViewModal: false,
    productViewModalHeader: "",
    productViewContent: {},
    productPriceHistory: [],
    productQuantityHistory: []
}

export const productViewModalReducer = (state, action) => {
    state = state || initialState;
    switch(action.type){
        case SET_PRODUCT_VIEW_MODAL:
            state = {
                ...state,
                productViewModal: action.open
            };
            break;
        case SET_PRODUCT_VIEW_MODAL_HEADER:
            state = {
                ...state,
                productViewModalHeader: action.header
            };
            break;
        case SET_PRODUCT_VIEW_MODAL_CONTENT:
            state = {
                ...state,
                productViewContent: action.product
            };
            break;
        case SET_PRODUCT_VIEW_PRICE_HISTORY:
            state = {
                ...state,
                productPriceHistory: action.priceHistory
            };
            break;
        case SET_PRODUCT_VIEW_QUANTITY_HISTORY:
            state = {
                ...state,
                productQuantityHistory: action.quantityHistory
            };
            break;
        case SET_INITIAL_STATE:
            state = initialState;
            break;
        default:
            break;
    }
    return state
}