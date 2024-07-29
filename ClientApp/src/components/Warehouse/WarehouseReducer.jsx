export const SET_WAREHOUSE_DATA = "SET_WAREHOUSE_DATA";
export const SET_PRODUCT_CREATE_MODAL = "SET_PRODUCT_CREATE_MODAL";

const initialstate = {
    warehouseData: [],
    productCreateModal: false
}

export const warehouseReducer = (state, action) => {
    state = state || initialstate;
    switch(action.type){
        case SET_WAREHOUSE_DATA:
            state = {
                ...state,
                warehouseData: action.warehouseData
            };
            break;
        case SET_PRODUCT_CREATE_MODAL:
            state = {
                ...state,
                productCreateModal: action.open
            };
            break;
        default:
            break;
    }
    return state;
}