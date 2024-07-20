export const SET_LOGIN = "SET_LOGIN";
export const SET_BUTTON_LOADING = "SET_BUTTON_LOADING";
export const SET_USER_ID = "SET_USER_ID";
export const SET_USER_ACCESS = "SET_USER_ACCESS";
export const SET_ERROR_MODAL = "SET_ERROR_MODAL";
export const SET_ERROR_MESSAGE = "SET_ERROR_MESSEGE";

const initialState = {
    isLoggedIn: false,
    isButtonLoading: false,
    userId: "",
    userAccess: [],
    errorMessage: "",
    errorModal: false
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
        case SET_ERROR_MODAL:
            state = {
                ...state,
                errorModal: action.open
            };
            break;
        case SET_ERROR_MESSAGE:
            state = {
                ...state,
                errorMessage: action.value
            };
            break;
        default:
            break;
    }
    return state;
}