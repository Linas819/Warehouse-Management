export const SET_PRODUCT_VIEW_MODAL = "SET_PRODUCT_VIEW_MODAL";
export const SET_PRODUCT_VIEW_MODAL_HEADER = "SET_MODAL_HEADER";
export const SET_PRODUCT_VIEW_MODAL_CONTENT = "SET_MODAL_PRODUCT_VIEW";
export const SET_INITIAL_STATE = "SET_INITIAL_STATE";

const initialState = {
    productViewModalOpen: false,
    productViewModalHeader: "",
    productViewContent: {}
}

export const productViewModalReducer = (state, action) => {
    state = state || initialState;
    switch(action.type){
        case SET_PRODUCT_VIEW_MODAL:
            state = {
                ...state,
                productViewModalOpen: action.open
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
        case SET_INITIAL_STATE:
            state = initialState;
            break;
        default:
            break;
    }
    return state
}