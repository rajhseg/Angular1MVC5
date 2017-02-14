/// <reference path="../../Scripts/angular.js" />
/// <reference path="../../Scripts/angular-route.js" />

var mvcapp = angular.module('mvcapp', ['ngRoute', 'ngCookies', 'ngStorage', 'angularNotification']);

mvcapp.config(['$routeProvider', '$localStorageProvider', function ($routeProvider, $localStorageProvider) {

    $routeProvider.when('/home', { templateUrl: 'app/templates/home.html', controller: 'homeController' })
        .when('/about', { templateUrl: 'app/templates/about.html', controller: 'aboutController' })
        .when('/login', { templateUrl: 'app/templates/login.html', controller: 'loginController' })
        .when('/register', { templateUrl: 'app/templates/register.html', controller: 'registerController' })
        .when('/profile', { templateUrl: 'app/templates/profile.html', controller: 'profileController', resolve: { authenticate: Authenticate } })
        .when('/history', { templateUrl: 'app/templates/history.html', controller: 'historyController', resolve: { authenticate: Authenticate } })
        .when('/messages', { templateUrl: 'app/templates/messages.html', controller: 'messagesController' })        
        .otherwise({
            redirectTo:'/home'
        });

    $localStorageProvider.setKeyPrefix('mvcapp');

}]);

Authenticate.$inject = ['$rootScope','$location','authService'];

function Authenticate($rootScope,$location,authService) {
    if (!authService.isLoggedIn()) {
        $rootScope.redirect = $location.path();
        $location.path("/login");
    }
}

mvcapp.run(['$rootScope', '$location', '$http', 'authService', function ($rootScope, $location, $http, authService) {
    if (authService.isLoggedIn())
    {
        $http.defaults.headers.common['authorization'] = 'Bearer ' + authService.getAuthCode().Authcode;
    }
}]);

mvcapp.factory('httpRequest', ['$q', 'authService', function ($q, authService) {

    var _interceptors = {
        request: function (config) {
            if (angular.isObject(config)) {
                config.delay = new Date().getTime();
                var loggedin = authService.getAuthCode();

                if (loggedin != null)
                    config.headers["authorization"] = "Bearer " + loggedin.Authcode;

            }
            return config;
        },
        response: function (response) {
            if (angular.isObject(response.config)) {
                response.config.delay = new Date().getTime() - response.config.delay;
            }
            return response;
        },
        requestError: function (config) {
            if (angular.isObject(config)) {
                config.delay = new Date().getTime();
            }
            return $q.reject(config);
        },
        responseError: function (response) {
            if (angular.isObject(response.config)) {
                response.config.delay = new Date().getTime() - response.config.delay;
            }
            return $q.reject(response);
        }
    };

    return _interceptors;

}]);

mvcapp.config(['$httpProvider', function ($httpProvider) {
    $httpProvider.interceptors.push('httpRequest');
}]);
