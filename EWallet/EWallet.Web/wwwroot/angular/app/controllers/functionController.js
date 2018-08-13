app.controller('functionController', ['$scope', '$location', function ($scope, $location) {
    

    var titleTab = ['Danh sách Object Database', 'Danh sách tính năng'];
    $scope.tab = 1;

    $scope.TitleTab = titleTab[0];

    $scope.currentTab = function (index) {
        return ($scope.tab == index);
    }

    $scope.changeTab = function (index) {
        $scope.tab = index;
        $scope.TitleTab = titleTab[index - 1];
    }

    var oTable;
    var oTableList;
    var table = $("#tbl");
    var tablelist = $("#tbllist");
    var form = $('#formInput');
    var aform = $('#AformInput');
    var list = "/ProjectFunction/List/";
    var listlog = "/ProjectFunction/ShowLogDatabase/";
    var listfclog = "/ProjectFunction/ShowLogFunction/";
    var listurl = "/ProjectFunction/ListObjectDatabase/";
    var createdbfunction = "/ProjectFunction/CreateDatabaseFunction/";
    var editdbfunction = "/ProjectFunction/EditDatabaseFunction/";
    var projectfcurl = "/ProjectFunction/Function/";
    var createfunction = "ProjectFunction/Create/";
    var editfunction = "/ProjectFunction/EditFunction/";
    var templateurl = "/ProjectFunction/GetTemplate";
    var deletefcurl = "/ProjectFunction/DeleteFunction/";
    var projectonlineurl = "/ProjectFunction/ListOnline/";

    var objectdatabase = null;
    var editobjectdatabase = null;
    
    form.unbind();
    //Validate
    form.removeData('validator');
    form.removeData('unobtrusiveValidation');
    $.validator.unobtrusive.parse(form);

    form.find('input').change(function () {
        $(this).removeData('previousValue');
        form.validate().element('#' + $(this).attr('Id'));
    });

    //list
    $scope.list = function (url) {
        $location.url(list + url);
    }

    //insert function
    $scope.saveCreate = function () {
        //Form Submit
        form.unbind().submit(function (e) {
            e.preventDefault();
            var $this = $(this);
            if ($(this).valid()) {
                ShowLoading('Vui lòng đợi...');

                var data = $this.serializeObject();
                data.IsOnline = $('#IsOnline').prop('checked');

                var params = data.ProjectId;
                if (data.ParentId > 0){
                    params = params + '/' + data.ParentId;
                }

                $.ajax({
                    contentType: 'application/json; charset=utf-8',
                    url: createfunction,
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
                            $location.url(list + params);
                            $scope.$apply();
                        }
                    }
                })
            }
        });
    };

    //insert continue function
    $scope.saveCreateContinue = function () {
        //Form Submit
        form.unbind().submit(function (e) {
            e.preventDefault();
            var $this = $(this);
            if ($(this).valid()) {
                ShowLoading('Vui lòng đợi...');

                var data = $this.serializeObject();
                data.IsOnline = $('#IsOnline').prop('checked');
                $.ajax({
                    contentType: 'application/json; charset=utf-8',
                    url: createfunction,
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
                            $location.url(projectfcurl + data.Id);
                            $scope.$apply();
                        }
                    }
                })
            }
        });
    };

    //edit function
    $scope.saveEdit = function () {
        //Form Submit
        form.unbind().submit(function (e) {
            e.preventDefault();
            var $this = $(this);
            if ($(this).valid()) {
                ShowLoading('Vui lòng đợi...');

                var data = $this.serializeObject();
                data.IsOnline = $('#IsOnline').prop('checked');
                var params = data.ProjectId;
                if (data.ParentId > 0) {
                    params = params + '/' + data.ParentId;
                }

                $.ajax({
                    contentType: 'application/json; charset=utf-8',
                    url: editfunction,
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
                            $location.url(list + params);
                            $scope.$apply();
                        }
                    }
                })
            }
        });
    };

    $scope.saveEditContinue = function () {
        //Form Submit
        form.unbind().submit(function (e) {
            e.preventDefault();
            var $this = $(this);
            if ($(this).valid()) {
                ShowLoading('Vui lòng đợi...');

                var data = $this.serializeObject();
                data.IsOnline = $('#IsOnline').prop('checked');

                $.ajax({
                    contentType: 'application/json; charset=utf-8',
                    url: editfunction,
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
                            $location.url(projectfcurl + data.Id);
                            $scope.$apply();
                        }
                    }
                })
            }
        });
    };

    $scope.searchOnline = function () {
        LoadDataOnline();
    };

    $scope.search = function () {
        LoadData();
    };

    $scope.searchObject = function (functionid) {
        LoadDataObject(functionid);
    };
      
    $scope.searchLogObject = function (id) {
        LoadLogData(id);
    };

    $scope.searchLogFunctionObject = function (id) {
        LoadLogFunction(id);
    };

    $scope.saveCreateDatabaseFunction = function () {
        //Form Submit
        form.unbind().submit(function (e) {
            e.preventDefault();
            var $this = $(this);
            if ($(this).valid()) {
                ShowLoading('Vui lòng đợi...');

                var data = $this.serializeObject();
                data.IsOnline = $('#IsOnline').prop('checked');

                var projectFunctionId = data.ProjectFunctionId;

                $.ajax({
                    contentType: 'application/json; charset=utf-8',
                    url: createdbfunction,
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
                            $location.url(projectfcurl + projectFunctionId);
                            $scope.$apply();
                        }
                    }
                })
            }
        });
    };

    $scope.saveCreateDatabaseFunctionContinue = function () {
        //Form Submit
        form.unbind().submit(function (e) {
            e.preventDefault();
            var $this = $(this);
            if ($(this).valid()) {
                ShowLoading('Vui lòng đợi...');

                var data = $this.serializeObject();
                data.IsOnline = $('#IsOnline').prop('checked');

                var projectFunctionId = data.ProjectFunctionId;

                $.ajax({
                    contentType: 'application/json; charset=utf-8',
                    url: createdbfunction,
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
                            $location.url(editdbfunction + projectFunctionId + '/' + data.Id);
                            $scope.$apply();
                        }
                    }
                })
            }
        });
    };

    $scope.editDatabaseFunction = function () {
        //Form Submit
        aform.unbind().submit(function (e) {
            e.preventDefault();
            var $this = $(this);
            if ($(this).valid()) {
                ShowLoading('Vui lòng đợi...');

                var data = $this.serializeObject();
                data.IsOnline = $('#IsOnline').prop('checked');

                var projectFunctionId = data.ProjectFunctionId;

                $.ajax({
                    contentType: 'application/json; charset=utf-8',
                    url: editdbfunction,
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
                            $location.url(projectfcurl + projectFunctionId);
                            $scope.$apply();
                        }
                    }
                })
            }
        });
    };

    $scope.editDatabaseFunctionContinue = function () {
        //Form Submit
        aform.unbind().submit(function (e) {
            e.preventDefault();
            var $this = $(this);
            if ($(this).valid()) {
                ShowLoading('Vui lòng đợi...');

                var data = $this.serializeObject();
                data.IsOnline = $('#IsOnline').prop('checked');

                var projectFunctionId = data.ProjectFunctionId;

                $.ajax({
                    contentType: 'application/json; charset=utf-8',
                    url: editdbfunction,
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
                            $location.url(editdbfunction + projectFunctionId + '/' + data.Id);
                            $scope.$apply();
                        }
                    }
                })
            }
        });
    };

    $scope.closeDatabaseFunction = function () {
        aform.unbind().submit(function (e) {
            e.preventDefault();
            $("#update-object").modal('hide');
        });
        
    };

    $scope.projectfunction = function (functionid) {
        $location.url(projectfcurl + functionid);
        $scope.$apply();
    };

    $scope.saveRelease = function () {
        //Form Submit
        form.unbind().submit(function (e) {
            e.preventDefault();
            var $this = $(this);
            if ($(this).valid()) {
                ShowLoading('Vui lòng đợi...');

                var data = $this.serializeObject();
                data.ReleaseCode = editor.getValue();
                $.ajax({
                    contentType: 'application/json; charset=utf-8',
                    url: 'ProjectFunction/Release/',
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
                            $scope.$apply();
                        }
                    }
                })
            }
        });
    };

    //$scope.showModalRelease = function (id) {
    //    $.ajax({
    //        contentType: 'application/json; charset=utf-8',
    //        url: "ProjectFunction/Release?projectfunctionid=" + id,
    //        dataType: 'html',
    //        type: "GET",
    //        async: false,
    //        success: function (data) {
    //            $("#update-object").find(".modal-body").html(data);
    //            $("#update-object").modal("show");
    //            var editor = LoadMirrorReleaseCode();
    //            //$("#DeployId").select2();

    //            $("#createobject").click(function (e) {
    //                var $this = $(this).closest("#AformInput");
    //                if ($this.valid()) {
    //                    ShowLoading('Vui lòng đợi...');

    //                    var data = $this.serializeObject();
    //                    data.ReleaseCode = editor.getValue();

    //                    $.ajax({
    //                        contentType: 'application/json; charset=utf-8',
    //                        url: 'ProjectFunction/Release/',
    //                        data: JSON.stringify({ model: data }),
    //                        dataType: 'json',
    //                        type: "POST",
    //                        success: function (data) {
    //                            if (data.Status == "Fail") {
    //                                HideLoading();
    //                                swal({ title: data.Title, text: data.Message, type: data.Type, confirmButtonText: "Đóng" });
    //                            } else if (data.Status == "Success") {
    //                                HideLoading();
    //                                swal({ title: data.Title, text: data.Message, type: data.Type });
    //                                $scope.$apply();
    //                                LoadDataObject(id);
    //                            }
    //                        },
    //                        complete: function () {
    //                            $("#update-object").modal('hide');
    //                        }
    //                    })
    //                }
    //            });
    //        }
    //    });
    //};

    $scope.showModalObject = function (id) {

        if (objectdatabase == null) {
            $.ajax({
                contentType: 'application/json; charset=utf-8',
                url: "ProjectFunction/CreateDatabaseFunction?id=" + id,
                dataType: 'html',
                type: "GET",
                async: false,
                success: function (data) {
                    $("#update-object").modal("show");
                    $("#update-object").find(".modal-body").html("loading...");
                    $("#update-object").find(".modal-body").html(data);

                    CKEDITOR.replace('CKDescription');
                    var editor = LoadEditor();
                    $("#createobject").click(function (e) {
                        var $this = $(this).closest("#AformInput");
                        if ($this.valid()) {
                            ShowLoading('Vui lòng đợi...');

                            var data = $this.serializeObject();
                            data.IsOnline = $this.find('#IsOnline').prop('checked');
                            data.CKDescription = CKEDITOR.instances.CKDescription.getData();
                            data.SourceCode = editor.getValue();
                            objectdatabase = data;
                            var projectFunctionId = data.ProjectFunctionId;

                            $.ajax({
                                contentType: 'application/json; charset=utf-8',
                                url: createdbfunction,
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
                                        $scope.$apply();
                                        LoadDataObject(id);
                                        objectdatabase = null;
                                    }
                                },
                                complete: function () {
                                    $("#update-object").modal('hide');
                                }
                            })
                        }
                    });
                }
            });
        }
        else {
            $("#update-object").modal("show");
        }

        
    };

    $scope.addTemplate = function () {
        $.ajax({
            contentType: 'application/json; charset=utf-8',
            url: templateurl,
            dataType: 'json',
            type: "POST",
            success: function (data) {
                var editor = $("#Description").data("wysihtml5").editor;
                var txt = editor.getValue();
                editor.setValue(txt + data.Message, true);
            }
        })
    };

    function LoadDataObject(functionid) {
        var FunctionId = $("#Id").val();

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
                    d.FunctionId = FunctionId
                },
                "type": "POST",
                "async": false,
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
                    "data": "Id",
                    "title": "#",
                    "orderable": false
                }, {
                    "data": "Name",
                    "title": "Object Name",
                    "orderable": false
                }, {
                    "data": "TypeDBName",
                    "title": "Loại",                    
                    "orderable": false,
                }, {
                    "data": "IsOnline",
                    "title": "Online",
                    "orderable": false,
                    "render": function (datatemp, type, row, meta){
                        if (row.IsOnline)
                            return '<input data-functionid=' + row.Id + ' data-id=' + functionid + ' type="checkbox" checked class="cboxonline" /> <span class="label label-sm label-success">Online </span>';
                        else
                            return '<input data-functionid=' + row.Id + ' data-id=' + functionid + ' type="checkbox" class="cboxonline" /> <span class="label label-sm label-warning">Chưa Online </span>';
                    },
                }, {
                    "data": "DatabaseName",
                    "title": "Tên DB",
                    "orderable": false,
                }, {
                    "data": "NickName",
                    "title": "Người cập nhật",
                    "orderable": false,
                }, {
                    "title": "Action",
                    "render": function (datatemp, type, row, meta) {
                        return '<a target="_blank" class="btn green" role="button" href="#/ProjectFunction/View/' + row.Id + '"><i class="fa fa-info"></i> Xem</a>' +
                        //'<a class="btn green"   role="button" href="#/ProjectFunction/EditDatabaseFunction/' + functionid + '/' + row.Id + '"><i class="fa fa-pencil"></i> Cập nhật</a>' +
                        '<button class="btn green abc" role="button" data-functionid=' + row.Id + ' data-id=' + functionid + ' ><i class="fa fa-pencil"></i> Cập nhật</button>' +
                        '<a target="_blank" class="btn green" role="button" href="#/ProjectFunction/ShowLogDatabase/' + row.Id + '"><i class="fa fa-pencil"></i> Show Log</a>' +
                        '<button class="btn red" onclick="deleteItemObject(' + row.Id + ')" role="button"><i class="fa fa-times"></i> Xóa</button>'
                    },
                    "width": 500,
                    "orderable": false,
                }
            ]
        });

        $('.cboxonline').change(function () {
            var id = $(this).attr("data-functionid");
            var check = false;
            if ($(this).is(":checked")) {
                check = true;
            }
            $.ajax({
                contentType: 'application/json; charset=utf-8',
                url: "ProjectFunction/EditDatabaseFunctionIsOnline",
                data: JSON.stringify({ Id: id, IsOnline: check }),
                dataType: 'json',
                type: "POST",
                success: function (data) {
                    if (data.Status == "Fail") {
                        HideLoading();
                        swal({ title: data.Title, text: data.Message, type: data.Type, confirmButtonText: "Đóng" });
                    } else if (data.Status == "Success") {
                        HideLoading();
                        showSuccessToastr(data.Message);
                        $scope.$apply();
                        LoadDataObject(FunctionId);
                    }
                }
            });            
        });

        $(".abc").click(function () {
            if (editobjectdatabase == null) {
                var functionid = $(this).attr("data-functionid");
                var id = $(this).attr("data-id");
                $.ajax({
                    contentType: 'application/json; charset=utf-8',
                    url: "ProjectFunction/EditDatabaseFunction?id=" + id + "&dbfunctionid=" + functionid,
                    dataType: 'html',
                    type: "GET",
                    async: false,
                    success: function (data) {
                        $("#update-object").modal("show");
                        $("#update-object").find(".modal-body").html("loading...")
                        $("#update-object").find(".modal-body").html(data);
                        
                        CKEDITOR.replace('CKDescription');
                        var editor = LoadEditor();

                        $("#updateobject").click(function (e) {
                            var $this = $(this).closest("#AformInput");
                            if ($this.valid()) {
                                ShowLoading('Vui lòng đợi...');

                                var data = $this.serializeObject();
                                data.IsOnline = $this.find('#IsOnline').prop('checked');
                                data.CKDescription = CKEDITOR.instances.CKDescription.getData();
                                data.SourceCode = editor.getValue();
                                var projectFunctionId = data.ProjectFunctionId;

                                //luu lai
                                editobjectdatabase = data;

                                $.ajax({
                                    contentType: 'application/json; charset=utf-8',
                                    url: editdbfunction,
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
                                            $scope.$apply();
                                            LoadDataObject(FunctionId);
                                            editobjectdatabase = null;
                                        }
                                    },
                                    complete: function () {
                                        $("#update-object").modal('hide');
                                    }
                                })
                            }
                        });
                    }
                });
            }
            else {
                $("#update-object").modal('show');
            }
        });
    }

    function LoadDataOnline() {
        var date = $("#StrDate").val();
        var projectid = $("#ProjectId").val();
        var keyword = $("#Keyword").val();

        if ($.fn.dataTable.isDataTable(tablelist)) {
            oTableList.destroy();
        }

        oTableList = $(tablelist).DataTable({
            "serverSide": true,
            "processing": true,
            "searching": false,
            "lengthMenu": DataTablesConst.LengthMenu,
            "pageLength": DataTablesConst.PageLength,
            "language": DataTablesConst.Language,
            "ajax": {
                "url": projectonlineurl,
                "data": function (d) {
                    d.ProjectId = projectid,
                    d.Keyword = keyword,
                    d.StrDate = date
                },
                "type": "POST",
                "async": false,
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
                    "data": "FunctionName",
                    "title": "Tên chức năng",
                    "orderable": false
                }, {
                    "data": "ProjectName",
                    "title": "Tên project",
                    "orderable": false
                }
            ]
        });
    }

    function LoadData() {        
        var projectfcid = $("#ProjectFunctionId").val();
        var projectid = $("#ProjectId").val();
        var keyword = $("#Keyword").val();

        if ($.fn.dataTable.isDataTable(tablelist)) {
            oTableList.destroy();
        }

        oTableList = $(tablelist).DataTable({
            "serverSide": true,
            "processing": true,
            "searching": false,
            "lengthMenu": DataTablesConst.LengthMenu,
            "pageLength": DataTablesConst.PageLength,
            "language": DataTablesConst.Language,
            "ajax": {
                "url": list,
                "data": function (d) {
                    d.ProjectId = projectid,
                    d.Keyword = keyword,
                    d.ProjectFunctionId = projectfcid
                },
                "type": "POST",
                "async": false,
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
                    "data": "Id",
                    "title": "#",
                    "orderable": false
                }, {
                    "data": "FunctionName",
                    "title": "Tên Module/Chức năng",
                    "orderable": false
                }, {
                    "data": "IsOnline",
                    "title": "Online",
                    "orderable": false,
                    "render": function (datatemp, type, row, meta) {
                        if (!row.IsOnline)
                            return '<span class="label label-sm label-warning">Chưa Online </span>';
                        else
                            return '<span class="label label-sm label-success">Online </span>';
                    },
                }, {
                    "title": "Action",
                    "render": function (datatemp, type, row, meta) {
                        var script = '';
                        if (row.IsHaveChild > 0) {
                            script = script + '<a class="btn green" role="button" href="#/ProjectFunction/List/' + projectid + '/' + row.Id + '"><i class="fa fa-info"></i> Xem</a>';
                        }
                        script = script + '<a class="btn green" role="button" href="#/ProjectFunction/Function/' + row.Id + '"><i class="fa fa-info"></i> Cập nhật</a>';

                        script = script + '<a class="btn green" role="button" href="#/ProjectFunction/Create/' + row.ProjectId + '/' + row.Id + '"><i class="fa fa-info"></i> Thêm mới</a>'

                        //return '<a class="btn green" role="button" href="#/ProjectFunction/List/' + row.Id + '"><i class="fa fa-info"></i> Xem</a>' +
                        //'<a class="btn green" role="button" href="#/ProjectFunction/EditDatabaseFunction/' + functionid + '/' + row.Id + '"><i class="fa fa-pencil"></i> Cập nhật</a>' +
                        script = script + '<button class="btn red btnFunctionDelete" data-id="'+ row.Id +'" role="button"><i class="fa fa-times"></i> Xóa</button>';

                        return script;
                    },
                    "width": 500,
                    "orderable": false,
                }
            ]
        });

        $(".btnFunctionDelete").click(function () {
            var id = $(this).attr("data-id");

            swal({
                title: "Thông báo",
                text: "Bạn có chắc muốn xóa Database này?",
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
                        url: deletefcurl,
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
                                location.reload();
                            }
                        }
                    })
                    swal("Thông báo", "Bạn đã xóa thành công", "success");
                }
                else { swal("Thông báo", "Đã hủy", "error"); }
            });
        });
    }

    function LoadLogData(functionid) {
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
                "url": listlog,
                "data": function (d) {
                    d.functionid = functionid
                },
                "type": "POST",
                "async": false,
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
                 },
                {
                    "data": "Version",
                    "title": "Version",
                    "orderable": false
                }, {
                    "data": "SourceCode",
                    "title": "SourceCode",
                    "orderable": false,
                    "render": function (datatemp, type, row, meta) {
                        return '<textarea id="' + functionid + '_' + row.Version +'" class="SourceCode">' + row.SourceCode + '</textarea>';
                    },
                }, {
                    "data": "CreateDate",
                    "title": "Ngày cập nhật",
                    "orderable": false,
                    //"render": function (datatemp, type, row, meta) {
                    //    return formatDate(row.CreateDate);
                    //},
                }, {
                    "data": "NickName",
                    "title": "NickName",
                    "orderable": false
                }
            ],
            "fnInitComplete": function (oSettings, json) {
                if (json.aaData.length > 0) {
                    for (var i = 0; i < json.aaData.length; i++){
                        loadEditorByClass(functionid + '_' + json.aaData[i].Version);
                    }
                    
                }
            }
        });

        //loadEditorByClass(functionid);
    }

    function LoadLogFunction(functionid) {
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
                "url": listfclog,
                "data": function (d) {
                    d.FunctionId = functionid
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
                    "data": "Version",
                    "title": "Version",
                    "orderable": false
                }, {
                    "data": "Content",
                    "title": "Nội dung",
                    "orderable": false
                }, {
                    "data": "SourceCode",
                    "title": "SourceCode",
                    "render": function (datatemp, type, row, meta) {                   
                        return '<textarea id="' + functionid + '_' + row.Version + '" class="SourceCode">' + row.SourceCode + '</textarea>';
                    },
                    "orderable": false
                }
            ],
            "fnInitComplete": function (oSettings, json) {
                if (json.aaData.length > 0) {
                    if (json.aaData.length > 0) {
                        for (var i = 0; i < json.aaData.length; i++) {
                            loadEditorByClass(functionid + '_' + json.aaData[i].Version);
                        }

                    }
                }               
            }
        });
     
    }
    var loadEditorByClass = function (functionid) {
        var mime = 'text/x-sql';
        var editor = CodeMirror.fromTextArea(document.getElementById('' + functionid + ''), {
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

    var LoadEditor = function () {
        var mime = 'text/x-sql';
        var editor = CodeMirror.fromTextArea(document.getElementById('SourceCode'), {
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