var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblDataPhoneSpecification').DataTable({
        "ajax": { url: '/phonespecification/getall' },
        "columns": [
            { data: 'ram', "width": "10%" },
            { data: 'internal_memory', "width": "10%" },
            { data: 'screen_size', "width": "10%" },
            {
                data: null,
                render: function (row) {
                    return row.cpu_of_specification.amount_cernels;
                },
                "width": "11%"
            },
            {
                data: null,
                render: function (data, type, row) {
                    return row.cpu_of_specification.frequency;
                },
                "width": "11%"
            },
            {
                data: null,
                render: function (row) {
                    return row.operating_system_of_specification.operating_system_name;
                },
                "width": "11%"
            },
            {
                data: null,
                render: function (data, type, row) {
                    return row.operating_system_of_specification.operating_system_version;
                },
                "width": "11%"
            },
            {
                data: 'specification_id',
                "render": function (data) {
                    return `<div class="w-75 btn-group" role="group">
                    <a href="/phonespecification/upsert?specification_id=${data}" class="btn btn-primary mx-2"><i class="bi bi-pencil-fill"></i > &nbsp; Обновить</a>
                    <a onClick=Delete('/phonespecification/delete/${data}') class="btn btn-danger mx-2"><i class="bi bi-trash3"></i > &nbsp; Удалить</a>
                    </div>`
                },
                "width": "20%"
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
        text: "Спецификация будет удалена безвозвратно!",
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
                },
                error: function (data) {
                    toastr.error(data.message);
                }
            })
        }
    })
}