var app = angular.module('WikiApp', ['ngRoute', 'ngCookies']);
app.run(['$rootScope', '$templateCache', '$location', '$cookieStore', function ($rootScope, $templateCache, $location, $cookieStore) {
    $rootScope.mustReloadNotification = true;

    $rootScope.$on('$routeChangeStart', function (event, next, current) {
        if (!(next.controller == 'homeController' || next.controller == undefined))
            $(".r-bar").css({ "display": "none" });
            $(".control-sidebar").removeClass("control-sidebar-open");
            ShowLoading();
    });
    $rootScope.$on('$routeChangeSuccess', function (event, current, previous) {
        var cookie = getCustomerCookie();
        if (cookie == undefined || cookie == "") {
            window.location.href = '/login';
        }
       
        //Update Title
        $rootScope.title = (current.$$route.title) ? current.$$route.title : '' + ' [Wiki Portal]';
        
        HideLoading();
        ActiveMenu();
    });

    $rootScope.$on('$viewContentLoaded', function () {
        $templateCache.removeAll();
    });

}]);

app.config(['$httpProvider', function ($httpProvider) {
    //initialize get if not there
    if (!$httpProvider.defaults.headers.get) {
        $httpProvider.defaults.headers.get = {};
    }
    //disable IE ajax request caching
    $httpProvider.defaults.headers.get['If-Modified-Since'] = 'Mon, 26 Jul 1997 05:00:00 GMT';
    // extra
    $httpProvider.defaults.headers.get['Cache-Control'] = 'no-cache';
    $httpProvider.defaults.headers.get['Pragma'] = 'no-cache';
}]);
app.config(['$httpProvider', function ($httpProvider) {
    //initialize get if not there
    if (!$httpProvider.defaults.headers.get) {
        $httpProvider.defaults.headers.get = {};
    }
    //disable IE ajax request caching
    $httpProvider.defaults.headers.get['If-Modified-Since'] = 'Mon, 26 Jul 1997 05:00:00 GMT';
    // extra
    $httpProvider.defaults.headers.get['Cache-Control'] = 'no-cache';
    $httpProvider.defaults.headers.get['Pragma'] = 'no-cache';
}]);

app.config(['$routeProvider', '$locationProvider', function ($routeProvider, $locationProvider) {
    $routeProvider
    //account
    .when('/', {
        templateUrl: '/Home/HomePage',
        controller: 'accountController',
        title: 'Trang chủ'
    })
    .when('/login', {
        templateUrl: '/Account/Login',
        controller: 'accountController',
        title: 'Đăng nhập'
    })
    .when('/Account/LogOut', {
        templateUrl: '/Account/LogOut',
        controller: 'accountController',
        title: 'Đăng xuất'
    })
    .when('/Account/ChangePassword', {
        templateUrl: '/Account/ChangePassword',
        controller: 'accountController',
        title: 'Đổi mật khẩu'
    })
    .when('/Account/Profile', {
        templateUrl: '/Account/Profile',
        controller: 'accountController',
        title: 'Thông tin cá nhân'
    })
    .when('/Account/List', {
        templateUrl: '/Account/List',
        controller: 'accountController',
        title: 'Danh sách tài khoản'
    })
    .when('/Account/Create', {
        templateUrl: '/Account/Create',
        controller: 'accountController',
        title: 'Thêm mới tài khoản'
    })
    .when('/Account/Edit/:id', {
        templateUrl: function (params) { return '/Account/Edit/' + params.id; },
        controller: 'accountController',
        title: 'Cập nhật tài khoản'
    })

    //Home
    .when('/Home/About', {
        templateUrl: '/Home/About',
        controller: 'homeController'
    })
    .when('/Home/Contact', {
        templateUrl: '/Home/Contact',
        controller: 'homeController'
    })

    //Project
    .when('/Project/Create', {
        templateUrl: '/Project/Create',
        controller: 'projectController',
        title: 'Thêm mới Project'
    })
    .when('/Project/List', {
        templateUrl: '/Project/List',
        controller: 'projectController',
        title: 'Danh sách Project'
    })
    .when('/Project/Edit/:id', {
        templateUrl: function (params) { return '/Project/Edit/' + params.id; },
        controller: 'projectController',
        title: 'Cập nhật Project'
    })

    //Project Database
    .when('/ProjectDatabase/Create', {
        templateUrl: '/ProjectDatabase/Create',
        controller: 'projectDatabaseController',
        title: 'Thêm mới Database'
    })
    .when('/ProjectDatabase/Edit/:id', {
        templateUrl: function (params) { return '/ProjectDatabase/Edit/' + params.id; },
        controller: 'projectDatabaseController',
        title: 'Cập nhật Database'
    })
    .when('/ProjectDatabase/List', {
        templateUrl: '/ProjectDatabase/List',
        controller: 'projectDatabaseController',
        title: 'Danh sách Database'
    })

    //AccountProject
     .when('/Project/ListAccount/:id', {
         templateUrl: function (params) { return '/Project/ListAccount/' + params.id; },
         controller: 'accountProjectController',
         title: 'Danh sách tài khoản tham gia'
     })
    .when('/Project/ListAccountOut/:id', {
        templateUrl: function (params) { return '/Project/ListAccountOut/' + params.id; },
        controller: 'accountProjectController',
        title: 'Danh sách tài khoản chưa tham gia'
    })

    //Function
    .when('/ProjectFunction/List/:id', {
        templateUrl: function (params) { return '/ProjectFunction/List/' + params.id; },
        controller: 'functionController',
        title: 'Danh sách tính năng'
    })
    .when('/ProjectFunction/List/:id/:projectfcid', {
        templateUrl: function (params) { return '/ProjectFunction/List?id=' + params.id + '&projectfcid=' + params.projectfcid; },
        controller: 'functionController',
        title: 'Danh sách tính năng'
    })
    .when('/ProjectFunction/Create/:id', {
        templateUrl: function (params) { return '/ProjectFunction/Create/' + params.id; },
        controller: 'functionController',
        title: 'Thêm mới tính năng'
    })
    .when('/ProjectFunction/Create/:id/:parentid', {
        templateUrl: function (params) { return '/ProjectFunction/Create?id=' + params.id + '&parentid=' + params.parentid; },
        controller: 'functionController',
        title: 'Thêm cấp con cho tính năng'
    })
    .when('/ProjectFunction/Function/:id', {
        templateUrl: function (params) { return '/ProjectFunction/Function/' + params.id; },
        controller: 'functionController',
        title: 'Cập nhật tính năng'
    })
    .when('/ProjectFunction/ShowLogDatabase/:id', {
        templateUrl: function (params) { return '/ProjectFunction/ShowLogDatabase/' + params.id; },
        controller: 'functionController',
        title: 'Lịch sử Database'
    })
    .when('/ProjectFunction/ShowLogFunction/:id', {
        templateUrl: function (params) { return '/ProjectFunction/ShowLogFunction/' + params.id; },
        controller: 'functionController',
        title: 'Lịch sử tính năng'
    })
    //.when('/ProjectFunction/CreateDatabaseFunction/:id', {
    //    templateUrl: function (params) { return '/ProjectFunction/CreateDatabaseFunction/' + params.id; },
    //    controller: 'functionController'
    //})
    //.when('/ProjectFunction/EditDatabaseFunction/:id/:dbfunctionid', {
    //    templateUrl: function (params) { return '/ProjectFunction/EditDatabaseFunction?id=' + params.id + '&dbfunctionid=' + params.dbfunctionid; },
    //    controller: 'functionController'
    //})
    .when('/ProjectFunction/View/:id', {
        templateUrl: function (params) { return '/ProjectFunction/View?id=' + params.id; },
        controller: 'functionController',
        title: 'Xem tính năng'
    })
    //deploy
    .when('/Deploy/List', {
        templateUrl: '/Deploy/List',
        controller: 'deployController',
        title: 'Trang chủ'
    })
    .when('/Deploy/Create', {
        templateUrl: '/Deploy/Create',
        controller: 'deployController'
    })
    .when('/Deploy/Edit/:id', {
        templateUrl: function (params) { return '/Deploy/Edit?id=' + params.id; },
        controller: 'deployController',
        title: 'Cập nhật Deploy'
    })
    .when('/Deploy/ViewRelease/:id', {
        templateUrl: function (params) { return '/Deploy/ViewRelease?id=' + params.id; },
        controller: 'deployController',
        title: 'Xem Release'
    })

    //ProjectFunctionOnline
    .when('/ProjectFunction/ListOnline', {
        templateUrl: '/ProjectFunction/ListOnline',
        controller: 'functionController',
        title: 'Tính năng online'
    })

    .when('/ProjectFunction/Release/:projectfunctionid', {
        templateUrl: function (params) { return '/ProjectFunction/Release?projectfunctionid=' + params.projectfunctionid; },
        controller: 'functionController',
    })

    //Release
    .when('/Release/List', {
        templateUrl: '/Release/List',
        controller: 'releaseController',
        title: 'Danh sách Release'
    })
    .when('/Release/Reversion/:id', {
        templateUrl: function (params) { return '/Release/Reversion?id=' + params.id; },
        controller: 'releaseController',
        title: 'Danh sách Version'
    })
    .when('/Release/DatabaseLog/:id', {
        templateUrl: function (params) { return '/Release/DatabaseLog?id=' + params.id; },
        controller: 'releaseController',
    })


    //MemberShip
    .when('/MemberShip/PageFunctionManager', {
        templateUrl: '/MemberShip/PageFunctionManager',
        controller: 'membershipController',
        title: 'Kéo thả theo nhóm'
    })

}]);
