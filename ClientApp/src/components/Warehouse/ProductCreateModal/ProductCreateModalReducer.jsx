export const SET_PRODUCT_CREATE_MODAL = "SET_PRODUCT_CREATE_MODAL";

const initialState = {
    productCreateModalOpen: false
}

export const productCreateModalReducer = (state, action) => {
    state = state || initialState;
    switch(action.type){
        case SET_PRODUCT_CREATE_MODAL:
            state = {
                ...state,
                productCreateModalOpen: action.open
            };
            break;
        default:
            break;
    }
    return state;
}