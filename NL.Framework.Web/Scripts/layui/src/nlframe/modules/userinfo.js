layui.define(['form', 'upload'], function (exports) {
    var $ = layui.$
        , layer = layui.layer
        , admin = layui.admin
        , setter = layui.setter
        , form = layui.form
        , upload = layui.upload;

    var $body = $('body');

    //自定义验证
    form.verify({
        nickname: function (value, item) { //value：表单的值、item：表单的DOM对象
            if (!new RegExp("^[a-zA-Z0-9_\u4e00-\u9fa5\\s·]+$").test(value)) {
                return '用户名不能有特殊字符';
            }
            if (/(^\_)|(\__)|(\_+$)/.test(value)) {
                return '用户名首尾不能出现下划线\'_\'';
            }
            if (/^\d+\d+\d$/.test(value)) {
                return '用户名不能全为数字';
            }
        }

        //我们既支持上述函数式的方式，也支持下述数组的形式
        //数组的两个值分别代表：[正则匹配、匹配不符时的提示文字]
        , pass: [
            /^[\S]{6,12}$/
            , '密码必须6到12位，且不能出现空格'
        ]

        //确认密码
        , repass: function (value) {
            if (value !== $('#LAY_password').val()) {
                return '两次密码输入不一致';
            }
        }
    });

    //上传头像
    var avatarSrc = $('#LAY_avatarSrc');
    upload.render({
        url: '/System/UploadImage'
        , elem: '#LAY_avatarUpload'
        , done: function (res) {
            if (res.ResultState == 200) {
                avatarSrc.val(res.ResultPath);
            } else {
                layer.msg(res.ResultMsg, { icon: 5 });
            }
        }
    });

    //查看头像
    admin.events.avartatPreview = function (othis) {
        var src = avatarSrc.val() || 'favicon.ico';
        var _path = admin.getConfig(setter.NLFRAME_CONFIG_USER_IMAGE);
        layer.photos({
            photos: {
                "title": "查看头像" //相册标题
                , "data": [{
                    "src": _path + src //原图地址
                }]
            }
            , shade: 0.01
            , closeBtn: 1
            , anim: 5
        });
    };
    //对外暴露的接口
    exports('userinfo', {});
});