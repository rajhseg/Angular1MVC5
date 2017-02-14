mvcapp.controller('registerController', ['$scope', 'DbFactory', '$location', 'notifyService', function ($scope, DbFactory, $location, notifyService) {

    $scope.user = {};
    $scope.ShowError = false;

    $scope.register = function (validForm) {
        if(!validForm) {
            $scope.ShowError = true;
            return;
        }
        $scope.ShowError = false;        
        DbFactory.register('api/account/adduser', $scope.user, $scope.registerSucceded, $scope.registerFailed);
    }

    $scope.clear = function () {
        $scope.user = {};
        console.log($scope.user);
    }

    $scope.registerSucceded = function (result) {
        if (result.data.status == true) {
            notifyService.Success('', 'User creation is successful');
            $location.path("/login");
        }
    }

    $scope.registerFailed = function (result) {
        if (angular.isObject(result.data)) {
            notifyService.Success('', result.statusText);
        } else {
            notifyService.Success('', result.data);
        }               
    }

}]);