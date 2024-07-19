import axios from 'axios';

export const LoginUser = (user, history) => {
    return async () => {
        let result = await axios.post(`user`, user);
        if(result.data.login)
        {
            sessionStorage.setItem('token', result.data.token);
            axios.defaults.headers.common['Authorization'] = "Bearer " + result.data.token;
            history.push("/menu");
        }
    }
}

export const LoginAuthentication = (history) => {
    return async (dispatch) => {
        let token = sessionStorage.getItem('token');
        if(token)
        {
            dispatch(SetAuthorizationToken(token));
            let result = await axios.get(`user`);
            if(result.data.login)
            {
                dispatch(SetAuthorizationToken(result.data.token));
                history.push(history.location.pathname);
            }
            else
                history.push("/");
        }
        else
            history.push("/");
    }
}

export const SetAuthorizationToken = (token) => {
    return () => {
        sessionStorage.setItem('token', token);
        axios.defaults.headers.common['Authorization'] = "Bearer " + token;
    }
}