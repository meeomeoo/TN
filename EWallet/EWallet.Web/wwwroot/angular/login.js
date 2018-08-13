$(document).ready(function () {
    $('#Email').on('paste', function (e) {
        $(this).val(e.originalEvent.clipboardData.getData('Text'));
    });
    //setCustomerCookie(undefined);
    var loginurl = '/Account/Login/';
    /* Login Form Submit
    * ======     
    */
    $('#createForm').submit(function (e) {
        e.preventDefault();
        var $this = $(this);
        
        if ($this.isValid()) {
            ShowLoading();

            var loginModel = $this.serializeObject();


            $.ajax({
                contentType: 'application/json; charset=utf-8',
                url: loginurl,
                data: JSON.stringify({ model: loginModel }),
                dataType: 'json',
                type: "POST",
                async: true,
                success: function (data) {
                    if (data.Status == "Fail") {
                        HideLoading();
                        swal({ title: data.Title, text: data.Message, type: "error", confirmButtonText: "Đóng" });
                    } else if (data.Status == "Success") {
                        HideLoading();
                        swal({ title: data.Title, text: data.Message, type: "success" });
                        window.location = ("/#" + data.ReturnUrl);
                    }
                }, error: function () {
                    HideLoading();
                    //Enable all button and input
                    swal({ title: "Không thể đăng nhập!", text: "Lỗi hệ thống, vui lòng thử lại hoặc thông báo cho chúng tôi biết!", type: "error", confirmButtonText: "Đóng" });
                }
            })
        }
    });

    $("#RememberMe").iCheck({
        checkboxClass: 'icheckbox_minimal-blue',
        increaseArea: '20%' // optional
    });

})