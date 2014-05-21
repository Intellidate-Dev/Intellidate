$(document).ready(function () {
    $("#btnSendPassword").bind("click", function () {
        var _email = $("#txtEmail").val();
        if (_email == NULLSTRNG) {
            return;
        }
        var _obj = new Object();
        _obj.EmailAddress = _email;

        var _text = $(".login-button").html();
        $(".login-button").html("Processing");

        $("#txtEmail").prop("disabled", true);
        $(this).prop("disabled", true);

        $.postDATA(APICALLFORGOTPASSWORD, _obj, function (ret) {
            $("#txtEmail").prop("disabled", false);
            $("#btnSendPassword").prop("disabled", false);
            $(".login-button").html("Sent");
            setTimeout(function () {
                $(".login-button").html(_text);
            }, 2000);

        }, function () { });
    });
});