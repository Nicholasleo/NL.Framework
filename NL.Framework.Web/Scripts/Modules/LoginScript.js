layui.config({
    base: '../Scripts/layui/src/layuiadmin/' //静态资源所在路径
}).extend({
    index: 'lib/index' //主入口模块
}).use(['index', 'NLFrameAjax', 'user','form'], function () {
    var $ = layui.$
        , nAjax = layui.NLFrameAjax
        , setter = layui.setter
        , form = layui.form;

    $(document).keydown(function (e) {
        if (e.keyCode === 13) {
            e.preventDefault();
            $("[lay-filter='LAY-user-login-submit']").trigger("click");
        }
    });
    form.render();

    form.on('checkbox(remember)', function (data) {
        layui.data('loginInfo', {
            key: 'LoginRemember',
            value: data.elem.checked
        });
        remember = data.elem.checked;
    });

    //提交
    form.on('submit(LAY-user-login-submit)', function (obj) {
        var index = layer.load(1);
        var data = obj.field;
        nAjax.NLPost({
            url: '/Login/CheckLogin',
            data: data,
            successfn: function (res) {
                layer.close(index);
                if (res.Code == 200) {
                    setter.LOGIN_KEY = res.LoginUserEnt.UserId;
                    layui.data(setter.tableName, {
                        key: res.LoginUserEnt.UserId
                        , value: JSON.stringify(res.LoginUserEnt)
                    });
                    if (remember) {
                        //该处可改用加密的方式进行存储
                        layui.data('loginInfo', {
                            key: 'UserInfo',
                            value: {
                                UserCode: data.UserCode,
                                Password: data.Password
                            }
                        });
                    } else {
                        layui.data('loginInfo', null);
                    }
                    ////登入成功的提示与跳转
                    layer.msg('登入成功', {
                        offset: '15px'
                        , icon: 1
                        , time: 1000
                    }, function () {
                        location.href = '/Home/Index'; //后台主页
                    });
                } else {
                    layer.msg(res.Code + "  提示：" + res.Message);
                }
            }
        });
    });


    var remember = layui.data('loginInfo').LoginRemember || false;

    if (remember && layui.data('loginInfo').UserInfo != undefined) {
        form.val('layadmin-user-login', {
            'UserCode': layui.data('loginInfo').UserInfo.UserCode || '',
            'Password': layui.data('loginInfo').UserInfo.Password || '',
            'remember': remember
        });
    }
});