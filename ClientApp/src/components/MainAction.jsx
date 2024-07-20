import axios from "axios";
import { SET_ERROR_MESSAGE, SET_ERROR_MODAL, SET_LOGIN, SET_USER_ID, SET_USER_ACCESS } from "./MainReducer";

export const LoginUser = (user, history) => {
    return async (dispatch) => {
        let result = await axios.post(`user`, user);
        if(result.data.login)
        {
            dispatch(SetAuthorizationToken(result.data.token));
            dispatch({type: SET_LOGIN, value: true});
            dispatch({type: SET_USER_ID, value: result.data.userId});
            dispatch({type: SET_USER_ACCESS, value: result.data.userAccess});
            history.push("/menu");
        } else {
            dispatch(SetErrorModal(true, "User not found"))
        }
    }
}

export const LoginAuthentication = (history) => {
    return async (dispatch, getstate) => {
        const { isLoggedIn } = getstate().main; 
        let token = sessionStorage.getItem('token');
        if(!isLoggedIn && token)
        {
            dispatch(SetAuthorizationToken(token));
            let result = await axios.get(`user`);
            if(result.data.login)
            {
                dispatch(SetAuthorizationToken(result.data.token));
                dispatch({type: SET_LOGIN, value: true});
                dispatch({type: SET_USER_ID, value: result.data.userId});
                dispatch({type: SET_USER_ACCESS, value: result.data.userAccess});
                history.push(history.location.pathname);
            }
            else
                history.push("/");
        }
    }
}

export const SetAuthorizationToken = (token) => {
    return () => {
        sessionStorage.setItem('token', token);
        axios.defaults.headers.common['Authorization'] = "Bearer " + token;
    }
}

export const SetErrorModal = (open, message) => {
    return (dispatch) => {
        dispatch({type: SET_ERROR_MESSAGE, value: message});
        dispatch({type: SET_ERROR_MODAL, open: open});
    }
}