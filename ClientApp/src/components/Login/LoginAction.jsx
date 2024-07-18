import axios from 'axios';

export const LoginUser = (user, history) => {
    return async (dispatch) => {
        await axios.post(`user`, user);
        history.push("/warehouseinventory")
    }
}