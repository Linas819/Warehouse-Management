import { configureStore } from '@reduxjs/toolkit';
import { mainReducer } from './MainReducer';
import { warehouseReducer } from './Warehouse/WarehouseReducer';
import { productViewModalReducer } from './Warehouse/ProductViewModal/ProductViewModalReducer';
import { ordersReducer } from './Order/OrderReducer';
import { orderProductsModalReducer } from './Order/OrderProducts/OrderProductsReducer';

const enhancers = [];

const isDevelopment = process.env.NODE_ENV === 'development';
if(isDevelopment && typeof window !== 'undefined' && window.__REDUX_DEVTOOLS_EXTENSION__)
    enhancers.push(window.__REDUX_DEVTOOLS_EXTENSION__());

export const store = configureStore({
    reducer: {
        main: mainReducer,
        warehouse: warehouseReducer,
        productViewModal :productViewModalReducer,
        orders: ordersReducer,
        orderProducts: orderProductsModalReducer
    },
    compose: enhancers
})