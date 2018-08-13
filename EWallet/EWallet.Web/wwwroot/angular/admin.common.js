var currentModelId = "";
function closeModalWindow() {
    $('#' + currentModelId).data('tWindow').close();
}
function openModalWindow(modalId) {
    currentModelId = modalId;
    $('#' + modalId).data('tWindow').center().open();
}

function setLocation(url) {
    window.location.href = url;
}

function OpenWindow(query, w, h, scroll) {
    var l = (screen.width - w) / 2;
    var t = (screen.height - h) / 2;

    winprops = 'resizable=1, height=' + h + ',width=' + w + ',top=' + t + ',left=' + l + 'w';
    if (scroll) winprops += ',scrollbars=1';
    var f = window.open(query, "_blank", winprops);
}



function locdau(str) {
    str = str.toLowerCase();
    str = str.replace(/à|á|ạ|ả|ã|â|ầ|ấ|ậ|ẩ|ẫ|ă|ằ|ắ|ặ|ẳ|ẵ/g, "a");
    str = str.replace(/è|é|ẹ|ẻ|ẽ|ê|ề|ế|ệ|ể|ễ/g, "e");
    str = str.replace(/ì|í|ị|ỉ|ĩ/g, "i");
    str = str.replace(/ò|ó|ọ|ỏ|õ|ô|ồ|ố|ộ|ổ|ỗ|ơ|ờ|ớ|ợ|ở|ỡ/g, "o");
    str = str.replace(/ù|ú|ụ|ủ|ũ|ư|ừ|ứ|ự|ử|ữ/g, "u");
    str = str.replace(/ỳ|ý|ỵ|ỷ|ỹ/g, "y");
    str = str.replace(/đ/g, "d");
    str = str.replace(/ð/g, "d");
    str = str.replace(/!|@|\$|%|\^|\*|\(|\)|\+|\=|\<|\>|\?|\/|,|\.|\:|\'| |\"|\&|\#|\[|\]|~/g, "-");
    str = str.replace(/-+-/g, "-"); 
    str = str.replace(/^\-+|\-+$/g, "");
    //str = str.replace("-", "");
    str = str.replace(" ", "-");
    return str;
}

function formatDate(date) {
    date = new Date(date.replace(/-/g, "/"));
    var hours = date.getHours();
    var minutes = date.getMinutes();
    var ampm = hours >= 12 ? 'pm' : 'am';
    hours = hours % 12;
    hours = hours ? hours : 12; // the hour '0' should be '12'
    minutes = minutes < 10 ? '0' + minutes : minutes;
    var strTime = hours + ':' + minutes + ' ' + ampm;
    return date.getMonth() + 1 + "/" + date.getDate() + "/" + date.getFullYear() + "  " + strTime;
}


function tabstrip_on_tab_select(e) {
    //we use this function to store selected tab index into HML input
    //this way we can persist selected tab between HTTP requests
    $("#selected-tab-index").val($(e.item).index());
}

function display_kendoui_grid_error(e) {
    if (e.errors) {
        if ((typeof e.errors) == 'string') {
            //single error
            //display the message
            alert(e.errors);
        } else {
            //array of errors
            //source: http://docs.kendoui.com/getting-started/using-kendo-with/aspnet-mvc/helpers/grid/faq#how-do-i-display-model-state-errors?
            var message = "The following errors have occurred:";
            //create a message containing all errors.
            $.each(e.errors, function (key, value) {
                if (value.errors) {
                    message += "\n";
                    message += value.errors.join("\n");
                }
            });
            //display the message
            alert(message);
        }
    } else {
        alert('Error happened');
    }
}

function OnCreateSuccessParent(data) {
    if (data.success){ 
        $('a.k-window-action').trigger('click');
        $('span.k-i-close').trigger('click');
    }
    else {
        if (data.message) {
            alert(data.message);
        }
    }
};

function addAntiForgeryToken(data) {
    //if the object is undefined, create a new one.
    if (!data) {
        data = {};
    }
    //add token
    var tokenInput = $('input[name=__RequestVerificationToken]');
    if (tokenInput.length) {
        data.__RequestVerificationToken = tokenInput.val();
    }
    return data;
};

var ShowLoading = function (loadingText) {
    if (loadingText) {
        $('.loading').find('.loading-text').text(loadingText);
    }
    $('.loading').fadeIn();
}
var HideLoading = function () {
    $('.loading').fadeOut();
}

UpdateMenuState = function ($location) {

    //$('ul.treeview-menu a').each(function () {
    //    var $this = $(this);
    //    //Get A Href Url
    //    var aLink = $this.attr("href");
    //    //Parse to location params
    //    var location = getLocation(aLink);

    //    //Get current location
    //    var currenrHref = window.location.href;
    //    var currentLocation = getLocation(currenrHref);
    //    if ($location != undefined && aLink.toLowerCase().indexOf($location.$$path.toLowerCase()) >= 0) {
    //        $('li.current-item ul.treeview-menu li.active').removeClass('current-item').removeClass("active");
    //        $this.parents("li").addClass("current-item").addClass("active");
    //    }
    //})

}

var ActiveMenu = function () {
    //cap nhat lai trang thai menu
    $('.limenu').removeClass('active');
    $('.limenu').removeClass('open');
    $('.datalistmenu').removeClass('active');
    $('.limenu a .selected').remove();
    var currentLink = window.location;
    var $currentNode = $('a[href="' + currentLink.hash + '"]');
    $currentNode.parents('li').addClass('active');
    $currentNode.append('<span class="selected"></span>');
}
function getLocation(href) {
    var location = document.createElement("a");
    location.href = href;
    // IE doesn't populate all link properties when setting .href with a relative URL,
    // however .href will return an absolute URL which then can be used on itself
    // to populate these additional fields.
    if (location.host == "") {
        location.href = location.href;
    }
    return location;
};

$(document).ready(function () {

    /* Menu Fucntion
    * ======     
    */
    $('.sidebar-menu li, treeview-menu li a').click(function () {
        setTimeout(function () {
            UpdateMenuState();
        }, 0);
    })
    UpdateMenuState();
    /* Tooltip
    * ======     
    */
    $('input[data-tooltip]').focus(function () {
        $(this).tooltip('show');
    })
    $('input[data-tooltip]').blur(function () {
        $(this).tooltip('hide');
    })

    //Stop Paste

    $('input:not(#UserName):not(#Password), textarea').bind("paste", function (e) {
        e.preventDefault();
    });
});

function setCustomerCookie(data) {
    if (data == undefined)
        $.removeCookie('bts.wiki.customers');
    else
        $.cookie('bts.wiki.customers', data);
}

function getCustomerCookie() {
    return $.cookie('bts.wiki.customers');
}

/* Validate Form
* ====== 
* CHeck form input valid
*/
$.fn.isValid = function () {
    var $this = $(this);
    var validator = $this.validate(); // obtain validator
    var anyError = false;

    $this.find("input").each(function () {
        if (!validator.element(this)) { // validate every input element inside this step
            anyError = true;
        }
    });
    return !anyError;
};

/* Serialize a form to json object
   * ====== 
   * Using by call: $(form).serializeObject();
   */
$.fn.serializeObject = function () {
    var o = {};
    var a = this.serializeArray();
    $.each(a, function () {
        if (o[this.name] !== undefined) {
            if (!o[this.name].push) {
                o[this.name] = [o[this.name]];
            }
            o[this.name].push(this.value || '');
        } else {
            o[this.name] = this.value || '';
        }
    });
    return o;
};

/************* Toastr Start *************/
var showSuccessToastr = function (msg, title, callback) {
    msg = msg || "Thông Báo";
    title = title || "Thông Báo";
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
    msg = msg || "Thông Báo";
    title = title || "Thông Báo";
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
    msg = msg || "Thông Báo";
    title = title || "Thông Báo";
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
    msg = msg || "Thông Báo";
    title = title || "Thông Báo";
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
var getUrlParameter = function getUrlParameter(sParam) {
    var sPageURL = decodeURIComponent(window.location.search.substring(1)),
        sURLVariables = sPageURL.split('&'),
        sParameterName,
        i;

    for (i = 0; i < sURLVariables.length; i++) {
        sParameterName = sURLVariables[i].split('=');

        if (sParameterName[0] === sParam) {
            return sParameterName[1] === undefined ? true : sParameterName[1];
        }
    }
};

/************* Xử lý parse QueryString ************* */
function checkExistQueryString() {
    var field = 'q';
    var url = window.location.href;
    if (url.indexOf('?') != -1)
        return true;
    else if (url.indexOf('&') != -1)
        return true;
    return false
}

function ParseQueryString() {
    //parse querystring
    var urlParams;
    (window.onpopstate = function () {
        var match,
            pl = /\+/g,  // Regex for replacing addition symbol with a space
            search = /([^&=]+)=?([^&]*)/g,
            decode = function (s) { return decodeURIComponent(s.replace(pl, " ")); },
            query = window.location.search.substring(1);

        urlParams = {};
        while (match = search.exec(query))
            urlParams[decode(match[1])] = decode(match[2]);
    })();

    $(document).ready(function () {
        if (checkExistQueryString()) {
            //bind data for input text
            $(".form-control").each(function () {
                if ($(this).is('input') && $(this).attr("type") == "text") {
                    for (var key in urlParams) {
                        if ($(this).attr("id").toLowerCase() == key.toLowerCase()) {
                            $(this).val(urlParams[key]);
                        }
                    }
                }
                else if ($(this).is('select')) {
                    for (var key in urlParams) {
                        if ($(this).attr("id").toLowerCase() == key.toLowerCase()) {
                            $(this).val(urlParams[key]);
                        }
                    }
                }
                else if ($(this).is('input') && $(this).attr("type") == "checkbox") {
                    for (var key in urlParams) {
                        if ($(this).attr("id").toLowerCase() == key.toLowerCase()) {
                            $(this).prop("checked", urlParams[key]);
                        }
                    }
                }
                else if ($(this).is('input') && $(this).attr("type") == "checkbox" && $(this).attr("make-switch")) {
                    for (var key in urlParams) {
                        if ($(this).attr("id").toLowerCase() == key.toLowerCase()) {
                            $(this).bootstrapSwitch('state', urlParams[key]);
                        }
                    }
                }
            });

            if (oTable == null) {
                search();
            } else {
                oTable.search('').draw();
            }
        }

    });
}