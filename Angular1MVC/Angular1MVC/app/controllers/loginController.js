﻿/// <reference path="../../Scripts/angular.js" />

mvcapp.controller('loginController', ['$scope', '$state','$rootScope', 'DbFactory', 'authService', 'notifyService', '$location',
        function ($scope,$state, $rootScope,DbFactory, authService, notifyService, $location) {

    

    $scope.login = function (user) {
        DbFactory.login('api/account/login', user, $scope.LoginSucceded, $scope.LoginFailure);
    }


    $scope.LoginSucceded = function (result) {
        authService.setAuthCode(result.data);  
        var loggeduser = authService.getAuthCode();  
        $rootScope.$broadcast('loguser', loggeduser);
        if (result.data.Status) {
            notifyService.Success('', 'Login successful');

            if ($rootScope.redirect != null) {
                $state.transitionTo($rootScope.redirect);
                $rootScope.redirect = null;
                return;
            } else {
                $state.transitionTo('home');
            }
        }
        else
            notifyService.Error('', 'Login Failed for ' + result.data.Username);
        $scope.userinfo = {};
    }

    $scope.LoginFailure = function (result) {
        console.log("login failure");
        notifyService.Error('', 'Authentication failed');
        $scope.userinfo = {};
    }

    
}])