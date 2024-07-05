export const SET_MODAL = "SET_MODAL";

const initialState = {
    productCreateModalOpen: false
}

export const productCreateModalReducer = (state, action) => {
    state = state || initialState;
    switch(action.type){
        case SET_MODAL:
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