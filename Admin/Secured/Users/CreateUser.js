
//ready function for bind datepicker and check email
$(document).ready(function () {
    $("#txtDob").datepicker({
        changeMonth: true,
        changeYear: true
    });

    $("#txtEmail").change(function () {
        checkmail();
    });

    function checkmail() {
        var _email = $("#txtEmail").val().trim();
        var dotpos = _email.lastIndexOf(".");
        var atpos = _email.indexOf("@");
        var status = false;
        if (_email == "") {
            $("#pMsg").html("<li>Email should not be empty</li>");
            $('#txtEmail').focus();
            status = false;
            return false;
        }
        else {
            status = true;
            $("#pMsg").html("");
        }
        if (atpos < 1 || dotpos < atpos + 2 || dotpos + 2 >= _email.length) {

            $("#pMsg").html($("#pMsg").html() + "<li>Not a valid e-mail address</li>");
            $('#txtEmail').focus();
            status = false;
            return false;
        }
        else {
            status = true;
            $("#pMsg").html("");
        }

        if (status) {
            var _email = $("#txtEmail").val().trim();
            var _frmdata="Type=C&EmailId=" + _email + "&UserId=0";
            $.postDATA("Service.ashx?method=CheckEmailAddress", _frmdata, function (data) {
                $("#hdnValue").val(data);
                    if (!data) {
                        $("#pMsg").html($("#pMsg").html() + "<li>Email-id already exists try another</li>");
                    } else {
                        $("#pMsg").html("");
                    }
            });

        }
    }

});


//ready function for validate user details
$(document).ready(function () {

    $("#cmdCreateUser").click(function () {
        //validation

        $("#pMsg").html("");
        var _status = false;
        var _email = $("#txtEmail").val().trim();
        var dotpos = _email.lastIndexOf(".");
        var atpos = _email.indexOf("@");

        if ($("#txtLogin").val().trim() == "") {
            $("#pMsg").html($("#pMsg").html() + "<li>Login name should not be empty </li>");
            _status = false;
            return false;
        }
        else {
            $("#pMsg").html("");
            _status = true;
        }


        if ($("#txtFullName").val().trim() == "") {
            $("#pMsg").html($("#pMsg").html() + "<li>Full name should not be empty </li>");
            _status = false;
            return false;
        } else {
            $("#pMsg").html("");
            _status = true;
        }


        if (_email == "") {
            $("#pMsg").html("<li>Email should not be empty</li>");
            $('#txtEmail').focus();
            _status = false;
            return false;
        } else {
            $("#pMsg").html("");
            _status = true;
        }


        if (atpos < 1 || dotpos < atpos + 2 || dotpos + 2 >= _email.length) {
            $("#pMsg").html($("#pMsg").html() + "<li>Not a valid e-mail address</li>");
            $('#txtEmail').focus();
            _status = false;
            return false;
        }
        else {
            $("#pMsg").html("");
            _status = true;
        }


        if ($("#txtPassword").val().trim() == "") {
            $("#pMsg").html($("#pMsg").html() + "<li>Password should not be empty </li>");
            _status = false;
            return false;
        } else {
            $("#pMsg").html("");
            _status = true;
        }


        if ($("#txtPassword").val().trim().length <= 6) {
            $("#pMsg").html($("#pMsg").html() + "<li>Password should be minimum 6 characters </li>");
            _status = false;
            return false;
        } else {
            $("#pMsg").html("");
            _status = true;
        }


        if ($("#txtConfirmPwd").val().trim() == "") {
            $("#pMsg").html($("#pMsg").html() + "<li>confirm password should not be empty </li>");
            _status = false;
            return false;
        } else {
            $("#pMsg").html("");
            _status = true;
        }


        if ($("#txtConfirmPwd").val().trim() != $("#txtPassword").val().trim()) {
            $("#pMsg").html($("#pMsg").html() + "<li>Password and confirm password should be same</li>");
            _status = false;
            return false;
        } else {
            $("#pMsg").html("");
            _status = true;
        }


        if (jQuery("#ddlGender option:selected").val().trim() == "0") {
            $("#pMsg").html($("#pMsg").html() + "<li>Please select gender</li>");
            _status = false;
            return false;
        } else {
            $("#pMsg").html("");
            _status = true;
        }


        if ($("#txtDob").val().trim() == "") {
            $("#pMsg").html($("#pMsg").html() + "<li>Date of birth should not be empty</li>");
            _status = false;
            return false;
        } else {
            $("#pMsg").html("");
            _status = true;
        }


        if (_status) {
            var _res = $("#hdnValue").val();
           // alert(_res);
            if (_res == "true") {
                var dobValue = $("#txtDob").val();
                var _frmdata= "LoginName=" + $("#txtLogin").val() + "&FullName=" + $("#txtFullName").val() + "&EmailAddress=" + $("#txtEmail").val() + "&Password=" + $("#txtConfirmPwd").val() + "&Gender=" + jQuery("#ddlGender option:selected").val() + "&Dob=" + dobValue;               
                $.postDATA("Service.ashx?method=AddNewUser",_frmdata, function (data) {     
                        if (data) {
                            $("#pMsg").html("<li>New user inserted successfully</li>");
                        }
                        else {
                            $("#pMsg").html("<li>New user insertion field</li>");
                        }
                });
            }
            else {
                $("#pMsg").html($("#pMsg").html() + "<li>Email-id already exists try another</li>");
            }
        }
    });


});