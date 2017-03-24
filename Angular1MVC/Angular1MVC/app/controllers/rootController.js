mvcapp.controller('rootController', ['$scope', '$rootScope', 'authService', function ($scope, $rootScope, authService) {

    $scope.loggeduser = { Status : '', Username :'', Authcode:'', Role :'' };

    $scope.$on('loguser', function (evt, user) {
        $scope.loggeduser = user;
    });

}]);