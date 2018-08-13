app.controller('membershipController', ['$scope', '$location', function ($scope, $location) {
    var allPages = [];
    var treeData;
    var jsTree;
    var hasInitTree = false;
    var currentPageId = -1;
    var dataSave = [];
    var updatenodeurl = '/MemberShip/SaveUpdateNode/';

    function buildPage(pages) {
        var tree = [];
        $.each(pages, function (index, element) {            
            var pageId = 'project_' + element.PageId;

            if (!element.IsProject) {
                pageId = 'function_' + element.PageId;
            }

            allPages[pageId] = element;

            var item = {
                'id': pageId,
                'text': element.PageId + "." + element.Name,
                'icon': ''
            };

            //if (element.PageId == currentPageId) {
            //    item['state'] = { 'selected': true };
            //}
            if (element.Groups.length > 0) {
                item['children'] = buildPage(element.Groups);
                item['icon'] = 'fa fa-folder';
            } else {
                item['icon'] = 'fa fa-file';
            }

            //if (element.IsEnable) {
            //    item['icon'] = item['icon'] + ' icon-state-warning';
            //} else {
            //    item['icon'] = item['icon'] + ' icon-state-default';
            //}

            tree.push(item);
        });
        return tree;
    }

    function getPages() {
        $.ajax({
            url: '/MemberShip/GetPages',
            contentType: "application/json;charset=uft-8",
            dataType: 'json',
            type: 'POST',
            error: function (res) {
                swal({ title: "Lỗi", text: res, type: "error", confirmButtonText: "Đóng" });
            },
            success: function (data) {
                if (!checkRight(data)) {
                    return;
                }

                treeData = buildPage(data);

                if (hasInitTree == true) {
                    $('#tree_3').jstree(true).settings.core.data = treeData;
                    $("#tree_3").jstree(true).refresh();
                } else {
                    $("#tree_3")
                        // listen for event
                        .on('select_node.jstree', function (node, selected, event) {
                            //var pageId = selected.node.id.replace('page_', '');
                            //AddPageIdForRef(pageId);
                            //if (!($('#mySwitch').bootstrapSwitch('state'))) {
                            //    FilterData(allPages['page_' + pageId]);
                            //}

                            console.log("");
                        }).bind("move_node.jstree", function(e, data) {
                            console.log("Drop node " + data.node.id + " to " + data.parent);
                            dataSave[data.node.id] = { 'id': data.node.id, 'parent': data.parent };                            
                        })
                        .jstree({
                            "core": {
                                "themes": {
                                    "responsive": false
                                },
                                // so that create works
                                "check_callback": function (op, node, node_parent, pos, more) {
                                    if ((more && more.dnd && (op === 'move_node' || op === 'copy_node') && node.parent == "#") ||
                                        (more && more.dnd && (op === 'move_node' || op === 'copy_node') && node_parent.id == "#")) {
                                        return false;
                                    }
                                    return true;
                                },
                                "data": treeData
                            },
                            "types": {
                                "default": {
                                    "icon": "fa fa-folder icon-state-warning icon-lg"
                                },
                                "file": {
                                    "icon": "fa fa-file icon-state-warning icon-lg"
                                }
                            },
                            "state": { "key": "demo2" },
                            "plugins": ["contextmenu", "dnd", "state", "types"]
                        });
                    hasInitTree = true;
                }
            }
        });
    }

    $scope.initTree = function () {
        getPages();
    };

    $scope.saveUpdateNode = function () {
        
        var data = [];

        for (key in dataSave) {
            data.push(dataSave[key]);
        }

        data = JSON.stringify({ model: data });
        
        ShowLoading('Vui lòng đợi...');
        $.ajax({
            contentType: 'application/json; charset=utf-8',
            url: updatenodeurl,
            data: data,
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

    };

}]);