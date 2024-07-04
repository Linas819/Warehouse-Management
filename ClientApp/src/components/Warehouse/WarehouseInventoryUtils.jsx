import WarehouseActionButtons from "./WarehouseActionButtons";

export const warehouseInventoryColumnDefs = [
    { headerName: "Product ID", field: "productId", filter: true, sortable: true, flex: 1, resizable: true, floatingFilter: true},
    { headerName: "Product name", field: "productName", filter: true, sortable: true, flex: 1, resizable: true, floatingFilter: true },
    { headerName: "Product EAN", field: "productEan", filter: true, sortable: true, flex: 1, resizable: true, floatingFilter: true },
    { headerName: "Product Type", field: "productType", filter: true, sortable: true, flex: 1, resizable: true, floatingFilter: true },
    { headerName: "Product Weight", field: "productWeight", filter: true, sortable: true, flex: 1, resizable: true, floatingFilter: true },
    { headerName: "Product Price", field: "productPrice", filter: true, sortable: true, flex: 1, resizable: true, floatingFilter: true },
    { headerName: "Product Quantity", field: "productQuantity", filter: true, sortable: true, flex: 1, resizable: true, floatingFilter: true },
    { headerName: "Action", field: "Action", flex: 1, resizable: true, cellRenderer: WarehouseActionButtons, cellStyle: {textAlign: "center"} },
];