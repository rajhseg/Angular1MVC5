mvcapp.controller('passwordController', ['$scope', 'DbFactory', 'authService', '$rootScope', 'notifyService', '$state','$location',
    function ($scope, DbFactory, authService, $rootScope, notifyService, $state, $location) {

    $scope.ShowError = false;
    $scope.user = {};

    $scope.changePassword = function (validForm) {

        if (!validForm) {
            $scope.ShowError = true;
            return;
        }

        DbFactory.UpdatePassword('api/user/updatepassword',
        {
            oldpassword: $scope.user.oldpassword,
            newpassword: $scope.user.newpassword,
            renewpassword: $scope.user.renewpassword
        },
            $scope.changeSuccess,
            $scope.changeFailed);    
          
    }

    $scope.deactivate = function () {
        DbFactory.DeActivate('api/user/deactivate', {}, $scope.deactivateSuccess, $scope.deactivateFailure)
    }

    $scope.deactivateSuccess = function (result) {
        notifyService.Success('', 'Deactivation Done');
        $location.path("/register");

    }

    $scope.deactivateFailure = function (result) {
        notifyService.Error('', 'Unauthorized, Deactivation Failed');
        $location.path("/login");
    }

    $scope.changeSuccess = function (result) {
        authService.setAuthCode(result.data);
        var loggeduser = authService.getAuthCode();
        $rootScope.$broadcast('loguser', loggeduser);     
        notifyService.Success('','Password changed successfully');
    }

    $scope.changeFailed = function (result) {
        notifyService.Error('','Password change failed');
    }

}]);