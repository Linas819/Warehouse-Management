import OrdersActionButtons from "./OrdersActionButtons";


export const ordersListColumnDefs = [
    {headerName: "Order ID", field: "orderId", filter: true, sortable: true, flex: 1, resizable: true, floatingFilter: true},
    {headerName: "Address from", field: "addressFrom", filter: true, sortable: true, flex: 1, resizable: true, floatingFilter: true},
    {headerName: "Address to", field: "addressTo", filter: true, sortable: true, flex: 1, resizable: true, floatingFilter: true},
    {headerName: "Action", field: "action", flex: 1, resizable: true, cellStyle: {textAlign: "center"}, cellRenderer: OrdersActionButtons}
];