app.controller('deployController', ['$scope', '$location', function ($scope, $location) {
    
    var oTable;
    var table = $("#tbl");
    var form = $('#formInput');
    var listurl = "/Deploy/List";
    var createurl = "/Deploy/Create";
    var editurl = '/Deploy/Edit/';
    var delurl = '/Deploy/Delete/';
    var qcediturl = '/Deploy/QCEdit';
    var viewreleaseurl = '/Deploy/ViewRelease'

    var keyword = $location.search().Keyword;
    $("#Keyword").val(keyword);

    $scope.list = function () {
        $location.url(listurl);
    };

    $scope.search = function (isqcleader) {        
        LoadData(isqcleader);
    };

    $scope.searchViewRelease = function (deployid) {
        LoadViewRelease(deployid);
    };
    
    $scope.saveCreate = function () {
        //Form Submit
        form.unbind().submit(function (e) {
            e.preventDefault();
            var $this = $(this);
            if ($(this).valid()) {
                ShowLoading('Vui lòng đợi...');

                var data = $this.serializeObject();
                data.IsOnline = $this.find('#IsOnline').prop('checked');

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
                data.IsOnline = $this.find('#IsOnline').prop('checked');

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
                data.IsOnline = $this.find('#IsOnline').prop('checked');

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
                data.IsOnline = $this.find('#IsOnline').prop('checked');

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

    function LoadData(isqcleader) {
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
                "data": function (d) {
                    d.Keyword = keyword
                },
                "async": false,
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
                    "data": "RowNumber",
                    "title": "#",
                    "orderable": false
                }, {
                    "data": "ProjectName",
                    "title": "Tên Project",
                    "orderable": false
                }, {
                    "data": "DeployName",
                    "title": "Tiêu đề",
                    "orderable": false
                }, {
                    "data": "NickName",
                    "title": "NickName",
                    "orderable": false,
                }, {
                    "data": "IsOnline",
                    "title": "IsOnline",
                    "orderable": false,
                    "render": function (datatemp, type, row, meta) {
                        if (!row.IsOnline)
                            return '<span class="label label-sm label-warning">Chưa Online </span>';
                        else
                            return '<span class="label label-sm label-success">Online </span>';
                    },
                }, {
                    "data": "IsQCTest",
                    "title": "IsQCTest",
                    "orderable": false,
                    "render": function (datatemp, type, row, meta) {
                        if (!row.IsQCTest)
                            return '<span class="label label-sm label-warning">Chưa test </span>';
                        else
                            return '<span class="label label-sm label-success">OK </span>';
                    },
                }, {
                    "data": "QCNote",
                    "title": "QCNote",
                    "orderable": false,
                }, {
                    "title": "Action",
                    "render": function (datatemp, type, row, meta) {
                        if (isqcleader == 0) {
                            return '<a class="btn green" role="button" href="#/Deploy/Edit/' + row.Id + '"><i class="fa fa-info"></i> Cập nhật</a>' +
                                '<a class="btn green" role="button" href="#/Deploy/ViewRelease/' + row.Id + '"><i class="fa fa-info"></i> Xem Release</a>' +
                            '<button class="btn red deleteItem" role="button" data-id="' + row.Id + '"><i class="fa fa-times"></i> Xóa</button>'
                        }
                        return '<button class="btn green qcupdate" role="button" data-id="' + row.Id + '"><i class="fa fa-info"></i>QC Cập nhật</button>';
                    },
                    "width": 500,
                    "orderable": false,
                }
            ]
        });

        //qcupdate
        $(".qcupdate").click(function () {
            var id = $(this).attr("data-id");

            $.ajax({
                contentType: 'application/json; charset=utf-8',
                url: qcediturl + "?id=" + id,
                dataType: 'html',
                type: "GET",
                async: false,
                success: function (data) {
                    $("#update-deploy").find(".modal-body").html(data);
                    $("#update-deploy").modal("show");
                    $("#updatedeploy").click(function () {
                        var sform = $(this).closest("form");
                        //Form Submit
                        sform.unbind().submit(function (e) {
                            e.preventDefault();
                            var $this = $(this);
                            if ($(this).valid()) {
                                ShowLoading('Vui lòng đợi...');

                                var data = $this.serializeObject();
                                data.IsQCTest = $this.find('#IsQCTest').prop('checked');

                                $.ajax({
                                    contentType: 'application/json; charset=utf-8',
                                    url: qcediturl,
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
                                            $("#update-deploy").modal("hide");
                                            LoadData(isqcleader);
                                        }
                                    }
                                })
                            }
                        });
                    });
                }
            });
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

    //
    function LoadViewRelease(deployid) {
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
                "url": viewreleaseurl,
                "data": function (d) {
                    d.Keyword = keyword,
                    d.DeployId = deployid
                },
                "async": false,
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
                    "data": "RowNumber",
                    "title": "#",
                    "orderable": false
                }, {
                    "data": "NameRelease",
                    "title": "Tên Release",
                    "orderable": false
                }, {
                    "data": "NickName",
                    "title": "Người Release",
                    "orderable": false
                }, {
                    "data": "ReleaseCode",
                    "title": "ReleaseCode",
                    "orderable": false,
                    "render": function (datatemp, type, row, meta) {
                        return '<textarea id="' + deployid + '_' + row.RowNumber + '" class="ReleaseCode">' + row.ReleaseCode + '</textarea>';
                    },
                }
            ],
            "fnInitComplete": function (oSettings, json) {
                if (json.aaData.length > 0) {
                    if (json.aaData.length > 0) {
                        for (var i = 0; i < json.aaData.length; i++) {
                            loadEditorByClass(deployid + '_' + json.aaData[i].RowNumber);
                        }

                    }
                }
            }
        });
    }

    var loadEditorByClass = function (id) {
        var mime = 'text/x-sql';
        var editor = CodeMirror.fromTextArea(document.getElementById('' + id + ''), {
            mode: mime,
            indentWithTabs: true,
            smartIndent: true,
            lineNumbers: true,
            matchBrackets: true,
            autofocus: true,
            theme: 'twilight',
            extraKeys: {
                "F11": function (cm) {
                    //console.log(1);
                    cm.setOption("fullScreen", !cm.getOption("fullScreen"));
                },
                "Esc": function (cm) {
                    if (cm.getOption("fullScreen")) cm.setOption("fullScreen", false);
                }
            }
        });
        return editor;
    }

}]);