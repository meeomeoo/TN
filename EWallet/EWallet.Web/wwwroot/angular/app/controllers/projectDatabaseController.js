app.controller('projectDatabaseController', ['$scope', '$location', function ($scope, $location) {

    var oTable;
    var table = $("#tbl");
    var form = $('#formInput');
    var listurl = "/ProjectDatabase/List";
    var createurl = "/ProjectDatabase/Create";    
    var editurl = '/ProjectDatabase/Edit/';
    var deleteurl = '/ProjectDatabase/Delete';

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

    $scope.saveCreate = function () {
        //Form Submit
        form.unbind().submit(function (e) {
            e.preventDefault();
            var $this = $(this);
            if ($(this).valid()) {
                ShowLoading('Vui lòng đợi...');

                var data = $this.serializeObject();
                data.Id = 1;

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
                data.Id = 1;

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
    };

    $scope.list = function () {
        $location.url(listurl);
    };
    
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
                "url": listurl,
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
                    "data": "RowNumber",
                    "title": "#",
                    "orderable": false
                }, {
                    "data": "DatabaseName",
                    "title": "Tên Database",
                    "orderable": false
                }, {
                    "data": "ProjectName",
                    "title": "Tên Project",
                    "orderable": false
                }, {
                    "title": "Action",
                    "render": function (datatemp, type, row, meta) {
                        return '<a class="btn green" role="button" href="#/ProjectDatabase/Edit/' + row.Id + '"><i class="fa fa-info"></i> Cập nhật</a>' +
                        '<button class="btn red" role="button" onclick="Delete(' + row.Id + ')"><i class="fa fa-times"></i> Xóa</button>'
                    },
                    "orderable": false,
                }
            ]
        });
    }
}]);