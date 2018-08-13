// api
var updateStatusAdvertisementUrl = '/Advertisement/UpdateStatus/';

var updateStatusAdvertisement = function (that, id, status) {
    Loading.Show();
    $.ajax({
        contentType: 'application/json; charset=utf-8',
        url: updateStatusAdvertisementUrl,
        data: { id: id, status: status },
        dataType: 'json',
        //type: "POST",
        cache: false,
        success: function (response) {
            if (response !== null) {
                if (response.code === statusCode.Success) {
                    showSuccessToastr(response.message);

                    if (response.data.status !== 5) {
                        if (response.data.status === 1) {
                            $('.button-pending').removeClass('hidden');
                            $('.button-available').addClass('hidden');
                        } else if (response.data.status === 2) {
                            $('.button-pending').addClass('hidden');
                            $('.button-available').removeClass('hidden');
                        }
                    } else {
                        window.location = "/advertisement/edit/" + id;
                    }

                    Loading.Hide();
                }

            } else {
                showErrorToastr(response.message);
            }
        }
    });
};

$(function () {

});