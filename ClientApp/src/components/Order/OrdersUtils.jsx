import OrdersActionButtons from "./OrdersActionButtons";
import OrderProductDelete from "./OrderProducts/OrderProductDelete";
import AddressDeleteButton from "./AddressDeleteButton";


export const ordersListColumnDefs = [
    {headerName: "Order ID", field: "orderId", filter: true, sortable: true, flex: 1, resizable: true, floatingFilter: true},
    {headerName: "Address from", field: "addressFrom", filter: true, sortable: true, flex: 1, resizable: true, floatingFilter: true},
    {headerName: "Address to", field: "addressTo", filter: true, sortable: true, flex: 1, resizable: true, floatingFilter: true},
    {headerName: "Created by", field: "createdBy", filter: true, sortable: true, flex: 1, resizable: true, floatingFilter: true},
    {headerName: "Created date", field: "createdDateTime", filter: true, sortable: true, flex: 1, resizable: true, floatingFilter: true},
    {headerName: "Updated by", field: "updatedUserId", filter: true, sortable: true, flex: 1, resizable: true, floatingFilter: true},
    {headerName: "Updated date", field: "updateDateTime", filter: true, sortable: true, flex: 1, resizable: true, floatingFilter: true},
    {headerName: "Action", field: "action", flex: 2, resizable: true, cellStyle: {textAlign: "center"}, cellRenderer: OrdersActionButtons}
];

export const orderProductsColumnDefs = [
    {headerName: "Product ID", field: "productId", filter: true, sortable: true, flex: 1, resizable: true, floatingFilter: true},
    {headerName: "Quantity", field: "orderProductQuantity", filter: true, sortable: true, flex: 1, resizable: true, floatingFilter: true},
    {headerName: "Product name", field: "productName", filter: true, sortable: true, flex: 2, resizable: true, floatingFilter: true},
    {headerName: "Created by", field: "createdBy", filter: true, sortable: true, flex: 1, resizable: true, floatingFilter: true},
    {headerName: "Created date", field: "createdDateTime", filter: true, sortable: true, flex: 2, resizable: true, floatingFilter: true},
    {headerName: "Delete", field: "delete", flex: 1, resizable: true, cellRenderer: OrderProductDelete},
];

export const addressesColumnDefs = [
    {headerName: "Address ID", field: "addressId", filter: true, sortable: true, flex: 1, resizable: true, floatingFilter: true},
    {headerName: "Address Country", field: "addressCountry", filter: true, sortable: true, editable: true, flex: 2, resizable: true, floatingFilter: true},
    {headerName: "ZIP", field: "addressZipCode", filter: true, sortable: true, flex: 1, editable: true, resizable: true, floatingFilter: true},
    {headerName: "Region", field: "addressRegion", filter: true, sortable: true, flex: 1, editable: true, resizable: true, floatingFilter: true},
    {headerName: "City", field: "addressCity", filter: true, sortable: true, flex: 1, editable: true, resizable: true, floatingFilter: true},
    {headerName: "Street", field: "addressStreet", filter: true, sortable: true, flex: 1, editable: true, resizable: true, floatingFilter: true},
    {headerName: "House", field: "addressHouse", filter: true, sortable: true, flex: 1, editable: true, resizable: true, floatingFilter: true},
    {headerName: "Apartment", field: "addressApartment", filter: true, sortable: true, editable: true, flex: 1, resizable: true, floatingFilter: true},
    {headerName: "Created By", field: "createdBy", filter: true, sortable: true, flex: 1, resizable: true, floatingFilter: true},
    {headerName: "Created Date", field: "createdDateTime", filter: true, sortable: true, flex: 2, resizable: true, floatingFilter: true},
    {headerName: "Update By", field: "updateUserId", filter: true, sortable: true, flex: 1, resizable: true, floatingFilter: true},
    {headerName: "Update Date", field: "updateDateTime", filter: true, sortable: true, flex: 2, resizable: true, floatingFilter: true},
    {headerName: "Delete", field: "delete", flex: 1, resizable: true, floatingFilter: true, cellRenderer: AddressDeleteButton},
]