layui.config({
    base: '../Scripts/layui/src/nlframe/' //静态资源所在路径
}).extend({
    index: 'lib/index' //主入口模块
}).use(['index', 'NLFrameAjax','table'], function () {
    var $ = layui.$
        , nAjax = layui.NLFrameAjax
        , form = layui.form
        , table = layui.table;
    //用户管理
    table.render({
        elem: '#LAY-user-manage'
        , url: '/System/GetUserList' //模拟接口
        , cellMinWidth: 150
        , cols: [[
            { type: 'checkbox', fixed: 'left' }
            , { field: 'Fid', width: 100, hide: true, title: 'ID', sort: true }
            , { field: 'UserCode', title: '登录名' }
            , { field: 'UserName', title: '用户名' }
            , { field: 'UserPwd', title: '密码' }
            , {
                field: 'Gender', width: 80, title: '性别', templet: '#GenderTpl'
            }
            , { field: 'UserAge', width: 80, title: '年龄' }
            , { field: 'IdCard', title: '身份证' }
            , { field: 'Email', title: '电子邮件' }
            , { field: 'WeChat', title: '微信' }
            , { field: 'QQ', title: 'QQ' }
            , { field: 'MobilePhone', title: '手机' }
            , { field: 'Address', title: '地址' }
            , { field: 'IsAdmin', title: '超级管理员', templet: '#IsAdminTpl' }
            , { field: 'FirstLoginTime', title: '第一次登录', width: 220, templet: '<div>{{ layui.laytpl.toDateString(d.FirstLoginTime) }}</div>' }
            , { field: 'LastLoginTime', title: '最近一次登录', width: 220, templet: '<div>{{ layui.laytpl.toDateString(d.LastLoginTime) }}</div>' }
            , { field: 'State', title: '状态', width: 80, templet: '#StateTpl' }
            , { field: 'Description', title: '描述' }
            , { field: 'CreateTime', title: '创建时间', width: 220, templet: '<div>{{ layui.laytpl.toDateString(d.CreateTime) }}</div>' }
            , { field: 'CreatePerson', title: '创建人' }
            , { field: 'ModifyTime', title: '修改时间', width: 220, templet: '<div>{{ layui.laytpl.toDateString(d.ModifyTime) }}</div>' }
            , { field: 'ModifyPerson', title: '修改人' }
            , { title: '操作', width: 240, align: 'center', fixed: 'right', toolbar: '#table-useradmin-webuser' }
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
    table.on('tool(LAY-user-manage)', function (obj) {
        var data = obj.data;
        if (obj.event === 'delete') {
            layer.confirm('真的删除行么', function (index) {
                var pData = [];
                pData.push(data);
                nAjax.NLPost({
                    url: '/System/DeleteUser',
                    data: JSON.stringify(pData),
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
                layer.close(index);
            });
        } else if (obj.event === 'edit') {
            var tr = $(obj.tr);
            var index = layer.open({
                type: 2
                , title: '编辑用户'
                , content: '/System/UserEdit?fid=' + data.Fid
                , maxmin: true
                , area: ['800px', '700px']
                , btn: ['确定', '取消']
                , yes: function (index, layero) {
                    var iframeWindow = window['layui-layer-iframe' + index]
                        , submitID = 'LAY-user-edit-submit'
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
                        nAjax.NLPost({
                            url: '/System/UpdateUser',
                            data: field,
                            successfn: function (res) {
                                if (res.Code == 200) {
                                    //请求成功后，重载table
                                    table.reload('LAY-user-manage'); //数据刷新
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
            });
        } else if (obj.event === 'bind') {
            var tr = $(obj.tr);
            var index = layer.open({
                type: 2
                , title: '绑定角色'
                , content: '/System/UserBind?fid=' + data.Fid
                , maxmin: false
                , area: ['600px', '500px']
                , btn: ['确定', '取消']
                , yes: function (index, layero) {
                    var iframeWindow = window['layui-layer-iframe' + index]
                        , submitID = 'NL-user-role-submit'
                        , submit = layero.find('iframe').contents().find('#' + submitID);

                    //监听提交
                    iframeWindow.layui.form.on('submit(' + submitID + ')', function (data) {
                        var field = data.field; //获取提交的字段
                        //提交 Ajax 成功后，静态更新表格中的数据
                        if (field.RoleId == "") {
                            layer.open({
                                title: '绑定角色',
                                content: '请选择需要绑定的角色'
                            });
                            return;
                        }
                        nAjax.NLPost({
                            url: '/System/UpdateUserRole',
                            data: field,
                            successfn: function (res) {
                                if (res.Code == 200) {
                                    table.reload('LAY-user-manage'); //数据刷新
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
            });
        }
    });

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

    $('.layui-btn.nlframe-btn-common').on('click', function () {
        var type = $(this).data('type');
        active[type] ? active[type].call(this) : '';
    });
});