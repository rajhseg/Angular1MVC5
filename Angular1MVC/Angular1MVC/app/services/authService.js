mvcapp.service('authService', ['$localStorage', '$rootScope',function ($localStorage,$rootScope) {

    this.getAuthCode = function () {
        return $localStorage.authuser;
    }

    this.setAuthCode = function (authdata) {
        $localStorage.authuser = authdata;
    }

    this.removeAuthCode = function () {
        delete $localStorage.authuser;        
        var loggeduser = { Status: '', Username: '', Authcode: '' };
        $rootScope.$broadcast('loguser', loggeduser);
    }

    this.isLoggedIn = function () {
        return $localStorage.authuser != null && $localStorage.authuser.Authcode != null;
    }


}]);