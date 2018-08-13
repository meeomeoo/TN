$(document).ready(function () {

    var gotoStep = function (step) {
        if (step === 1) {
            //Set title 
            $(".js-title-step").text(profiler_text.input_phone_number);

            //Show/hide button
            $("#btnPrevious").hide();
            $("#btnNext").show();

            //Show/Hide Input
            $("#divInputPhone").show();
            $("#divInputOTP").hide();
        }
        else if (step === 2) {
            //Set title 
            $(".js-title-step").text(profiler_text.input_phone_verify_code);

            //Show/hide button
            $("#btnPrevious").show();
            $("#btnNext").hide();

            //Show/Hide Input
            $("#divInputPhone").hide();
            $("#divInputOTP").show();
        }
    };

    $("#btnShowPhoneVerifyPhone").on('click', function () {
        gotoStep(1);
    });

    $("#btnNext").on('click', function (e) {
        var phoneNumber = $("#phone_number").val();
        if (phoneNumber.length < 10 || !$.isNumeric(phoneNumber)) {
            showWarningToastr(ewallet.message.phone_invalid);
        }
        else {
            $.ajax({
                type: "POST",
                url: profile_url.phone_verify,
                data: {
                    phoneNumber: phoneNumber
                },
                success: function (res) {
                    if (!res.success) {
                        var message = res.message;
                        showWarningToastr(message);
                        gotoStep(1);
                    }
                    else {
                        gotoStep(2);
                    }
                },
                error: function (err) {
                    showErrorToastr(ewallet.message.request_fail);
                    gotoStep(1);
                }
            })
        }
    });

    $("#btnPrevious").on('click', function (e) {
        gotoStep(1);
    });

    $("#btnConfirmPhoneVerify").on('click', function () {
        var otp = $("#phone_otp").val();
        var phoneNumber = $("#phone_number").val();
        if (otp.length < ewallet.phone_otp_length || otp.length > ewallet.phone_otp_length) {
            showErrorToastr(ewallet.message.phone_otp_invalid);
        }
        else {
            $.ajax({
                type: "POST",
                url: profile_url.confirm_phone_verify,
                data: {
                    phoneNumber: phoneNumber,
                    code: otp
                },
                success: function (res) {
                    if (res.success) {
                        showSuccessToastr(message);
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

});
