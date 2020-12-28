
var DeleteID = 0;
var DeleteUrl = '';

function Success(data) {
    if (data.success) {
    $('#mess').removeClass("alert-danger").addClass("alert-success");

        my_success();
        table.ajax.reload();
    }
    else $('#mess').removeClass("alert-success").addClass("alert-danger");

    $('#mess').html(data.message);
    $('#toast').toast({delay: 3000 });
    $('#toast').toast('show');
}

function BeginDelete(id, url) {
    DeleteID = id;
    DeleteUrl = url;
    $('#ConfirmModal').modal('show');
}

function Delete() {
    $.ajax({
        type: 'POST',
        url: DeleteUrl,
        data: JSON.stringify({
            id: DeleteID
        }),
        contentType: 'application/json; charset=utf-8',
        success: function (data) {
            if (data.success) {
                $('#mess').removeClass("alert-danger").addClass("alert-success");
                table.ajax.reload();
            }
            else $('#mess').removeClass("alert-success").addClass("alert-danger");

            $('#mess').html(data.message);
            $('#toast').toast({ delay: 5000 });
            $('#toast').toast('show');
        }
    });
}
