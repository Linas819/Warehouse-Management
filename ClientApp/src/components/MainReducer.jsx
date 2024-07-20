export const SET_LOGIN = "SET_LOGIN";
export const SET_BUTTON_LOADING = "SET_BUTTON_LOADING";
export const SET_USER_ID = "SET_USER_ID";
export const SET_USER_ACCESS = "SET_USER_ACCESS"

const initialState = {
    isLoggedIn: false,
    isButtonLoading: false,
    userId: "",
    userAccess: []
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
        case SET_USER_ID:
            state = {
                ...state,
                userId: action.value
            };
            break;
        case SET_USER_ACCESS:
            state = {
                ...state,
                userAccess: action.value
            };
            break;
        default:
            break;
    }
    return state;
}