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
            text: "Заявка на ремонт будет удалена безвозвратно! Вместе с заявкой на ремонт будет удалена и её история!",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonText: 'Удалить',
            cancelButtonText: 'Отмена',
            reverseButtons: true
        }).then((result) => {
            if (result.isConfirmed) {
                $.ajax({
                    url: "/RequestForRepair/Delete/" + itemId,
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