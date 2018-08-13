app.controller('projectController', ['$scope', '$location', function ($scope, $location) {

    var oTable;
    var table = $("#tbl");
    var form = $('#formInput');
    var listurl = "/Project/List";
    var createurl = "/Project/Create";    
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

    $scope.list = function () {
        $location.url(listurl);
    };

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
                "data": function (d) {
                    d.Keyword = keyword
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
                    "data": "ProjectName",
                    "title": "Tên Project",
                    "orderable": false
                }, {
                    "data": "Description",
                    "title": "Mô tả",
                    "orderable": false
                },
                //{
                //    "data": "FullName",
                //    "title": "Tên Manager",
                //    "orderable": false,
                //},
                {
                    "title": "Action",
                    "render": function (datatemp, type, row, meta) {
                        return '<a class="btn green" role="button" href="#/Project/Edit/' + row.Id + '"><i class="fa fa-info"></i> Cập nhật</a>' +
                        '<a class="btn green" role="button" href="#/Project/ListAccount/' + row.Id + '"><i class="fa fa-user"></i> User tham gia</a>' +
                        '<a class="btn green" role="button" href="#/Project/ListAccountOut/' + row.Id + '"><i class="fa fa-user"></i> Thêm User</a>' +
                        '<button class="btn red" role="button" onclick="Delete(' + row.Id + ')"><i class="fa fa-times"></i> Xóa</button>'
                    },
                    "width": 500,
                    "orderable": false,
                }
            ]
        });
    }
}]);