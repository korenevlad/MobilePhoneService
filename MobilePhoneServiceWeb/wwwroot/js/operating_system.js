﻿var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblDataOperatingSystem').DataTable({
        "ajax": { url: '/operatingsystem/getall' },
        "columns": [
            { data: 'operating_system_name', "width": "35%" },
            { data: 'operating_system_version', "width": "35%" },
            {
                data: 'operating_system_id',
                "render": function (data) {
                    return `<div class="w-75 btn-group" role="group">
                    <a href="/operatingsystem/upsert?operating_system_id=${data}" class="btn btn-primary form-control mx-2"><i class="bi bi-pencil-fill"></i > &nbsp; Обновить</a>
                    <a onClick=Delete('operatingsystem/delete/${data}') class="btn btn-danger form-control"><i class="bi bi-trash3"></i > &nbsp; Удалить</a>
                    </div>`
                },
                "width": "30%"
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
        text: "Категория будет удалена безвозвратно!",
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