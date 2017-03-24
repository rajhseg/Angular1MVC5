/// <reference path="../../Scripts/angular.js" />
/// <reference path="../../Scripts/angular-route.js" />

var mvcapp = angular.module('mvcapp', ['ui.router', 'ngCookies', 'ngStorage', 'angularNotification','angularFileUpload']);

mvcapp.config(['$stateProvider', '$urlRouterProvider', '$localStorageProvider', '$locationProvider', function ($stateProvider, $urlRouterProvider, $localStorageProvider, $locationProvider) {


    $stateProvider.state('home', { name: 'home', url: '/home', templateUrl: 'app/templates/home.html', controller: 'homeController' })
        .state('about', { name: 'about', url: '/about', templateUrl: 'app/templates/about.html', controller: 'aboutController' })
        .state('login', { name: 'login', url: '/login', templateUrl: 'app/templates/login.html', controller: 'loginController', resolve: { signIn: AlreadySignin } })
        .state('register', { name: 'register', url: '/register', templateUrl: 'app/templates/register.html', controller: 'registerController', resolve: { signIn: AlreadySignin } })
        .state('profile', { name: 'profile', url: '/profile', templateUrl: 'app/templates/profile.html', controller: 'profileController', authenticate: true})
        .state('history', { name: 'history', url: '/history', templateUrl: 'app/templates/history.html', controller: 'historyController', authenticate: true })
        .state('addproduct', { name: 'addproduct', url: '/addproduct', templateUrl: 'app/templates/messages.html', controller: 'messagesController', authenticate: true })
        .state("default", { name: 'default', url: '/', templateUrl: 'app/templates/home.html', controller: 'homeController' })
        .state("changepassword", { name: 'changepassword', url: '/changepassword', templateUrl: 'app/templates/password.html', controller: 'passwordController', authenticate: true })
        .state("deactivate", { name: 'deactivate', url: '/deactivate', templateUrl: 'app/templates/deactivate.html', controller: 'passwordController', authenticate: true });

    $urlRouterProvider.otherwise('/');

    $localStorageProvider.setKeyPrefix('mvcapp');
    $locationProvider.html5Mode(false);

}]);

mvcapp.config(['$provide',function ($provide) {
    $provide.decorator('$exceptionHandler', ['$delegate', '$log', function ($delegate, $log) {
        return function (exception, cause) {
            $delegate(exception, cause);
            $log.info(exception.message + " (because of " + cause + " )");
        };
    }])
}]);


AlreadySignin.$inject = ['$rootScope','$state', '$location', 'authService'];
function AlreadySignin($rootScope, $state,$location, authService) {
    if (authService.isLoggedIn()) {       
        return $location.path("/home");       
    }
}


mvcapp.run(['$rootScope', '$location', '$http', 'authService', '$state', '$timeout',
    function ($rootScope, $location, $http, authService, $state, $timeout) {

    if (authService.isLoggedIn())
    {
        $http.defaults.headers.common['Authorization'] = 'Bearer ' + authService.getAuthCode().Authcode;
    }

    function safeApply(scope, fn) {
        (scope.$$phase || scope.$root.$$phase) ? fn() : scope.$apply(fn);
    }

    $rootScope.$on('$stateChangeStart', function (event, toState, toParams, fromState, fromParams) {

        if (toState.authenticate && !authService.isLoggedIn()) {
            // User isn’t authenticated
            $state.transitionTo("login");            
            $rootScope.redirect = toState.name;
            event.preventDefault();
        }

        if (toState.authenticate && authService.isLoggedIn()) {
            var role = authService.getAuthCode().Role;
            
        }

        safeApply($rootScope, function () {
            $timeout(function () {
                $rootScope.$broadcast('updatelogin');
            })           
        });

        

    });

}]);

mvcapp.factory('httpRequest', ['$q', 'authService', function ($q, authService) {

    var _interceptors = {
        request: function (config) {
            if (angular.isObject(config)) {
                config.delay = new Date().getTime();
                var loggedin = authService.getAuthCode();

                if (loggedin!=undefined && loggedin.Authcode != null)
                    config.headers["Authorization"] = "Bearer " + loggedin.Authcode;

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
