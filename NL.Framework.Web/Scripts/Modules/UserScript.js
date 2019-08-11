﻿layui.config({
    base: '../Scripts/layui/src/layuiadmin/' //静态资源所在路径
}).extend({
    index: 'lib/index' //主入口模块
}).use(['index', 'useradmin', 'NLFrameAjax','table'], function () {
    var $ = layui.$
        , nAjax = layui.NLFrameAjax
        , form = layui.form
        , table = layui.table;

    //监听搜索
    form.on('submit(LAY-user-front-search)', function (data) {
        var field = data.field;

        //执行重载
        table.reload('LAY-user-manage', {
            where: field
        });
    });

    //事件
    var active = {
        delete: function () {
            var checkStatus = table.checkStatus('LAY-user-manage')
                , checkData = checkStatus.data; //得到选中的数据

            if (checkData.length === 0) {
                return layer.msg('请选择用户');
            }
            console.log(checkData);
            //layer.prompt({
            //    formType: 1
            //    , title: '敏感操作，请验证口令'
            //}, function (value, index) {
            //    layer.close(index);
            //});
            layer.confirm('确定删除吗？', function (index) {
                //执行 Ajax 后重载
                nAjax.NLPost({
                    url: '/System/DeleteUser',
                    data: JSON.stringify(checkData),
                    listParam: true,
                    successfn: function (res) {
                        if (res.Code == 200) {
                            //obj.del();
                            //请求成功后，重载table
                            table.reload('LAY-user-manage'); //数据刷新
                        }
                        layer.msg(res.Message);
                    }
                });
                table.reload('LAY-user-manage');
                layer.msg('已删除');
            });
        }
        , add: function () {
            layer.open({
                type: 2
                , title: '添加用户'
                , content: '/System/UserAdd'
                , maxmin: true
                , area: ['800px', '700px']
                , btn: ['确定', '取消']
                , yes: function (index, layero) {
                    var iframeWindow = window['layui-layer-iframe' + index]
                        , submitID = 'LAY-user-add-submit'
                        , submit = layero.find('iframe').contents().find('#' + submitID);
                    //监听提交
                    iframeWindow.layui.form.on('submit(' + submitID + ')', function (data) {
                        var field = data.field; //获取提交的字段
                        if (field.RoleId == "") {
                            layer.open({
                                title: '绑定角色',
                                content: '请选择需要绑定的角色'
                            });
                            return;
                        }
                        //提交 Ajax 成功后，静态更新表格中的数据
                        //$.ajax({});
                        nAjax.NLPost({
                            url: '/System/AddUser',
                            data: field,
                            successfn: function (res) {
                                postResult = res;
                                if (res.Code == 200) {
                                    success = true;
                                    layer.close(index); //关闭弹层
                                    //请求成功后，重载table
                                    table.reload('LAY-user-manage'); //数据刷新
                                    layui.msg(res.Message);
                                } else {
                                    layer.open({
                                        title: res.Code,
                                        content: res.Message
                                    });
                                    return;
                                }
                            }
                        });
                    });
                    submit.trigger('click');
                }
            });
        }
    };

    $('.layui-btn.layuiadmin-btn-useradmin').on('click', function () {
        var type = $(this).data('type');
        active[type] ? active[type].call(this) : '';
    });
});