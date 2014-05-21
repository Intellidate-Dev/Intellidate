
//constants
var NOTI_USERID = 1;
var NOTI_APIMESSAGE = "../API/UserNotifications/";
var NOTI_APINOTI = "../API/UserNotifications?UserId=" + NOTI_USERID;
var NOTI_SPANMSG = "#spanMsg";
var NOTI_GNMMTD = "GNM";

//set message count
$(document).ready(function () {
    var _obj = new Object();
    _obj.MethodName = NOTI_GNMMTD;
    _obj.UserId = NOTI_USERID;
    $(NOTI_SPANMSG).text(NULLSTRNG);

    //get new number of unread messages
    $.postDATA(NOTI_APIMESSAGE, _obj, function (ret) {
        $(NOTI_SPANMSG).text(ret);
    }, function () {

    });



    //get number of unread notifications

    $.getJSON(NOTI_APINOTI)
         .done(function (data) {
             alert(data);
         })
         .fail(function (jqXHR, textStatus, err) {
            
         });



});
