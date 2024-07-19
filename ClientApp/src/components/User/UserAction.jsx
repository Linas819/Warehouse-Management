import axios from 'axios';

export const LoginUser = (user, history) => {
    return async (dispatch) => {
        let result = await axios.post(`user`, user);
        if(result.data.user.login)
        {
            sessionStorage.setItem('token', result.data.user.token);
            axios.defaults.headers.common['Authorization'] = "Bearer " + result.data.user.token;
            history.push("/menu");
        }
    }
}