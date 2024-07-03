export const SET_WAREHOUSE_DATA = "SET_WAREHOUSE_DATA";

const initialstate = {
    warehouseData: {}
}

export const warehouseReducer = (state, action) => {
    state = state || initialstate;
    switch(action.type){
        case SET_WAREHOUSE_DATA:
            state = {
                ...state,
                warehouseData: action.value
            };
            break;
    }
    return state;
}