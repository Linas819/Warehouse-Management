import OrdersActionButtons from "./OrdersActionButtons";


export const ordersListColumnDefs = [
    {headerName: "Order ID", field: "orderId", filter: true, sortable: true, flex: 1, resizable: true, floatingFilter: true},
    {headerName: "Address from", field: "addressFrom", filter: true, sortable: true, flex: 1, resizable: true, floatingFilter: true},
    {headerName: "Address to", field: "addressTo", filter: true, sortable: true, flex: 1, resizable: true, floatingFilter: true},
    {headerName: "Created by", field: "createdBy", filter: true, sortable: true, flex: 1, resizable: true, floatingFilter: true},
    {headerName: "Created date", field: "createdDate", filter: true, sortable: true, flex: 1, resizable: true, floatingFilter: true},
    {headerName: "Action", field: "action", flex: 1, resizable: true, cellStyle: {textAlign: "center"}, cellRenderer: OrdersActionButtons}
];

export const orderProductsColumnDefs = [
    {headerName: "Product ID", field: "productId", filter: true, sortable: true, flex: 1, resizable: true, floatingFilter: true},
    {headerName: "Quantity", field: "orderProductQuantity", filter: true, sortable: true, flex: 1, resizable: true, floatingFilter: true},
    {headerName: "Product name", field: "productName", filter: true, sortable: true, flex: 2, resizable: true, floatingFilter: true},
    {headerName: "Created by", field: "createdBy", filter: true, sortable: true, flex: 1, resizable: true, floatingFilter: true},
    {headerName: "Created date", field: "createdDate", filter: true, sortable: true, flex: 2, resizable: true, floatingFilter: true},
    {headerName: "Delete", field: "delete", flex: 1, resizable: true},
]