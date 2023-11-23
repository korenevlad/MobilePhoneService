var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblDataCpu').DataTable({
        "ajax": { url: '/cpu/getall' },
        "columns": [
            { data: 'cpu_id', "width": "10%" },
            { data: 'model', "width": "20%" },
            { data: 'amount_cernels', "width": "20%" },
            { data: 'frequency', "width": "20%" },
            {
                data: 'cpu_id',
                "render": function (data) {
                    return `<div class="w-75 btn-group" role="group">
                    <a href="/cpu/upsert?cpu_id=${data}" class="btn btn-primary form-control mx-2"><i class="bi bi-pencil-fill"></i > &nbsp; Обновить</a>
                    <a onClick=Delete('cpu/delete/${data}') class="btn btn-danger form-control"><i class="bi bi-trash3"></i > &nbsp; Удалить</a>
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
        text: "Процессор будет удален безвозвратно!",
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