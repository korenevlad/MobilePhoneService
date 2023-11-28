var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblDataPhoneModel').DataTable({
        "ajax": { url: '/phonemodel/getall' },
        "columns": [
            { data: 'model_id', "width": "10%" },
            {
                data: null,
                render: function (row) {
                    return row.manufacturer_of_phone_model.manufacturer_name;
                },
                "width": "13%"
            },
            { data: 'name', "width": "13%" },
            { data: 'year_of_release', "width": "13%" },
            {
                data: null,
                render: function (row) {
                    return row.manufacturer_of_phone_model.country;
                },
                "width": "13%"
            },
            {
                data: 'specification_id',
                "render": function (data) {
                    return `<div class="pt-2 container-fluid center-grid" role="group">
                        <a href="/phonemodel/specificationview/${data}" class="btn btn-outline-info mx-2 btn-block form-control"><i class="bi bi-arrow-up-right"></i></a>
                    </div>`
                },
                "width": "15%"
            },
            {
                data: 'model_id',
                "render": function (data) {
                    return `<div class="w-75 btn-group" role="group">
                    <a href="/phonemodel/upsert?model_id=${data}" class="btn btn-primary mx-2"><i class="bi bi-pencil-fill"></i > &nbsp; Обновить</a>
                    <a onClick=Delete('/phonemodel/delete/${data}') class="btn btn-danger mx-2"><i class="bi bi-trash3"></i > &nbsp; Удалить</a>
                    </div>`
                },
                "width": "25%"
            }
        ]
    });
}



function Delete(url) {
    const swalWithBootstrapButtons = Swal.mixin({
        customClass: {
            confirmButton: 'btn btn-success',
            cancelButton: 'btn btn-danger'
        },
        buttonsStyling: false
    })

    swalWithBootstrapButtons.fire({
        title: 'Вы уверены?',
        text: "Модель телефона будет удалена безвозвратно!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonText: 'Удалить',
        cancelButtonText: 'Отмена',
        reverseButtons: true
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: url,
                type: 'DELETE',
                success: function (data) {
                    dataTable.ajax.reload();
                    toastr.success(data.message);
                }
            })
        }
    })
}