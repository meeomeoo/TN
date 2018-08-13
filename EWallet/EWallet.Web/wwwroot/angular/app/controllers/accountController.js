app.controller('accountController', ['$scope', '$location', function ($scope, $location) {
    
    var oTable;
    var table = $("#tbl");
    var form = $('#formInput');
    var listurl = "/Account/List";
    var createurl = "/Account/Create";
    var editurl = '/Account/Edit/';
    var delurl = '/Account/Delete/';
    var changepass = '/Account/ChangePassword/';
    var indexurl = 'Home/Index';
    var providepsw = '/Account/ProvidePassword/';

    $scope.list = function () {
        $location.url(listurl);
    };

    $scope.search = function () {
        LoadData();
    };

    $scope.changePassword = function () {
        //Form Submit
        form.unbind().submit(function (e) {
            e.preventDefault();
            var $this = $(this);
            if ($(this).valid()) {
                ShowLoading('Vui lòng đợi...');

                var data = $this.serializeObject();

                $.ajax({
                    contentType: 'application/json; charset=utf-8',
                    url: changepass,
                    data: JSON.stringify({ model: data }),
                    dataType: 'json',
                    type: "POST",
                    success: function (data) {
                        if (data.Status == "Fail") {
                            HideLoading();
                            swal({ title: data.Title, text: data.Message, type: data.Type, confirmButtonText: "Đóng" });
                        } else if (data.Status == "Success") {
                            HideLoading();
                            swal({ title: data.Title, text: data.Message, type: data.Type });
                            $location.url('');
                            $scope.$apply();
                        }
                    }
                })
            }
        });
    };

    $scope.saveProfile = function () {
        //Form Submit
        form.unbind().submit(function (e) {
            e.preventDefault();
            var $this = $(this);
            if ($(this).valid()) {
                ShowLoading('Vui lòng đợi...');

                var data = $this.serializeObject();

                $.ajax({
                    contentType: 'application/json; charset=utf-8',
                    url: editurl,
                    data: JSON.stringify({ model: data }),
                    dataType: 'json',
                    type: "POST",
                    success: function (data) {
                        if (data.Status == "Fail") {
                            HideLoading();
                            swal({ title: data.Title, text: data.Message, type: data.Type, confirmButtonText: "Đóng" });
                        } else if (data.Status == "Success") {
                            HideLoading();
                            swal({ title: data.Title, text: data.Message, type: data.Type });
                            $location.url('');
                            $scope.$apply();
                        }
                    }
                })
            }
        });
    };

    $scope.saveProvidePassword = function () {
        //Form Submit
        form.unbind().submit(function (e) {
            e.preventDefault();
            var $this = $(this);
            if ($(this).valid()) {
                ShowLoading('Vui lòng đợi...');

                var data = $this.serializeObject();

                $.ajax({
                    contentType: 'application/json; charset=utf-8',
                    url: providepsw,
                    data: JSON.stringify({ model: data }),
                    dataType: 'json',
                    type: "POST",
                    success: function (data) {
                        if (data.Status == "Fail") {
                            HideLoading();
                            swal({ title: data.Title, text: data.Message, type: data.Type, confirmButtonText: "Đóng" });
                        } else if (data.Status == "Success") {
                            HideLoading();
                            swal({ title: data.Title, text: data.Message, type: data.Type });
                            $location.url(editurl + data.Id);
                            $scope.$apply();
                        }
                    }
                })
            }
        });
    };

    $scope.searchname = function (even) {
        if (even.keyCode == 13) {
            even.preventDefault();
            LoadData();
            return false;
        }
    };

    $scope.saveCreate = function () {
        //Form Submit
        form.unbind().submit(function (e) {
            e.preventDefault();
            var $this = $(this);
            if ($(this).valid()) {
                ShowLoading('Vui lòng đợi...');

                var data = $this.serializeObject();

                $.ajax({
                    contentType: 'application/json; charset=utf-8',
                    url: createurl,
                    data: JSON.stringify({ model: data }),
                    dataType: 'json',
                    type: "POST",
                    success: function (data) {
                        if (data.Status == "Fail") {
                            HideLoading();
                            swal({ title: data.Title, text: data.Message, type: data.Type, confirmButtonText: "Đóng" });
                        } else if (data.Status == "Success") {
                            HideLoading();
                            swal({ title: data.Title, text: data.Message, type: data.Type });
                            $location.url(listurl);
                            $scope.$apply();
                        }
                    }
                })
            }
        });
    }

    $scope.saveCreateContinue = function () {
        //Form Submit
        form.unbind().submit(function (e) {
            e.preventDefault();
            var $this = $(this);
            if ($(this).valid()) {
                ShowLoading('Vui lòng đợi...');

                var data = $this.serializeObject();

                $.ajax({
                    contentType: 'application/json; charset=utf-8',
                    url: createurl,
                    data: JSON.stringify({ model: data }),
                    dataType: 'json',
                    type: "POST",
                    success: function (data) {
                        if (data.Status == "Fail") {
                            HideLoading();
                            swal({ title: data.Title, text: data.Message, type: data.Type, confirmButtonText: "Đóng" });
                        } else if (data.Status == "Success") {
                            HideLoading();
                            swal({ title: data.Title, text: data.Message, type: data.Type });
                            $location.url(editurl + data.Id);
                            $scope.$apply();
                        }
                    }
                })
            }
        });
    }

    $scope.saveEdit = function () {
        //Form Submit
        form.unbind().submit(function (e) {
            e.preventDefault();
            var $this = $(this);
            if ($(this).valid()) {
                ShowLoading('Vui lòng đợi...');

                var data = $this.serializeObject();

                $.ajax({
                    contentType: 'application/json; charset=utf-8',
                    url: editurl,
                    data: JSON.stringify({ model: data }),
                    dataType: 'json',
                    type: "POST",
                    success: function (data) {
                        if (data.Status == "Fail") {
                            HideLoading();
                            swal({ title: data.Title, text: data.Message, type: data.Type, confirmButtonText: "Đóng" });
                        } else if (data.Status == "Success") {
                            HideLoading();
                            swal({ title: data.Title, text: data.Message, type: data.Type });
                            $location.url(listurl);
                            $scope.$apply();
                        }
                    }
                })
            }
        });
    }

    $scope.saveEditContinue = function () {
        //Form Submit
        form.unbind().submit(function (e) {
            e.preventDefault();
            var $this = $(this);
            if ($(this).valid()) {
                ShowLoading('Vui lòng đợi...');

                var data = $this.serializeObject();

                $.ajax({
                    contentType: 'application/json; charset=utf-8',
                    url: editurl,
                    data: JSON.stringify({ model: data }),
                    dataType: 'json',
                    type: "POST",
                    success: function (data) {
                        if (data.Status == "Fail") {
                            HideLoading();
                            swal({ title: data.Title, text: data.Message, type: data.Type, confirmButtonText: "Đóng" });
                        } else if (data.Status == "Success") {
                            HideLoading();
                            swal({ title: data.Title, text: data.Message, type: data.Type });
                            $location.url(editurl + data.Id);
                            $scope.$apply();
                        }
                    }
                })
            }
        });
    }

    function LoadData() {
        var keyword = $("#Keyword").val();

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
                "url": listurl,
                "type": "POST",
                "data": function (d) {
                    d.Keyword = keyword;
                },
                "async": false,
                "dataSrc": function (json) {
                    if (!checkRight(json)) {
                        json = { iTotalRecords: 0, iTotalDisplayRecords: 0, sEcho: 1, aaData: [] };
                    }
                    return json.aaData;
                }
            },
            "columns": [
                {
                    "data": "RowNumber",
                    "title": "#",
                    "orderable": false
                }, {
                    "data": "NickName",
                    "title": "Tên đăng nhập",
                    "orderable": false
                }, {
                    "data": "FullName",
                    "title": "Họ tên",
                    "orderable": false
                }, {
                    "data": "Email",
                    "title": "Email",
                    "orderable": false,
                }, {
                    "title": "Action",
                    "render": function (datatemp, type, row, meta) {
                        return '<a class="btn green" role="button" href="#/Account/Edit/' + row.Id + '"><i class="fa fa-info"></i> Cập nhật</a>' +
                        '<button class="btn red deleteItem" role="button" data-id="' + row.Id + '"><i class="fa fa-times"></i> Xóa</button>'
                    },
                    "width": 500,
                    "orderable": false,
                }
            ]
        });

        //xóa item
        $(".deleteItem").click(function () {
            var id = $(this).attr("data-id") + "";

            swal({
                title: "Thông báo",
                text: "Bạn có chắc muốn xóa tài khoản này?",
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
                        url: delurl,
                        data: JSON.stringify({ id: id }),
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

}]);