layui.config({
    base: '../Scripts/layui/src/nlframe/' //静态资源所在路径
}).extend({
    index: 'lib/index' //主入口模块
}).use(['index', 'NLFrameAjax', 'table'], function () {
    var $ = layui.$
        , nAjax = layui.NLFrameAjax
        , form = layui.form
        , table = layui.table;

    //角色管理
    table.render({
        elem: '#LAY-user-back-role'
        , url: '/System/GetRoleList' //模拟接口
        , cols: [[
            { type: 'checkbox', fixed: 'left' }
            , { field: 'FID', width: 10, title: 'ID', hide: true, sort: true }
            , { field: 'RoleName', title: '角色名' }
            , { field: 'Description', title: '具体描述', width: 400 }
            , { field: 'CreatePerson', title: '创建人', width: 150 }
            , { field: 'CreateTime', title: '创建时间', templet: '<div>{{ layui.laytpl.toDateString(d.CreateTime) }}</div>' }
            , { field: 'ModifyPerson', title: '修改人', width: 150 }
            , { field: 'ModifyTime', title: '修改时间', templet: '<div>{{ layui.laytpl.toDateString(d.ModifyTime) }}</div>' }
            , { title: '操作', width: 150, align: 'center', fixed: 'right', toolbar: '#table-useradmin-admin' }
        ]]
        , parseData: function (res) { //res 即为原始返回的数据
            return {
                "code": res.code, //解析接口状态
                "msg": res.msg, //解析提示文本
                "count": res.count, //解析数据长度
                "data": JSON.parse(res.data) //解析数据列表
            };
        }
        , page: true
        , limit: 30
        , height: 'full-220'
        , text: '对不起，加载出现异常！'
    });

    //监听工具条
    table.on('tool(LAY-user-back-role)', function (obj) {
        var data = obj.data;
        if (obj.event === 'delete') {
            layer.confirm('确定删除此角色？', function (index) {
                var pData = [];
                pData.push(data);
                nAjax.NLPost({
                    url: '/System/DeleteRole',
                    data: JSON.stringify(pData),
                    listParam: true,
                    successfn: function (res) {
                        if (res.Code == 200) {
                            //请求成功后，重载table
                            table.reload('LAY-user-back-role');
                        }
                        layer.msg(res.Message);
                    }
                });
                layer.close(index);
            });
        } else if (obj.event === 'edit') {
            var tr = $(obj.tr);
            layer.open({
                type: 2
                , title: '编辑角色'
                , content: '/System/RoleEdit?fid=' + data.Fid
                , area: ['500px', '480px']
                , btn: ['确定', '取消']
                , yes: function (index, layero) {
                    var iframeWindow = window['layui-layer-iframe' + index]
                        , submit = layero.find('iframe').contents().find("#LAY-user-role-submit");
                    //监听提交
                    iframeWindow.layui.form.on('submit(LAY-user-role-submit)', function (data) {
                        var field = data.field; //获取提交的字段
                        //提交 Ajax 成功后，静态更新表格中的数据
                        nAjax.NLPost({
                            url: '/System/UpdateRole',
                            data: field,
                            successfn: function (res) {
                                if (res.Code == 200) {
                                    //请求成功后，重载table
                                    table.reload('LAY-user-back-role'); //数据刷新
                                }
                                layer.msg(res.Message);
                            }
                        });
                        layer.close(index); //关闭弹层
                    });

                    submit.trigger('click');
                }
                , success: function (layero, index) {

                }
            })
        }
    });

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
                nAjax.NLPost({
                    url: '/System/DeleteRole',
                    data: JSON.stringify(checkData),
                    listParam: true,
                    successfn: function (res) {
                        if (res.Code == 200) {
                            //执行 Ajax 后重载
                            table.reload('LAY-user-back-role');
                        }
                        layer.msg(res.Message);
                    }
                });
            });
        },
        add: function () {
            layer.open({
                type: 2
                , title: '添加角色'
                , content: '/System/RoleAdd'
                , area: ['500px', '480px']
                , btn: ['确定', '取消']
                , yes: function (index, layero) {
                    var iframeWindow = window['layui-layer-iframe' + index]
                        , submit = layero.find('iframe').contents().find("#LAY-user-role-submit");

                    //监听提交
                    iframeWindow.layui.form.on('submit(LAY-user-role-submit)', function (data) {
                        var field = data.field; //获取提交的字段
                        //提交 Ajax 成功后，静态更新表格中的数据
                        nAjax.NLPost({
                            url: '/System/AddRole',
                            data: field,
                            successfn: function (res) {
                                if (res.Code == 200) {
                                    //请求成功后，重载table
                                    table.reload('LAY-user-back-role'); //数据刷新
                                }
                                layer.msg(res.Message);
                            }
                        });
                        layer.close(index); //关闭弹层
                    });

                    submit.trigger('click');
                }
            });
        }
    }
    $('.layui-btn.nlframe-btn-common').on('click', function () {
        var type = $(this).data('type');
        active[type] ? active[type].call(this) : '';
    });
});