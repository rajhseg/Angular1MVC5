/**
 * Created by Rajesh on 01-11-2014.
 */

var angularNotification = angular.module('angularNotification',[]);

angularNotification.factory('$notification', function ($timeout,$compile) {

    var factory = {};

    var template= '<div id="[notifyid]" class="notifyinnercontainer">' +
        '<div  style="background-color:[notificationbgcolor];color:[color];display: inline-block">'+
        '<div class="notifytitle">[title]</div>'+
        '<div class="closenotify"><div id="[closeid]" class="notifyclosebtn">&times;</div></div>'+
        '<div id="[msgid]" style="float: left;font-size:12px">[message]</div>' +
        '</div>' +
        '</div>';

    var containertemplate = '<div id="notifycontainer" style="z-index:999999;width:250px;position: fixed;">';

    var id='notificationid';
    var closeid='closenotid';
    var msgid = 'notifymsgid';
    var i=0;
    var height = 0;

    function Initoptions(){
        return{
            position: "bottom-right",
            title:'',
            message:'',
            notificationType:'#285e8e',
            color:'white',
            closebutton:true,
            showspeed:350,
            hidespeed:6000,
            showeasing:'linear'
        };
    }

    factory.notificationType={
        success:'#168824',
        error:'#E73114',
        information:'#285e8e',
        warning:'#fc9503',
        alert:'#E04551',
        normal:'#ffffff',
        dark:'#000000',
        light:'#7B8C9D',
        primary:'#45BDE0'
    };

    factory.open= function (overrideoptions) {
        var result={};
        var options = angular.extend({},Initoptions(),overrideoptions);
        var container = null;
        var notifytemplate = template;

        i=i+1;


        if(options.notificationType=='#ffffff' || options.notificationType =='white'){
            options.color = 'black';
        }
        notifytemplate=notifytemplate.replace('[notifyid]',(id+i));
        notifytemplate = notifytemplate.replace('[closeid]',(closeid+i));
        notifytemplate = notifytemplate.replace('[msgid]',(msgid+i));
        notifytemplate = notifytemplate.replace('[notificationbgcolor]',options.notificationType);
        notifytemplate = notifytemplate.replace('[color]',options.color);
        notifytemplate = notifytemplate.replace('[title]',options.title);
        notifytemplate = notifytemplate.replace('[message]',options.message);

        var $bodyelement = angular.element('body');
        var $scope = $bodyelement.scope();
        var tileNotify = $compile(notifytemplate)($scope); //angular.element(notifytemplate);
        container = angular.element('#notifycontainer');

        if(container.length > 0){
            container.prepend(tileNotify);
        }
        else {
            container = $compile(containertemplate)($scope);
            container.append(tileNotify);
            container.appendTo(angular.element('body'));
        }

        var xplace = options.position.split('-')[0] !='' ?  options.position.split('-')[0] : "bottom";
        var yplace = options.position.split('-')[1] != '' ? options.position.split('-')[1] : "right";
        container.css(xplace,'12px').css(yplace,'12px');


        if(options.title==''){
            $('#'+msgid+i).css('padding-right','12px').css('margin-top','3px');
        }else{
            $('#'+msgid+i).css('margin-top','-2px');
        }

        if(!options.closebutton){
            $('#'+(closeid+i)).parent().css('display','none');
            $('#'+msgid+i).css('padding-right','1px')
        }

        $('#'+(closeid+i)).click(function(){
            $(this).parent().parent().parent().hide("slow","linear",function(){

                $(this).remove();
            });

        });

        var cid = '#'+(id+i);
        var ismouseover = false;

        angular.element(cid).bind('mouseover', function (event) {
            ismouseover = true;
            $(cid).stop();
            $(cid).css('opacity','1');
        });

        angular.element(cid).bind('mouseleave', function (event) {
            ismouseover = false;
            setTimeout(function(){
                fadeoutdiv();
            },options.hidespeed);
        });

        var fadeoutdiv = function () {
            if(!ismouseover) {
                $(cid).fadeOut("slow",
                    function () {
                        $(cid).remove();
                    });
            }
        };

        $(cid).show(options.showspeed,options.showeasing,function(){
            setTimeout(function(){
                fadeoutdiv();
            },options.hidespeed);
        });

        angular.element(cid).css('cursor','pointer');
        result.messageScope = $scope;
        result.containerId = '#notifycontainer';
        result.notificationId = (id+i);
        result.closeId = (closeid+i);
        result.messageId = (msgid+i);
        result.jObjectContainer = $(result.containerId);
        result.angularContainer = container;

        return result;

    };

    return factory;
});