app.controller('releaseController', ['$scope', '$location', function ($scope, $location) {
    
    var oTable;
    var table = $("#tbl");
    var form = $('#formInput');
    var listurl = "/Release/List";
    var listreversionurl = "/Release/ListReversion";

    $scope.list = function () {
        $location.url(listurl);
    };

    $scope.search = function () {
        LoadData();
    };

    $scope.searchReversion = function (releaseid) {
        LoadDataReversion(releaseid);
    };

    function LoadDataReversion(releaseid) {

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
                "url": listreversionurl,
                "data": function (d) {
                    d.ReleaseId = releaseid
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
                    "data": "Id",
                    "title": "Id",
                    "orderable": false
                }, {
                    "data": "Name",
                    "title": "Tên Object",
                    "orderable": false,
                }, {
                    "data": "NickName",
                    "title": "NickName",
                    "orderable": false
                }, {
                    "data": "Description",
                    "title": "Mô tả",
                    "orderable": false,
                }, {
                    "title": "Action",
                    "render": function (datatemp, type, row, meta) {
                        return '<a target="_blank" class="btn green" role="button" href="#/Release/DatabaseLog/' + row.Id + '"><i class="fa fa-info"></i> Xem</a>'
                    },
                    "width": 200,
                    "orderable": false,
                }
            ]
        });
    }

    function LoadData() {
        var keyword = $("#Keyword").val();
        var deployid = $("#DeployId").val();

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
                    "orderable": false,
                    "render": function (datatemp, type, row, meta) {
                        return '<a href="#/Release/Reversion/'+ row.ID +'">'+ row.NameRelease +'</a>'
                    },
                }, {
                    "data": "NickName",
                    "title": "NickName",
                    "orderable": false
                }, {
                    "data": "DeployName",
                    "title": "Tên deploy",
                    "orderable": false,
                }
            ]
        });
    }

}]);