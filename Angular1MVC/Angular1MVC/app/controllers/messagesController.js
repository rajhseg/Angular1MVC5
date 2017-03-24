mvcapp.controller('messagesController', ['$scope', '$upload', function ($scope, $upload) {

    $scope.filedata = {};
    $scope.FilesData = [];

    $scope.success = function (data, status, headers, config) {
        console.log('config');
        console.log(config);
        $scope.FilesData.push(data);
    }

    $scope.error = function (data, status, headers, config) {
        console.log('error');
        console.log(config);
    }

    $scope.uploadfiles = function (files) {
        console.log("Files select callback");
        console.log(files);
    }

}]);