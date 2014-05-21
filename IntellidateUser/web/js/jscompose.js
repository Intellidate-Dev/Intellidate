
//constants
var APICOMPOSEBOX = "../API/ComposeBox/";
var COM_DVMESSAGE = "#dvMessage";
var COM_BTNDISCARD = "#btnDiscard";
var PROP_DISABLED = "disabled";
var COM_SMM_METHOD = "SMM";
var COM_SMR_METHOD = "SMR";
var COM_VSM_METHOD = "VSM";
var COM_DSM_METHOD = "DSM";
var COM_ATTR_DVCE = "contenteditable";

var COM_SENDERID = 1; 
var COM_RECIPIENTID = 2;
var COM_ISRECIPIENTLOGIN = true;

    $(document).ready(function () {

        //change text color
        $(COM_COLORSELECTOR).colorselector({
            callback: function (value, color, title) {
                formatColorDoc(COM_FORECOLOR, color);
            },
        });

        // insertImage
        $(COM_EMOTICON_ICONS).colorselector({
            callback: function (value, color, title) {
                var _imgurl = value;
                formatColorDoc(COM_INSERTIMG, _imgurl);
            }
        });

        //univarsal method for change text color and  insertImage
        function formatColorDoc(sCmd, sValue) {
            var oDoc;
            oDoc = document.getElementById(COM_DVCONTENT);
            document.execCommand(sCmd, false, sValue); oDoc.focus();
        }

        //to change smileys while user enter smileys symbols
        $(COM_DVCONTENTID).keypress(function () {
            var _content = $(COM_DVCONTENTID).html();
            var _replacedcontent = replaceEmoticons(_content);
            var _IsEmoticon = matchEmoticon($(COM_DVCONTENTID).text());
            if (_IsEmoticon) {
                $(COM_DVCONTENTID).html(NULLSTRNG);
                formatColorDoc(COM_INSERTHTML, _replacedcontent);
            }
            $(COM_DVMESSAGE).hide();

            //validate numbers   var _IsNumber = 
            _IsNumber = ValidateMobileNumber($(COM_DVCONTENTID).text());
            if (_IsNumber) {
                //save message into redis db as draft
                var _obj = new Object();
                _obj.SenderId = COM_SENDERID;
                _obj.RecipientId = COM_RECIPIENTID;
                _obj.MessageText = _replacedcontent;
                _obj.MethodName = COM_SMR_METHOD;
                $.postDATA(APICOMPOSEBOX, _obj, function (ret) {
                    $(COM_BTN_SEND).prop(PROP_DISABLED, false);
                    //check if message saved or not in redis
                }, function () {

                });

            } else {
                //alert(_IsNumber);
                _replacedcontent = _replacedcontent.replace(/\d+/g, '')
                $(COM_DVCONTENTID).html(NULLSTRNG);
                formatColorDoc(COM_INSERTHTML, _replacedcontent);
            }
        })

        //replacing emoticons based on user enterd symbols
        function replaceEmoticons(text) {  
            // a simple regex to match the characters used in the emoticons
            return text.replace(/[:\-)D:(X(;:P)]+/g, function (match) {
                return typeof COM_EMOTICONS[match] != 'undefined' ?
                       ' <img src="' + COM_EMOTICONS[match] + '"/> ' :
                       match;
            });
        }

        //validate if user enter 3 numaric values. we don't allow 3 numbers at a time 
        var status = 0;
        function ValidateMobileNumber(text) {
            var res = true;
            if (text.match(/\d+/g))
            {
                status = status + 1;
                if (status > 1) {
                    res = false;
                    status = 0;
                }
            }
            else {
                status = 0;
            }
            return res;
        }

        //validate user enterd symbols
        function matchEmoticon(text) {
            var _res = false;
            var regex = /[:\-)D]+/g;
            if (regex.test(text)) {
                _res = true;
            }
            else {
                _res = false;
            }
            return _res;
        }

    });

$(document).ready(function () {

    //to change font weight bold and un bold
    $(COM_A_BOLD).click(function () {
        formatDoc(COM_BOLD, true);
    });

    //to apply and remove underline to selected font
    $(COM_A_UNDERLINE).click(function () {
        formatDoc(COM_UNDERLINE, true);
    });

    //to apply and remove italic to selected font
    $(COM_A_ITALIC).click(function () {
        formatDoc(COM_ITALIC, true);
    });

    //main function for text editor
    function formatDoc(sCmd, sValue) {
        var oDoc;
        oDoc = document.getElementById(COM_DVCONTENT);
        document.execCommand(sCmd, false, sValue); oDoc.focus();
    }

    // formatDoc('fontname','Arial'); --to change font name
    // formatDoc('fontsize','1'); --to change font size
    // formatDoc('forecolor','red'); --to change font fore color
    // formatDoc('backcolor','red'); --to change font back color


    // formatDoc('removeFormat'); --to remove formatting
    // formatDoc('justifyleft'); --to justify left
    // formatDoc('insertorderedlist'); --to insert ordered list
    // formatDoc('insertunorderedlist'); --to insert unordered list
    // var sLnk=prompt('Write the URL here','http:\/\/');if(sLnk&&sLnk!=''&&sLnk!='http://'){formatDoc('createlink',sLnk)}" -to add link

});


//sent message code
$(document).ready(function () {

    $(COM_DVMESSAGE).hide(); 
    //send button click event
    $(COM_BTN_SEND).bind(COM_CLICK, function () {
        $(COM_BTN_SEND).prop(PROP_DISABLED, true);
        var _htmlMessage = $(COM_DVCONTENTID).html();
        if (_htmlMessage.trim() == NULLSTRNG || _htmlMessage == null) {
            alert(COM_ERR_MESSAGE);
            $(COM_BTN_SEND).prop(PROP_DISABLED, false);
            return false;
        }
        else {
            var _obj = new Object();
            _obj.SenderId = COM_SENDERID;
            _obj.RecipientId = COM_RECIPIENTID;
            _obj.MessageText = _htmlMessage;
            _obj.MethodName = COM_SMM_METHOD;
            $.postDATA(APICOMPOSEBOX, _obj, function (ret) {
                if (ret) {
                    $(COM_BTN_SEND).prop(PROP_DISABLED, false);
                    $(COM_DVMESSAGE).show();
                    $(COM_DVMESSAGE).text(COM_MESSAGESENT);
                    $(COM_DVCONTENTID).html(NULLSTRNG);
                }
                else {
                    $(COM_BTN_SEND).prop(PROP_DISABLED, true);
                    $(COM_DVMESSAGE).show();                   
                    $(COM_DVMESSAGE).text(COM_ERR_MSGREPLY);
                }
            }, function () {

            });
        }
    });

    //to hide the compose box
    $(COM_BTN_CLOSE).bind(COM_CLICK, function () {
        $(COM_DVMSG_BOX).hide();
    });

});

//validate user able to send message to recipient
$(document).ready(function () {
    var _obj = new Object();
    _obj.SenderId = COM_SENDERID;
    _obj.RecipientId = COM_RECIPIENTID;
    _obj.MessageText = NULLSTRNG;
    _obj.MethodName = COM_VSM_METHOD; //validate sender message method
    $.postDATA(APICOMPOSEBOX, _obj, function (ret) {
        if (!ret) {
            $(COM_DVCONTENTID).attr(COM_ATTR_DVCE, false);
            $(COM_BTN_SEND).prop(PROP_DISABLED, true);
            $(COM_DVMESSAGE).show();
            $(COM_DVMESSAGE).text(COM_ERR_MSGREPLY);
        } else {
            $(COM_DVCONTENTID).attr(COM_ATTR_DVCE, true);
            $(COM_BTN_SEND).prop(PROP_DISABLED, false);
            $(COM_DVMESSAGE).hide();
        }
    }, function () {

    });



});

//send notification to online user if user is not in online then send email 
$(document).ready(function () {





});

//Discard message button click event 
$(document).ready(function () {
    //to clear redis db draft message
    $(COM_BTNDISCARD).bind(COM_CLICK, function () {
        var _obj = new Object();
        _obj.SenderId = COM_SENDERID;
        _obj.RecipientId = COM_RECIPIENTID;
        _obj.MessageText = NULLSTRNG;
        _obj.MethodName = COM_DSM_METHOD;
        $.postDATA(APICOMPOSEBOX, _obj, function (ret) {
            if (ret) {

            }
        });

    });



});