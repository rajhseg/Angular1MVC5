
mvcapp.service('notifyService', ['$notification', function ($notification) {


    this.Success = function (title,message) {
        var result = $notification.open(
            {
                position: "bottom-right",
                title: title,
                message: '<div style="padding:10px;text-align:center">'+message+'</div>',
                notificationType: $notification.notificationType.success,
                color: 'white',
                closebutton: true,
                showspeed: 500,
                hidespeed: 3000,
                showeasing: 'linear'
            });
    };

    this.Error = function (title,message) {
        var result = $notification.open(
            {
                position: "bottom-right",
                title: title,
                message: '<div style="padding:10px;text-align:center"><div>' + message + '</div></div>',
                notificationType: $notification.notificationType.error,
                color: 'white',
                closebutton: true,
                showspeed: 1000,
                hidespeed: 3000,
                showeasing: 'linear'
            });
    };

    this.Warning = function (title,message) {
        var result = $notification.open(
            {
                position: "bottom-right",
                title: title,
                message: '<div style="padding:10px;text-align:center">' + message + '</div>',
                notificationType: $notification.notificationType.warning,
                color: 'white',
                closebutton: true,
                showspeed: 1000,
                hidespeed: 4000,
                showeasing: 'linear'
            });
    };

}]);