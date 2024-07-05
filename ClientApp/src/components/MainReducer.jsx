export const SET_LOGIN = "SET_LOGIN";
export const SET_BUTTON_LOADING = "SET_BUTTON_LOADING";

const initialState = {
    isLoggedIn: false,
    isButtonLoading: false
}

export const mainReducer = (state, action) => {
    state = state || initialState;
    switch(action.type){
        case SET_BUTTON_LOADING:
            state = {
                ...state,
                isButtonLoading: action.value
            };
            break;
        case SET_LOGIN:
            state = {
                ...state,
                isLoggedIn: action.value
            };
            break;
        default:
            break;
    }
    return state;
}