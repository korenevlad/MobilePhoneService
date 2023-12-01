﻿$(document).ready(function () {
    $(".delete-button").click(function () {

        var itemId = $(this).data("id");

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
                    url: "/Cpu/Delete/" + itemId,
                    type: 'DELETE',
                    success: function (data) {
                        if (data.success === true) {
                            toastr["success"](data.message);
                            setTimeout(function () {
                                location.reload();
                            }, 1000);
                        }
                        else {
                            toastr["error"](data.message);
                        }
                    }
                });
            }
        })
    });
});