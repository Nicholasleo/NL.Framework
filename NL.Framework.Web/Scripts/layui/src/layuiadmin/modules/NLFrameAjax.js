layui.define('jquery', function (exports) {
    var $ = layui.$;

    //var NLFrameAjax

    var NLFrameAjax = {
        NLReq: function (url, type, dataType, data, successfn, errorfn) {
            $.ajax({
                url: url,
                type: type || 'GET',
                dataType: dataType,
                data: data,
                success: successfn,
                error: errorfn
            });
        },
        NLPost: function (option) {
            $.ajax({
                url: option.url,
                type: 'POST',
                dataType: option.dataType || 'JSON',
                contentType: option.contentType || 'application/x-www-form-urlencoded',
                async: option.async || true,
                cache: option.cache || false,
                data: option.data,
                success: option.successfn,
                error: option.errorfn
            });
        },
        NLGet: function (option) {
            $.ajax({
                url: option.url,
                type: 'GET',
                dataType: option.dataType || 'JSON',
                async: option.async || true,
                cache: option.cache || false,
                data: option.data,
                success: option.successfn,
                error: option.errorfn
            });
        }
    };
    exports('NLFrameAjax', NLFrameAjax);
});