$(document).ready(function () {

    //Update image
    $("#fileUpload").on('change', function () {
        if ($('#fileUpload').prop('files').length > 0) {
            $("#formUploadImage").submit();
        }
    });

    $("#formUploadImage").on('submit', function (e) {
        e.preventDefault();
        //Lấy ra files
        var file_data = $('#fileUpload').prop('files')[0];
        //lấy ra kiểu file
        var type = file_data.type;
        //Xét kiểu file được upload
        var match = ["image/gif", "image/png", "image/jpg", "image/jpeg"];
        //kiểm tra kiểu file
        if (type == match[0] || type == match[1] || type == match[2] || type == match[3]) {
            //khởi tạo đối tượng form data và thêm files vào trong form data
            var form_data = new FormData(this);
            
            //sử dụng ajax post
            $.ajax({
                url: '/Upload/UploadImage',
                dataType: 'text',
                cache: false,
                contentType: false,
                processData: false,
                data: form_data,
                type: 'POST',
                beforeSend: function () {
                    Loading.Show();
                },
                complete: function () {
                    Loading.Hide();
                },
                success: function (res) {
                    if (res.success) {
                        $('#fileUpload').val('');
                        location.reload();
                    }
                    else {
                        $('#fileUpload').val('');
                        showErrorToastr(res.message);
                    }
                }
            });
        } else {
            showWarningToastr(ewallet.message.image_type_invalid);
            $('#fileUpload').val('');
        }
        return false;
    });

    $("#btnUpdateIdNumber").on('click', function () {
        var fullName = $("#txtFullName").val();
        var idNumber = $("#txtIdNumber").val();
        if (fullName.length === 0 || idNumber.length === 0) {
            showWarningToastr(profiler_text.must_be_input_fullname_and_idnumber)
            return false;
        }
        else {
            $.ajax({
                type: "POST",
                url: profile_url.update_id_number,
                data: {
                    fullName: fullName,
                    idNumber: idNumber
                },
                success: function (res) {
                    if (res.success) {
                        location.reload();
                    }
                    else {
                        var message = res.message;
                        showErrorToastr(message);
                    }
                },
                error: function (err) {
                    showErrorToastr(ewallet.message.request_fail);
                }
            });
        }

    });

    if ($("#txtIdNumber").length > 0) {
        if ($("#txtIdNumber").val().length === 0) {
            $("#myModal").modal();
        }
    }
})
