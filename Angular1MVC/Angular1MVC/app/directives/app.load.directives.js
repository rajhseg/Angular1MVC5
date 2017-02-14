/// <reference path="../../Scripts/angular.js" />
/// <reference path="../app.js" />

mvcapp.directive('appLoad', [function () {
	return {
		restrict: 'E',
		templateUrl: 'app/templates/app-load.html',
		link: function (scope,element,attr,cntrl) {

		}
	}
}]);

mvcapp.directive('topNavbar', [function () {
	return {
		restrict: 'E',
        templateUrl: 'app/templates/top-navbar.html',
		link: function (scope, element, attr, cntrl) {

		}
	}
}]);



mvcapp.directive('sideNavbar', ['$location', 'authService', function ($location, authService) {
	return {
		restrict: 'E',
        templateUrl: 'app/templates/side-navbar.html',
		link: function (scope, element, attr, cntrl) {
            scope.logout = function () {
                authService.removeAuthCode();
                $location.path('/login');
            }
		}
	}
}]);

mvcapp.directive('loginForm', ['$location',function ($location) {
    return {
        restrict: 'E',
        scope: {
            user: '=',
            submit:'&'
        },
        templateUrl: 'app/templates/login-form.html',
        link: function (scope,element,attr,cntrl) {
            scope.login = function () {
                scope.submit({ user: scope.user });
            }

            scope.register = function () {
                $location.path('/register');
            }
        }
    }
}]);


mvcapp.directive('validateInvalid', function () {
    return {
        restrict: "A",
        link: function (scope, element, attrs) {

            if (scope.form == undefined)
                scope.form = {};

            element.addClass('ng-invalid');

            scope.form.enableSubmit = false;

            scope.$watch(function () {
                return element.attr('class');
            }, function (val) {
                var valid = true;
                var classes = val.split(" ");

                angular.forEach(classes, function (classname) {
                    if (classname == "ng-invalid") {
                        valid = false;
                    }
                });

                if (valid)
                    scope.form.enableSubmit = true;
                else
                    scope.form.enableSubmit = false;

            });
        }
    }

});