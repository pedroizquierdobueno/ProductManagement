$(function () {
    var l = abp.localization.getResource("ProductManagement");

    var dataTable = $("#ProductsTable").DataTable(
        abp.libs.datatables.normalizeConfiguration({
            serverSide: true,
            paging: true,
            order: [[0, "asc"]],
            searching: false,
            scrollX: true,
            ajax: abp.libs.datatables.createAjax(productManagement.products.product.getList),
            columnDefs: [
                {
                    title: l("Name"),
                    data: "name"
                },
                {
                    title: l("CatagoryName"),
                    data: "categoryName",
                    orderable: false
                },
                {
                    title: l("Price"),
                    data: "price"
                },
                {
                    title: l("StockState"),
                    data: "stockState",
                    render: function (data) {
                        return l("Enum:StockState:" + data);
                    }
                },
                {
                    title: l("CreationTime"),
                    data: "creationTime",
                    dataFormat: "date"
                },
                {
                    title: l("Actions"),
                    rowAction: {
                        items: [
                            {
                                text: l("Edit"),
                                action: function (data) {
                                    editModal.open({ id: data.record.id });
                                }
                            }
                        ]
                    }
                }
            ]
        })
    );

    var createModal = new abp.ModalManager(abp.appPath + "Products/CreateProductModal");

    createModal.onResult(function () {
        dataTable.ajax.reload();
    });

    $("#NewProductButton").click(function (e) {
        e.preventDefault();
        createModal.open();
    });

    var editModal = new abp.ModalManager(abp.appPath + "Products/EditProductModal");

    editModal.onResult(function () {
        dataTable.ajax.reload();
    });
});