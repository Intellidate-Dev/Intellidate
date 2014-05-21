(function ($) {
    $.postJSON = function (serviceurl, obj, callback) {
        $.ajax({
            url: serviceurl,
            type: 'POST',
            dataType: 'json',
            data: obj,
            success: callback,
            error: function (_err) {
                alert("There has been an error processing the request. Please reload the page.");
                //window.location.href="/Secured/Dashboard";
            }
        });
    };
}(jQuery));


(function ($) {
    $.postDATA = function (serviceurl, formData, callback) {
        $.ajax({
            url: serviceurl,
            type: 'POST',
            dataType: 'json',
            data: formData,
            success: callback,
            error: function (_err) {
                alert("There has been an error processing the request. Please reload the page.");
                //window.location.href = "/Secured/Dashboard";
            }
        });
    };
}(jQuery));


(function ($) {
    $.uploadFILE = function (serviceurl, formData, callback) {
        $.ajax({
            url: serviceurl,
            type: 'POST',
            paramName: 'files',
            data: formData,
            success: callback,
            processData: false,
            contentType: false,
            error: function (_err) {
                alert("There has been an error processing the request. Please reload the page.");
            }
        });
    };
}(jQuery));

