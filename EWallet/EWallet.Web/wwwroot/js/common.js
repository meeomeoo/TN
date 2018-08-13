// Api
var globalAdmin = {
    ApiUrl: "/api"
};

// Method api
var methodAdmin = {
    GetCategory: "GetCategory",
};

// Status code
var statusCode = {
    SystemError: -1,
    Success: 1,
    Fail: 2
};

var Loading = {
    Show: function () { $("#loading").show(); },
    Hide: function () {
        setTimeout(function () { $("#loading").hide(); }, 1000);
    }
};

var byte2Kb = function (b) {
    return Math.ceil(b / 1024);
};

var GetFileNameOfPath = function (fullPath) {
    var text = ReplaceAll(fullPath, "\\", "/");
    console.log(text);
    var pos = text.lastIndexOf("/");
    if (pos > 0) {
        text = text.substr(pos + 1);
    }
    return text;
}

var ReplaceAll = function (text, oldText, newText) {
    var str = text.replace(oldText, newText);
    while (str.indexOf(oldText) > 0) {
        str = str.replace(oldText, newText);
    }
    return str;
}

function ConvertToCurrency(num) {
    num = num.toString().replace(/,/g, '');
    if (isNaN(num))
        num = "0";
    var sign = (num == (num = Math.abs(num)));
    num = Math.floor(num * 100 + 0.50000000001);
    //cents = num % 100;
    num = Math.floor(num / 100).toString();
    //if (cents < 10)
    //cents = "0" + cents;
    for (var i = 0; i < Math.floor((num.length - (1 + i)) / 3); i++)
        num = num.substring(0, num.length - (4 * i + 3)) + '.' +
            num.substring(num.length - (4 * i + 3));
    return num;
}

var LoginCode = {
    //[Description("Thành công")]
    Success: 0,

    //[Description("Username hoặc mật khẩu không chính xác")]
    PassOrEmailError: 1,

    //[Description("Tài khoản đã bị khóa")]
    BanNick: 2,

    //[Description("Lý do bảo mật")]
    NotAllowEnter: 3,

    //[Description("Tài khoản chưa được kích hoạt")]
    UserIsNotActive: 4,

    //[Description("Tài khoản không tồn tại")]
    NonExistsAccount: 5,

    //[Description("Lỗi hệ thống")]
    SystemError: 9999,
}

var Loading = {
    Id: "#loading",
    Show: function () { $(this.Id).show(); },
    Hide: function () { $(this.Id).hide() }
};

var Popup = {
    Id: "#modalAlert",
    Show: function (title, content, width, callbackhiden) {
        if (title == undefined || title == '')
            title = 'Nofification';

        $(this.Id + " .modal-title").html(title);
        $(this.Id + " div.modal-body").html(content);
        $(this.Id + " .modal-dialog").css("width", width);
        $(this.Id).modal("show");
    },
    Hide: function (callback) {
        $(this.Id).modal("hide");
        if (typeof callback == 'function') {
            callback();
        }
    }
};

var Confirm = {
    Id: "#modalConfirm",
    YesNo: function (title, content, yesText, noText, yesCallback, noCalback) {
        var footer = '<div>';
        //footer += ' <div class="box-btn">';
        footer += ' <button type="button" class="btn dark-white p-x-md" data-dismiss="modal">' + noText + '</button>';
        footer += '          <button type="button" id="btnConfirmYes" class="btn danger p-x-md" data-dismiss="modal">' + yesText + '</button>';
        //footer += '     </div>';
        $(this.Id + " .modal-title").html(title);
        $(this.Id + " .modal-body").html(content);
        $(this.Id + " .modal-footer").html(footer);
        $(this.Id).modal("show");

        $(this.Id + " #btnConfirmYes").click(function () {
            $(this.Id).modal("hide");
            if (typeof yesCallback == 'function') {
                yesCallback();
            }
        });
        $(this.Id).on('hidden.bs.modal', function (e) {
            $(this.Id).modal("hide");
            if (typeof noCalback == 'function') {
                noCalback();
            }
        });

    },
    Hide: function (callback) {
        $(this.Id + " modalConfirm").modal("hide");
        if (typeof callback == 'function') {
            callback();
        }
    }
}

var Alert = {
    Success: function (title, content, width, callbackhiden) {
        //$("#modalPopup").addClass("confirm");
        content = "<div class='success-icon'>" + content + "</div>";
        Popup.Show(title, content, width, callbackhiden);
    },
    Error: function (title, content, width, callbackhiden) {
        //$("#modalPopup").addClass("confirm");
        content = "<div class='error-icon'>" + content + "</div>";
        Popup.Show(title, content, width, callbackhiden);
    },
    Warning: function (title, content, width, callbackhiden) {
        //$("#modalPopup").addClass("confirm");
        content = "<div class='warning-icon'>" + content + "</div>";
        Popup.Show(title, content, width, callbackhiden);
    }
}

var CreatePaging = function (pageIndex, totalItem, itemInPage, countDisplayPage, fnName) {
    //console.log(pageIndex + " - " + totalItem + " - " + itemInPage);
    if (pageIndex <= 0 || totalItem <= 0 || itemInPage <= 0) {
        return "";
    }

    if (countDisplayPage == null || countDisplayPage == "" || countDisplayPage == "undefined")
        countDisplayPage = 5;
    //console.log(pageIndex + " - " + totalItem + " - " + itemInPage);
    var totalPage = Math.ceil(totalItem / itemInPage);
    if (totalPage <= 1)
        return "";
    //tinh so trang show;
    var cdp = Math.floor(countDisplayPage / 2);
    var startpage = pageIndex - cdp;
    if (startpage < 1)
        startpage = 1;
    var endpage = startpage + cdp * 2;
    if (endpage > totalPage)
        endpage = totalPage;

    //console.log("totalPage="+totalPage);
    //console.log("startpage="+startpage);
    //console.log("endpage=" + endpage);
    var ul = '<ul class="pagination pagination-sm">';


    if (pageIndex > 1) {
        var onclickF = "";
        if (fnName != null && fnName.length > 0)
            onclickF = fnName + "(" + (pageIndex - 1) + ")";
        ul += '<li><a onclick="' + onclickF + '" datapage="' + (pageIndex - 1) + '" href="javascript:void(0)" aria-label="Previous"><span aria-hidden="true">&laquo;</span></a></li>';
    }

    for (var i = startpage; i <= endpage; i++) {
        var active = "";
        if (i == pageIndex)
            active = "active";
        var onclick = "";
        if (fnName != null && fnName.length > 0)
            onclick = fnName + "(" + i + ")";
        var li = '<li class="' + active + '"><a  onclick="' + onclick + '" datapage="' + i + '"  href="javascript:void(0)">' + i + '</a></li>';
        ul += li;
    }

    if (pageIndex < totalPage) {
        var onclickL = "";
        if (fnName != null && fnName.length > 0)
            onclickL = fnName + "(" + (pageIndex + 1) + ")";
        ul += '<li><a onclick="' + onclickL + '" datapage="' + (pageIndex + 1) + '" href="javascript:void(0)" aria-label="Next"><span aria-hidden="true">&raquo;</span></a></li>';
    }
    ul += "</ul>";

    return ul;
};

// Kiem tra do dai ky tu input
function CheckValidateNumber(object) {
    //debugger;
    var isNumeric = $.isNumeric(object.value);
    if (isNumeric) {

        if (object.value.length > object.maxLength)
            object.value = object.value.slice(0, object.maxLength)
        else if (object.value < 0)
            object.value = '';
    }
    else {
        object.value = '';
    }
}

// Set cookie
function setCookie(cname, cvalue, exdays) {
    var d = new Date();
    d.setTime(d.getTime() + (exdays * 24 * 60 * 60 * 1000));
    var expires = "expires=" + d.toUTCString();
    document.cookie = cname + "=" + cvalue + "; " + expires + ";path=/";
}

// Get cookie
function getCookie(cname) {
    var name = cname + "=";
    var ca = document.cookie.split(';');
    for (var i = 0; i < ca.length; i++) {
        var c = ca[i];
        while (c.charAt(0) == ' ') c = c.substring(1);
        if (c.indexOf(name) != -1) return c.substring(name.length, c.length);
    }
    return "";
}

function ConvertToUnSign(obj) {
    var str = obj;

    str = str.toLowerCase();
    str = str.replace(/à|á|ạ|ả|ã|â|ầ|ấ|ậ|ẩ|ẫ|ă|ằ|ắ|ặ|ẳ|ẵ/g, "a");
    str = str.replace(/è|é|ẹ|ẻ|ẽ|ê|ề|ế|ệ|ể|ễ/g, "e");
    str = str.replace(/ì|í|ị|ỉ|ĩ/g, "i");
    str = str.replace(/ò|ó|ọ|ỏ|õ|ô|ồ|ố|ộ|ổ|ỗ|ơ|ờ|ớ|ợ|ở|ỡ/g, "o");
    str = str.replace(/ù|ú|ụ|ủ|ũ|ư|ừ|ứ|ự|ử|ữ/g, "u");
    str = str.replace(/ỳ|ý|ỵ|ỷ|ỹ/g, "y");
    str = str.replace(/đ/g, "d");

    str = str.replace(/!|@|%|\^|\*|\(|\)|\+|\=|\<|\>|\?|\/|,|\.|\:|\;|\'| |\"|\&|\#|\[|\]|~|$|_/g, "-");// tìm và thay thế các kí tự đặc biệt trong chuỗi sang kí tự -
    str = str.replace(/-+-/g, "-"); //thay thế 2- thành 1-
    str = str.replace(/^\-+|\-+$/g, "");//cắt bỏ ký tự - ở đầu và cuối chuỗi

    return str;
}

/************* Toastr Start *************/
var showSuccessToastr = function (msg, title, callback) {
    msg = msg || "Nofification";
    title = title || "Nofification";
    callback = callback || null;

    toastr.options = {
        closeButton: true,
        positionClass: 'toast-bottom-right',
        showEasing: 'swing',
        hideEasing: 'linear',
        showMethod: 'fadeIn',
        hideMethod: 'fadeOut',
        onclick: callback
    };
    toastr['success'](msg, title);
}

var showInfoToastr = function (msg, title, callback) {
    msg = msg || "Nofification";
    title = title || "Nofification";
    callback = callback || null;

    toastr.options = {
        closeButton: true,
        positionClass: 'toast-bottom-right',
        showEasing: 'swing',
        hideEasing: 'linear',
        showMethod: 'fadeIn',
        hideMethod: 'fadeOut',
        onclick: callback
    };
    toastr['info'](msg, title);
}

var showWarningToastr = function (msg, title, callback) {
    msg = msg || "Nofification";
    title = title || "Nofification";
    callback = callback || null;

    toastr.options = {
        closeButton: true,
        positionClass: 'toast-bottom-right',
        showEasing: 'swing',
        hideEasing: 'linear',
        showMethod: 'fadeIn',
        hideMethod: 'fadeOut',
        onclick: callback
    };
    toastr['warning'](msg, title);
}

var showErrorToastr = function (msg, title, callback) {
    msg = msg || "Nofification";
    title = title || "Nofification";
    callback = callback || null;

    toastr.options = {
        closeButton: true,
        positionClass: 'toast-bottom-right',
        showEasing: 'swing',
        hideEasing: 'linear',
        showMethod: 'fadeIn',
        hideMethod: 'fadeOut',
        onclick: callback
    };
    toastr['error'](msg, title);
}
/************* Toastr End *************/

/**
Định nghĩa format cho ngày tháng
**/
var DateTimeFormat = {
    UpperDate: "DD/MM/YYYY",
    UpperDateTime: "DD/MM/YYYY HH:mm:ss",
    UpperMonth: "MM/YYYY",
    UpperHour: "HH",
    UpperDateYYYYMMDD: "YYYY/MM/DD",
    UpperDateYYYY: "YYYY",

    UpperDateEN: "MM/DD/YYYY",
    UpperDateTimeEN: "MM/DD/YYYY HH:mm:ss",

    LowerMonth: "mm/yyyy",
    LowerDate: "dd/mm/yyyy",
    LowerDateTime: "dd/mm/yyyy hh:mm:ss",
    LowerDateTimeScript: "dd/MM/yyyy h:i:s",
    LowerTimeFull: "dd/mm/yyyy hh:ii:ss",

}

function getParameterByName(name, url) {
    if (!url) url = window.location.href;
    name = name.replace(/[\[\]]/g, "\\$&");
    var regex = new RegExp("[?&]" + name + "(=([^&#]*)|&|#|$)"),
        results = regex.exec(url);
    if (!results) return null;
    if (!results[2]) return '';
    return decodeURIComponent(results[2].replace(/\+/g, " "));
}

function isValidEmail(e) {
    var pattern = /^([a-z\d!#$%&'*+\-\/=?^_`{|}~\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]+(\.[a-z\d!#$%&'*+\-\/=?^_`{|}~\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]+)*|"((([ \t]*\r\n)?[ \t]+)?([\x01-\x08\x0b\x0c\x0e-\x1f\x7f\x21\x23-\x5b\x5d-\x7e\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]|\\[\x01-\x09\x0b\x0c\x0d-\x7f\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]))*(([ \t]*\r\n)?[ \t]+)?")@(([a-z\d\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]|[a-z\d\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF][a-z\d\-._~\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]*[a-z\d\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])\.)+([a-z\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]|[a-z\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF][a-z\d\-._~\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]*[a-z\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])\.?$/i;
    return pattern.test(e);

};

// hien thong bao khi update
function showMessageAction() {
    var m = getParameterByName('message');

    if (m == null)
        return false;

    if (m == 'Success')
        showSuccessToastr('Cập nhật thành công');
    else if (m == 'AccessDenied')
        showErrorToastr('Bạn không có quyền truy cập');
    else
        showErrorToastr('Cập nhật thật bại');
};

function addCommas(nStr) {
    nStr += '';
    x = nStr.split('.');
    x1 = x[0];
    x2 = x.length > 1 ? '.' + x[1] : '';
    var rgx = /(\d+)(\d{3})/;
    while (rgx.test(x1)) {
        x1 = x1.replace(rgx, '$1' + ',' + '$2');
    }
    return x1 + x2;
}

//formatNumber
function formatNumber(num) {
    return num.toString().replace(/(\d)(?=(\d{3})+(?!\d))/g, "$1,")
}

// Loading please wait
var Loading = {
    Id: "#loading",
    Show: function () { $(this.Id).show(); },
    Hide: function () { $(this.Id).hide() }
};

// getParameterByName
function getParameterByName(name,url) {
    if (!url) url = window.location.href;
    name = name.replace(/[\[\]]/g, "\\$&");
    var regex = new RegExp("[?&]" + name + "(=([^&#]*)|&|#|$)"),
        results = regex.exec(url);
    if (!results) return null;
    if (!results[2]) return '';
    return decodeURIComponent(results[2].replace(/\+/g, " "));
}

// Hien thong bao khi update
function showMessageAction() {
    debugger;
    var m = getParameterByName('s');

    if (m == null)
        return false;

    if (m === 'success')
        showSuccessToastr('Update successfully');
    else if (m === 'fail')
        showErrorToastr('Update failed');

}

$(document).ready(function () {
    //$('#fromDate').datetimepicker({
    //    format: DateTimeFormat.UpperDateTime,
    //});

    //$('#toDate').datetimepicker({
    //    format: DateTimeFormat.UpperDateTime,
    //});

    // format numeric
    $(".numeric").number(true, 0);

    // test toast
    showMessageAction();
});