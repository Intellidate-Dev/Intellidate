

$(document).ready(function () {
    $(LOGIN_ERR).hide();
    $(REG_USERNAMETXT).select();
})


$(document).ready(function () {
    $(LOGIN_LNKFPWD).click(function () {
        $(LOGIN_DVFPWD).css({ "display" : "block" });
    });
});

function ValidateUser() {

    var _unm = $(REG_USERNAMETXT).val().trim();
    var _pwd = $(REG_PASSWORDTXT).val().trim();

    var _result = false;
    if (_unm == NULLSTRNG) {
        $(LOGIN_ERR).show(100);
        $(LOGIN_ERRMSG).text(REG_ERR_BLNKUNAME);

        return false;
    } else {
        $(LOGIN_ERR).hide(100);
        _result = true;
    }

    if (_pwd == NULLSTRNG) {
        $(LOGIN_ERR).show(100);
        $(LOGIN_ERRMSG).text(REG_ERR_BLANKPASSWORD);
        return false;
    }
    else {
        $(LOGIN_ERR).hide(100);
        _result = true;
    }
    return _result;
}

