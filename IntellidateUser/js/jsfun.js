// Method to post data
(function ($) {
    $.postDATA = function (serviceurl, formData, successcallback, errorcallback) {
        $.ajax({
            url: serviceurl,
            contentType: 'application/x-www-form-urlencoded; charset=utf-8',
            type: TYPEPOST,
            dataType: TYPEJSON,
            data: formData,
            success: successcallback,
            error: errorcallback
        });
    };
}(jQuery));

(function ($) {
    $.getDATA = function (serviceurl, successcallback, errorcallback) {
        $.ajax({
            url: serviceurl,
            type: TYPEGET,
            dataType: TYPEJSON,
            success: successcallback,
            error: errorcallback
        });
    };
}(jQuery));

//