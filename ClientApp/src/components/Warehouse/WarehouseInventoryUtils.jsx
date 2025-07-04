import WarehouseActionButtons from "./WarehouseActionButtons";

export const warehouseInventoryColumnDefs = [
    { headerName: "Product ID", field: "productId", filter: true, sortable: true, flex: 1, resizable: true, floatingFilter: true},
    { headerName: "Product name", field: "name", filter: true, editable: true,  sortable: true, flex: 1, resizable: true, floatingFilter: true },
    { headerName: "Product EAN", field: "ean", filter: true, editable: true,  sortable: true, flex: 1, resizable: true, floatingFilter: true },
    { headerName: "Product Type", field: "type", filter: true, editable: true,  sortable: true, flex: 1, resizable: true, floatingFilter: true },
    { headerName: "Product Weight (kg)", field: "weight", filter: true, editable: true,  sortable: true, flex: 1, resizable: true, floatingFilter: true },
    { headerName: "Product Price ($)", field: "price", filter: true, editable: true,  sortable: true, flex: 1, resizable: true, floatingFilter: true },
    { headerName: "Product Quantity", field: "quantity", filter: true, editable: true,  sortable: true, flex: 1, resizable: true, floatingFilter: true },
    { headerName: "Action", field: "Action", flex: 1, resizable: true, cellRenderer: WarehouseActionButtons, cellStyle: {textAlign: "center"} },
];