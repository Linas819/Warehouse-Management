import axios from "axios";
import { SET_ERROR_MESSAGE, SET_ERROR_MODAL, SET_LOGIN, SET_USER_ID, SET_USER_ACCESS } from "./MainReducer";

export const accessRights = [
    {
        accessId: "inv001",
        accessName: "Inventory",
        pathname: "/warehouseinventory"
    },
    {
        accessId: "ord002",
        accessName: "Orders",
        pathname: "/orders"
    },
    {
        accessId: "reg003",
        accessName: "Register",
        pathname: "/register"
    },
]

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

export const RegisterUser = (user, history) => {
    return async (dispatch) => {
        let result = await axios.post(`user/register`, user)
        if(result.data.success)
        {
            history.push("/menu");
        } else {
            dispatch(SetErrorModal(true, result.data.message));
        }
    }
}

export const LoginAuthentication = (history) => {
    return async (dispatch, getstate) => {
        const { pathname } = history.location;
        const { isLoggedIn, userAccess } = getstate().main; 
        let token = sessionStorage.getItem('token');
        if(isLoggedIn && userAccess)
        {
            if(pathname === "/menu")
            {
                return;
            }
            else
            {
                const accessRightIndex = accessRights.findIndex(element => element.pathname === pathname);
                const userAccessIndex = userAccess.findIndex(element => element === accessRights[accessRightIndex].accessId);
                if(userAccessIndex > -1)
                {
                    return;
                } else {
                    history.push("/menu");
                }
            }
        } else if(!isLoggedIn && token) {
            dispatch(SetAuthorizationToken(token));
            let result = await axios.get(`user`);
            const { data } = result;
            dispatch(SetAuthorizationToken(data.token));
            dispatch({type: SET_LOGIN, value: true});
            dispatch({type: SET_USER_ID, value: data.userId});
            dispatch({type: SET_USER_ACCESS, value: data.userAccess});
            const accessRightIndex = accessRights.findIndex(element => element.pathname === pathname);
            const userAccessIndex = data.userAccess.findIndex(element => element === accessRights[accessRightIndex].accessId);
            if(userAccessIndex > -1)
                return;
            else
                history.push("/menu")
        } else 
            history.push("/");
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