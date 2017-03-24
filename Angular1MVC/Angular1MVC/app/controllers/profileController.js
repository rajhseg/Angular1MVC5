mvcapp.controller('profileController', ['$scope', 'DbFactory', function ($scope, DbFactory) {

    $scope.isEdit = false;
    $scope.user = {};
    $scope.ShowError = false;
  
    $scope.userInfoSuccess = function (result) {
        $scope.user = result.data.user;
    }

    $scope.userInfoFailed = function (result) {
        console.log(result);
    }

    $scope.Edit = function () {
        $scope.isEdit = true;
    }

    $scope.Save = function (validForm) {
        if (!validForm) {
            $scope.ShowError = true;
            return;
        }
        $scope.ShowError = false;
        DbFactory.UpdateProfile('api/user/edituser', $scope.user, $scope.editSucceded, $scope.editFailed);       
    }

    $scope.editSucceded = function (result) {
        $scope.user = result.data.user;
        $scope.isEdit = false;
    }

    $scope.editFailed = function (result) {

    }

  DbFactory.GetUserInfo('api/user/getbyid', $scope.userInfoSuccess, $scope.userInfoFailed);


}]);