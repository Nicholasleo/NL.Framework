layui.config({
    base: '../Scripts/layui/src/nlframe/' //静态资源所在路径
}).extend({
    index: 'lib/index' //主入口模块
}).use(['index', 'form', 'NLFrameAjax','table'], function () {
    var $ = layui.$
        , nAjax = layui.NLFrameAjax
        , form = layui.form
        , table = layui.table;

    //菜单管理
    table.render({
        elem: '#LAY-useradmin-menu'
        , url: '/System/GetMenuList'
        , cols: [[
            { type: 'checkbox', fixed: 'left' }
            , { field: 'Fid', width: 80, title: 'ID', hide: true, sort: true }
            , { field: 'MenuName', title: '菜单名称', align: 'center' }
            , { field: 'MenuUrl', title: '菜单地址', width: 240 }
            , { field: 'MenuIcon', title: '菜单图标', width: 100, templet: '<div><i class="layui-icon {{d.MenuIcon}}"></i></div>', unresize: true, align: 'center' }
            , { field: 'MenuIndex', title: '菜单顺序', sort: true, width: 100, unresize: true, align: 'center' }
            , { field: 'MenuIsShow', title: '菜单状态', templet: '#buttonTpl', width: 100, align: 'center', unresize: true, align: 'center' }
            , { field: 'CreatePerson', title: '创建人', align: 'center' }
            , { field: 'CreateTime', title: '创建时间', templet: '<div>{{ layui.laytpl.toDateString(d.CreateTime) }}</div>', align: 'center' }
            , { field: 'ModifyPerson', title: '修改人', align: 'center' }
            , { field: 'ModifyTime', title: '修改时间', templet: '<div>{{ layui.laytpl.toDateString(d.ModifyTime) }}</div>', align: 'center' }
            , { title: '操作', width: 150, align: 'center', fixed: 'right', toolbar: '#table-useradmin-menu' }
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
    table.on('tool(LAY-useradmin-menu)', function (obj) {
        var data = obj.data;
        if (obj.event === 'delete') {
            layer.confirm('确定删除此菜单？', function (index) {
                var pData = [];
                pData.push(data);
                nAjax.NLPost({
                    url: '/System/DeleteMenu',
                    data: JSON.stringify(pData),
                    listParam: true,
                    successfn: function (res) {
                        if (res.Code == 200) {
                            //请求成功后，重载table
                            table.reload('LAY-useradmin-menu'); //数据刷新
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
                , title: '编辑菜单'
                , content: '/System/MenuEdit?fid=' + data.Fid
                , area: ['550px', '450px']
                , btn: ['确定', '取消']
                , yes: function (index, layero) {
                    var iframeWindow = window['layui-layer-iframe' + index]
                        , submitID = 'LAY-user-menu-submit'
                        , submit = layero.find('iframe').contents().find('#' + submitID);

                    //监听提交
                    iframeWindow.layui.form.on('submit(' + submitID + ')', function (data) {
                        var field = data.field; //获取提交的字段
                        //提交 Ajax 成功后，静态更新表格中的数据
                        nAjax.NLPost({
                            url: '/System/UpdateMenu',
                            data: field,
                            successfn: function (res) {
                                if (res.code == 0) {
                                    //请求成功后，重载table
                                    table.reload('LAY-useradmin-menu'); //数据刷新
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
            //layer.full(index);
        }
    });

    //搜索菜单
    form.on('select(LAY-user-menu-type)', function (data) {
        console.log(data);
        //执行重载
        table.reload('LAY-useradmin-menu', {
            where: {
                filtter: data.value
            }
        });
    });

    //事件
    var active = {
        delete: function () {
            var checkStatus = table.checkStatus('LAY-useradmin-menu')
                , checkData = checkStatus.data; //得到选中的数据

            if (checkData.length === 0) {
                return layer.msg('请选择数据');
            }

            layer.confirm('确定删除吗？', function (index) {
                //执行 Ajax 后重载
                nAjax.NLPost({
                    url: '/System/DeleteMenu',
                    data: JSON.stringify(checkData),
                    listParam: true,
                    successfn: function (res) {
                        if (res.Code == 200) {
                            //请求成功后，写入 access_token
                            table.reload('LAY-useradmin-menu');
                        }
                        layer.msg(res.Message);
                    }
                });
            });
        },
        add: function () {
            layer.open({
                type: 2
                , title: '添加菜单'
                , content: '/System/MenuAdd'
                , area: ['500px', '480px']
                , btn: ['确定', '取消']
                , yes: function (index, layero) {
                    var iframeWindow = window['layui-layer-iframe' + index]
                        , submit = layero.find('iframe').contents().find("#LAY-user-menu-submit");

                    //监听提交
                    iframeWindow.layui.form.on('submit(LAY-user-menu-submit)', function (data) {
                        var field = data.field; //获取提交的字段
                        //提交 Ajax 成功后，静态更新表格中的数据
                        nAjax.NLPost({
                            url: '/System/AddMenu',
                            data: field,
                            successfn: function (res) {
                                if (res.Code == 200) {
                                    //请求成功后，重载table
                                    table.reload('LAY-useradmin-menu'); //数据刷新
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