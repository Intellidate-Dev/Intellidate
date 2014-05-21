

//Get and set Email Id
        $(document).ready(function () {
            $.postDATA("Service.ashx?f=GAD", "{}", function (data) {
                $("#txtEmail").val(data.EmailID);
            });
        });



//Change Email Id
$(document).ready(function () {

    $("#btnChangeEmail").click(function () {

        $("#pEmailMsg").html("");
        $("#pPwdMsg").html("");

        var _email = $("#txtEmail").val().trim();
        var dotpos = _email.lastIndexOf(".");
        var atpos = _email.indexOf("@");
        if (_email == "") {
            $("#pEmailMsg").html("<li>E-mail address should not be empty</li>");
            return false;
        }
        if (atpos < 1 || dotpos < atpos + 2 || dotpos + 2 >= _email.length) {
            $("#pEmailMsg").html("<li>Not a valid e-mail address</li>");
            $('#txtEmail').focus();
            _status = false;
            return false;
        }
        else {
                    
            var _frmdata = "EmailId=" + _email;
            $.postDATA("Service.ashx?f=CAE", _frmdata, function (data) {
                if (data) {
                    $("#pEmailMsg").html("<li> Email address successfully updated </li>");
                }
                else {
                    $("#pEmailMsg").html("<li> Email address not updated </li>");
                }
            });
        }

    });
});



//Change password
$(document).ready(function () {

    $("#btnChangePassword").click(function () {

        $("#pPwdMsg").html("");
        $("#pEmailMsg").html("");

        if ($("#txtPassword").val().trim() == "") {
            $("#pPwdMsg").html("<li>New Password should not be empty </li>");
            return false;
        }
        if ($("#txtRPassword").val().trim() == "") {
            $("#pPwdMsg").html("<li>Retype Password should not be empty </li>");
            return false;
        }
        if ($("#txtPassword").val().trim().length < 4) {
            $("#pPwdMsg").html("<li>Password minimum 4 charectors or above</li>");
            return false;
        }
        if ($("#txtPassword").val().trim() != $("#txtRPassword").val().trim()) {
            $("#pPwdMsg").html("<li>Password and Retype password should be same </li>");
            return false;
        }
        else {
            var _frmdata = "Password=" + $("#txtPassword").val().trim();
            $.postDATA("Service.ashx?f=CAP", _frmdata, function (data) {
                if (data) {
                    $("#pPwdMsg").html("<li> Password successfully updated </li>");
                }
                else {
                    $("#pPwdMsg").html("<li> Password not updated </li>");
                }
            });
        }
                
    });
});
