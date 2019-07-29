﻿layui.config({
    base: '../Scripts/layui/src/layuiadmin/' //静态资源所在路径
}).extend({
    index: 'lib/index' //主入口模块
}).use(['index', 'useradmin', 'table'], function () {
    var $ = layui.$
        , form = layui.form
        , table = layui.table
        , admin = layui.admin;

    //搜索角色
    form.on('select(LAY-user-adminrole-type)', function (data) {
        //执行重载
        table.reload('LAY-user-back-role', {
            where: {
                role: data.value
            }
        });
    });

    //事件
    var active = {
        delete: function () {
            var checkStatus = table.checkStatus('LAY-user-back-role')
                , checkData = checkStatus.data; //得到选中的数据

            if (checkData.length === 0) {
                return layer.msg('请选择数据');
            }

            layer.confirm('确定删除吗？', function (index) {
                //执行 Ajax 后重载
                $.ajax({
                    url: '/System/DeleteRole',
                    type: 'POST',
                    dataType: 'JSON',
                    data: checkData[0],
                    success: function (res) {
                        if (res.code > 0) {
                            //请求成功后，写入 access_token
                            table.reload('LAY-user-back-role');
                        }
                        layer.msg(res.msg);
                    }
                });
            });
        },
        add: function () {
            layer.open({
                type: 2
                , title: '添加新角色'
                , content: 'roleform.html'
                , area: ['500px', '480px']
                , btn: ['确定', '取消']
                , yes: function (index, layero) {
                    var iframeWindow = window['layui-layer-iframe' + index]
                        , submit = layero.find('iframe').contents().find("#LAY-user-role-submit");

                    //监听提交
                    iframeWindow.layui.form.on('submit(LAY-user-role-submit)', function (data) {
                        var field = data.field; //获取提交的字段

                        //提交 Ajax 成功后，静态更新表格中的数据
                        //$.ajax({});
                        table.reload('LAY-user-back-role');
                        layer.close(index); //关闭弹层
                    });

                    submit.trigger('click');
                }
            });
        }
    }
    $('.layui-btn.layuiadmin-btn-role').on('click', function () {
        var type = $(this).data('type');
        active[type] ? active[type].call(this) : '';
    });
});