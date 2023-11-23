var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblDataManufacturer').DataTable({
        "ajax": { url: '/manufacturer/getall' },
        "columns": [
            { data: 'manufacturer_id', "width": "10%" },
            { data: 'manufacturer_name', "width": "30%" },
            { data: 'country', "width": "30%" },
            {
                data: 'manufacturer_id',
                "render": function (data) {
                    return `<div class="w-75 btn-group" role="group">
                    <a href="/manufacturer/upsert?manufacturer_id=${data}" class="btn btn-primary form-control mx-2"><i class="bi bi-pencil-fill"></i > &nbsp; Обновить</a>
                    <a onClick=Delete('manufacturer/delete/${data}') class="btn btn-danger form-control"><i class="bi bi-trash3"></i > &nbsp; Удалить</a>
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
        text: "Производитель будет удален безвозвратно!",
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