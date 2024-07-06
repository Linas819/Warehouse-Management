import { configureStore } from '@reduxjs/toolkit';
import { mainReducer } from './MainReducer';
import { warehouseReducer } from './Warehouse/WarehouseReducer';
import { productCreateModalReducer } from './Warehouse/ProductCreateModal/ProductCreateModalReducer';
import { productViewModalReducer } from './Warehouse/ProductViewModal/ProductViewModalReducer';

const enhancers = [];

const isDevelopment = process.env.NODE_ENV === 'development';
if(isDevelopment && typeof window !== 'undefined' && window.__REDUX_DEVTOOLS_EXTENSION__)
    enhancers.push(window.__REDUX_DEVTOOLS_EXTENSION__());

export const store = configureStore({
    reducer: {
        main: mainReducer,
        warehouse: warehouseReducer,
        productCreateModal: productCreateModalReducer,
        productViewModal :productViewModalReducer
    },
    compose: enhancers
})