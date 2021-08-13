(function () {
    'use strict';

    angular
        .module('app', ['ngRoute', 'ngCookies', 'oc.lazyLoad'])
        .config(config)
        .directive("script", function () {
            return {
                restrict: "EA",
                scope: false,
                link: function (scope, elem, attr) {
                    if (attr.type === "text/javascript-lazy") {
                        var code = elem.text();
                        var f = new Function(code);
                        f();
                    }
                }
            };
        })
        .run(run);

    config.$inject = ['$routeProvider', '$locationProvider'];
    function config($routeProvider, $locationProvider) {
        var ocLazyLoad = function(files) {
            return [
                "$ocLazyLoad",
                function($ocLazyLoad) {
                    return $ocLazyLoad
                        .load([ {
                            name : "app",
                            insertBefore : "#ng_load_plugins_before",
                            files : files
                        } ]);
                } ];
        };

        $routeProvider
            .when('/', {
                templateUrl: 'html-page/home.html'
            })
            .when('/crs', {
                templateUrl: 'html-page/crs.html'
            })
            .when('/cac-dich-vu-khac', {
                templateUrl: 'html-page/cac-dich-vu-khac.html'
            })
            .when('/cam-ket-cua-hicon', {
                templateUrl: 'html-page/cam-ket-cua-hicon.html'
            })
            .when('/thong-diep-cua-ct-hdqt', {
                templateUrl: 'html-page/thong-diep-cua-ct-hdqt.html'
            })
            .when('/tam-nhin-su-menh', {
                templateUrl: 'html-page/tam-nhin-su-menh.html'
            })
            .when('/chung-chi-giai-thuong', {
                templateUrl: 'html-page/chung-chi-giai-thuong.html'
            })
            .when('/cong-ty-thanh-vien', {
                templateUrl: 'html-page/cong-ty-thanh-vien.html'
            })
            .when('/cung-cap-dich-vu-tu-van', {
                templateUrl: 'html-page/cung-cap-dich-vu-tu-van.html'
            })
            .when('/da-hoan-thanh', {
                templateUrl: 'html-page/da-hoan-thanh.html'
            })
            .when('/dang-trien-khai', {
                templateUrl: 'html-page/dang-trien-khai.html'
            })
            .when('/dich-vu', {
                templateUrl: 'html-page/dich-vu.html'
            })
            .when('/dich-vu-bao-hanh-bao-tri', {
                templateUrl: 'html-page/dich-vu-bao-hanh-bao-tri.html'
            })
            .when('/du-an', {
                templateUrl: 'html-page/du-an.html'
            })
            .when('/gioi-thieu-hicon', {
                templateUrl: 'html-page/gioi-thieu-hicon.html'
            })
            .when('/nang-luc-con-nguoi', {
                templateUrl: 'html-page/nang-luc-con-nguoi.html'
            })
            .when('/nang-luc-thiet-bi', {
                templateUrl: 'html-page/nang-luc-thiet-bi.html'
            })
            .when('/so-do-to-chuc', {
                templateUrl: 'html-page/so-do-to-chuc.html'
            })
            .when('/thi-cong-cong-trinh-cong-nghiep', {
                templateUrl: 'html-page/thi-cong-cong-trinh-cong-nghiep.html'
            })
            .when('/thi-cong-cong-trinh-dan-dung', {
                templateUrl: 'html-page/thi-cong-cong-trinh-dan-dung.html'
            })
            .when('/thi-cong-ha-tang-ky-thuat', {
                templateUrl: 'html-page/thi-cong-ha-tang-ky-thuat.html'
            })
            .when('/thu-vien-anh', {
                templateUrl: 'html-page/thu-vien-anh.html'
            })
            .when('/thu-vien-tai-lieu', {
                templateUrl: 'html-page/thu-vien-tai-lieu.html'
            })
            .when('/tin-noi-bo', {
                templateUrl: 'html-page/tin-noi-bo.html'
            })
            .when('/tin-xa-hoi', {
                templateUrl: 'html-page/tin-xa-hoi.html'
            })
            .when('/trong-tuong-lai', {
                templateUrl: 'html-page/trong-tuong-lai.html'
            })
            .when('/thu-vien', {
                templateUrl: 'html-page/thu-vien.html'
            })
            .when('/tin-tuc', {
                templateUrl: 'html-page/tin-tuc.html'
            })
            .when('/he-thong-gia-tri-cot-loi', {
                templateUrl: 'html-page/he-thong-gia-tri-cot-loi.html'
            })
            .when('/lien-he', {
                templateUrl: 'html-page/lien-he.html'
            })
            .otherwise({ redirectTo: '/' });
    }

    run.$inject = ['$rootScope', '$location', '$cookies', '$http'];
    function run($rootScope, $location, $cookies, $http) {
        // keep user logged in after page refresh
        $rootScope.globals = $cookies.getObject('globals') || {};
        if ($rootScope.globals.currentUser) {
            $http.defaults.headers.common['Authorization'] = 'Basic ' + $rootScope.globals.currentUser.authdata;
        }

        $rootScope.$on('$locationChangeStart', function (event, next, current) {
            // redirect to login page if not logged in and trying to access a restricted page

        });
    }

})();