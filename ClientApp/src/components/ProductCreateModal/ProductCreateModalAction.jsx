import axios from 'axios';
import { SET_MODAL } from './ProductCreateModalReducer'

export const SetProductCreateModal = (open) => {
    return (dispatch) => {
        dispatch({type: SET_MODAL, open: open});
    }
}