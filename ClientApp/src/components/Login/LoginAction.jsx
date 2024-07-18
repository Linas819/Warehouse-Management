import axios from 'axios';

export const LoginUser = (user) => {
    return async (dispatch) => {
        await axios.post(`user`, user);
    }
}