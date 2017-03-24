mvcapp.factory('DbFactory', ['$http','$location','$rootScope',function ($http,$location,$rootScope) {

    var service = {
        login: Login,
        register: Register,
        get: Get,
        post: Post,
        GetUserInfo: GetUserInfo,
        UpdateProfile: UpdateProfile,
        UpdatePassword: updatePassword,
        DeActivate: DeActivate
    };

    function Login(url, data, success, failure) {
        Post(url, data, success, failure);
    }

    function UpdateProfile(url, data, success, failure) {
        Post(url, data, success, failure);
    }

    function DeActivate(url, data, success, failure) {
        Post(url, data, success, failure);
    }

    function Register(url, data, success, failure) {
        var registerdata = {};

        registerdata.UserName = data.username;
        registerdata.DisplayName = data.displayname;
        registerdata.Password = data.password;
        registerdata.Email = data.email;
        registerdata.phone =  parseInt(data.phone,10);

        Post(url, registerdata, success, failure);
    }

    function GetUserInfo(url, success, failure) {
        return Get(url, {}, success, failure);
    }

    function updatePassword(url, data, scallback, fcallback) {
        return Post(url, data, scallback, fcallback);
    }

    function Get(url, config, success, failure) {
        return $http.get(url, config).then(function (result) { success(result) }, function (error) {
            if (error.status == "401") {
                $rootScope.previous = $location.path();
                $location.path("/login");
            } else {
                failure(error);
            }
        })
    }

    function Post(url,data,success,failure) {
        return $http.post(url, data)
            .then(function (result)
            {
                success(result)
            },
            function (error) {
            if (error.status == "401") {
                $rootScope.previous = $location.path();
                $location.path("/login");
            } else {
                failure(error);
            }
        })
    }

    return service;
}])