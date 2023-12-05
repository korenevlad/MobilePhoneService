$(document).ready(function () {
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
            text: "Клиент будет удален безвозвратно!",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonText: 'Удалить',
            cancelButtonText: 'Отмена',
            reverseButtons: true
        }).then((result) => {
            if (result.isConfirmed) {
                $.ajax({
                    url: "/Client/Delete/" + itemId,
                    type: 'DELETE',
                    success: async function (data) {
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