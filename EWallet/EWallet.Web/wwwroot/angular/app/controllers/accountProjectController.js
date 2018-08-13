app.controller('accountProjectController', ['$scope', '$location', function ($scope, $location) {

    var oTable;
    var table = $("#tbl");
    var form = $('#formInput');
    var listinurl = "/Project/ListAccount/";
    var listouturl = "/Project/ListAccountOut/";
    var adduserurl = "/Project/AddUserInProject";
    var deleteuserurl = "/Project/AddUserInProject";
    var editurl = '/Project/Edit/';

    form.unbind();
    //Validate
    form.removeData('validator');
    form.removeData('unobtrusiveValidation');
    $.validator.unobtrusive.parse(form);

    form.find('input').change(function () {
        $(this).removeData('previousValue');
        form.validate().element('#' + $(this).attr('Id'));
    });

    $scope.search = function () {
        LoadData();
    };

    $scope.searchout = function () {
        LoadDataOut();
    }

    $scope.list = function () {
        $location.url(listurl);
    };

    $scope.backListAccount = function (projectid) {
        $location.url(listinurl + projectid);
    };

    $scope.addRule = function () {
        var projectid = $("#ProjectId").val();
        var listid = getListUserId();

        ShowLoading('Vui lòng đợi...');


        $.ajax({
            contentType: 'application/json; charset=utf-8',
            url: adduserurl,
            data: JSON.stringify({ ProjectId : projectid, ListUserId : listid }),
            dataType: 'json',
            type: "POST",
            success: function (data) {
                if (data.Status == "Fail") {
                    HideLoading();
                    swal({ title: data.Title, text: data.Message, type: data.Type, confirmButtonText: "Đóng" });
                } else if (data.Status == "Success") {
                    HideLoading();
                    swal({ title: data.Title, text: data.Message, type: data.Type });
                    $location.url(listinurl + data.Id);
                    $scope.$apply();
                }
            }
        })
    };

    $scope.addRuleContinue = function () {
        var projectid = $("#ProjectId").val();
        var listid = getListUserId();

        ShowLoading('Vui lòng đợi...');


        $.ajax({
            contentType: 'application/json; charset=utf-8',
            url: adduserurl,
            data: JSON.stringify({ ProjectId: projectid, ListUserId: listid }),
            dataType: 'json',
            type: "POST",
            success: function (data) {
                if (data.Status == "Fail") {
                    HideLoading();
                    swal({ title: data.Title, text: data.Message, type: data.Type, confirmButtonText: "Đóng" });
                } else if (data.Status == "Success") {
                    HideLoading();
                    swal({ title: data.Title, text: data.Message, type: data.Type });
                    LoadDataOut();
                    $scope.$apply();
                }
            }
        })
    };

    function getListUserId() {
        var strlistuserid = "";
        for (var i = 0; i < listuser.length; i++) {
            if (i == listuser.length - 1) {
                strlistuserid = strlistuserid + listuser[i].UserId;                
            }
            else {
                strlistuserid = strlistuserid + listuser[i].UserId + ",";
            }
        }

        return strlistuserid;
    }

    function LoadData() {
        var keyword = $("#Keyword").val();
        var projectid = $("#ProjectId").val();

        if ($.fn.dataTable.isDataTable(table)) {
            oTable.destroy();
        }

        oTable = $(table).DataTable({
            "serverSide": true,
            "processing": true,
            "searching": false,
            "lengthMenu": DataTablesConst.LengthMenu,
            "pageLength": DataTablesConst.PageLength,
            "language": DataTablesConst.Language,
            "ajax": {
                "url": listinurl,
                "async": false,
                "data": function (d) {
                    d.Keyword = keyword,
                    d.ProjectId = projectid
                },
                "type": "POST",
                "dataSrc": function (json) {
                    //console.log(json);
                    if (!checkRight(json)) {
                        json = { iTotalRecords: 0, iTotalDisplayRecords: 0, sEcho: 1, aaData: [] };
                    }
                    return json.aaData;
                }
            },
            "columns": [
                {
                    "data": "NickName",
                    "title": "NickName",
                    "orderable": false
                }, {
                    "data": "FullName",
                    "title": "Họ tên",
                    "orderable": false
                }, {
                    "data": "Email",
                    "title": "Email",
                    "orderable": false
                }, {
                    "title": "Action",
                    "render": function (datatemp, type, row, meta) {
                        return '<button class="btn red deleteItem" role="button" data-id="'+ row.Id +'"><i class="fa fa-times"></i> Xóa</button>'
                    },
                    "orderable": false,
                }
            ]
        });

        //xóa item
        $(".deleteItem").click(function () {
            var projectid = $("#ProjectId").val();
            var listid = $(this).attr("data-id") + "";

            swal({
                title: "Thông báo",
                text: "Bạn có chắc muốn xóa tài khoản này ra khỏi Project này?",
                type: "warning",
                showCancelButton: true,
                confirmButtonColor: "#DD6B55",
                confirmButtonText: "Đồng ý",
                cancelButtonText: "Hủy bỏ",
                closeOnConfirm: false,
                closeOnCancel: false
            }, function (isConfirm) {
                if (isConfirm) {
                    ShowLoading('Vui lòng đợi...');
                    $.ajax({
                        contentType: 'application/json; charset=utf-8',
                        url: '/Project/AddUserOutProject',
                        data: JSON.stringify({ ProjectId: projectid, ListUserId: listid }),
                        dataType: 'json',
                        type: "POST",
                        success: function (data) {
                            if (data.Status == "Fail") {
                                HideLoading();
                                swal({ title: data.Title, text: data.Message, type: data.Type, confirmButtonText: "Đóng" });
                            } else if (data.Status == "Success") {
                                HideLoading();
                                swal({ title: data.Title, text: data.Message, type: data.Type });
                                LoadData();
                            }
                        }
                    })
                    swal("Thông báo", "Bạn đã xóa thành công", "success");
                }
                else { swal("Thông báo", "Đã hủy", "error"); }
            });
        });
    }

    function LoadDataOut() {
        var keyword = $("#Keyword").val();
        var projectid = $("#ProjectId").val();

        if ($.fn.dataTable.isDataTable(table)) {
            oTable.destroy();
        }

        oTable = $(table).DataTable({
            "serverSide": true,
            "processing": true,
            "searching": false,
            "lengthMenu": DataTablesConst.LengthMenu,
            "pageLength": DataTablesConst.PageLength,
            "language": DataTablesConst.Language,
            "ajax": {
                "url": listouturl,
                "async": false,
                "data": function (d) {
                    d.Keyword = keyword,
                    d.ProjectId = projectid
                },
                "type": "POST",
                "dataSrc": function (json) {
                    //console.log(json);
                    if (!checkRight(json)) {
                        json = { iTotalRecords: 0, iTotalDisplayRecords: 0, sEcho: 1, aaData: [] };
                    }
                    return json.aaData;
                }
            },
            "columns": [
                {
                    "title": "",
                    "render": function (datatemp, type, row, meta) {
                        return '<input type="checkbox" id="'+ row.Id +'">'
                    },
                    "orderable": false,
                    "width": 30,
                    "className": "txt-center"
                },
                {
                    "data": "NickName",
                    "title": "NickName",
                    "orderable": false
                }, {
                    "data": "FullName",
                    "title": "Họ tên",
                    "orderable": false
                }, {
                    "data": "Email",
                    "title": "Email",
                    "orderable": false
                }
            ]
        });

        $('input[type="checkbox"]').on('change', function (event) {

            var userid = $(this).attr("id");

            if ($(this).is(":checked")) {
                listuser.push({
                    "UserId": userid
                });
            }
            else {
                jQuery.each(listuser, function (i, val) {
                    if (val.UserId == userid) // delete index
                    {
                        listuser.splice(i, 1);
                    }
                });
            }
        });
    }
}]);