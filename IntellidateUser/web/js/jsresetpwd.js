
$(document).ready(function () {
    $(RESET_ERR).hide();
    });

function ValidatePassword() {
    $(LABEL_MSG).hide();
    var _pwd = $(RESET_TXTPWD).val();
    var _repwd = $(RESET_TXTREPWD).val();
        
    var _result = false;
    if (_pwd == NULLSTRNG) {
        $(RESET_ERR).show(100);
        $(RESET_ERRMSG).text(RES_ERR_BLANKNEWPASSWORD);
        return false;
    } else {
        $(RESET_ERR).hide();
        _result = true;
    }
    if (_pwd.length < 7) {
        $(RESET_ERR).show(100);
        $(RESET_ERRMSG).text(REG_ERR_SMALLPASSWORD);
        return false;
    }
    else {
        $(RESET_ERR).hide(100);
        _result = true;
    }


    if (_repwd == NULLSTRNG) {
        $(RESET_ERR).show(100);
        $(RESET_ERRMSG).text(RES_ERR_BLANKREPASSWORD);
        return false;
    }
    else {
        $(RESET_ERR).hide(100);
        _result = true;
    }

    if (_pwd != _repwd) {
        $(RESET_ERR).show(100);
        $(RESET_ERRMSG).text(REG_ERR_PASSWORDSNOMATCH);
        return false;
    }
    else {
        $(RESET_ERR).hide(100);
        _result = true;
    }
    return _result;
}
