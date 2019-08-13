﻿layui.config({
    base: '../Scripts/layui/src/layuiadmin/' //静态资源所在路径
}).extend({
    index: 'lib/index' //主入口模块
}).use(['index', 'table', 'dtree', 'NLFrameAjax'], function () {
    var $ = layui.$
        , nAjax = layui.NLFrameAjax
        , form = layui.form
        , layer = layui.layer
        , table = layui.table
        , dtree = layui.dtree;

    table.render({
        elem: '#LAY-dropdown-manage'
        , url: '/System/GetDropDownList'
        , cols: [[
            { type: 'radio', fixed: 'left' }
            , { field: 'FID', width: 10, title: 'ID', hide: true, sort: true }
            , { field: 'OptionsCode', title: '代码' }
            , { field: 'MyName', title: '显示名称' }
            , { field: 'MyValue', hide: true,title: '属性值' }
            , { field: 'Level', title: '级别' }
            , { field: 'CreatePerson', title: '创建人', align: 'center' }
            , { field: 'CreateTime', title: '创建时间', templet: '<div>{{ layui.laytpl.toDateString(d.CreateTime) }}</div>', align: 'center' }
            , { field: 'ModifyPerson', title: '修改人', align: 'center' }
            , { field: 'ModifyTime', title: '修改时间', templet: '<div>{{ layui.laytpl.toDateString(d.ModifyTime) }}</div>', align: 'center' }
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

    var fid;

    //监听行单击事件（单击事件为：rowDouble）
    table.on('row(LAY-dropdown-manage)', function (obj) {
        var data = obj.data;
        // 生成授权树
        fid = data.Fid;
        var Dtree = dtree.render({
            elem: '#rightTree',
            method: 'GET',
            initLevel: "1",
            line: true,
            iconfont: ["dtreefont", "layui-icon"] ,
            record: true,
            toolbar: true,
            scroll:'#toolBarDiv',
            url: "/System/GetDropDownTree?fid=" + fid,
            toolbarFun: toolBarFun
        });

        //标注选中样式
        obj.tr.addClass('layui-table-click').siblings().removeClass('layui-table-click');
        obj.tr.find('input[lay-type="layTableRadio"]').prop("checked", true);
        form.render('radio');
    });

    var toolBarFun = {
        addTreeNode: function (treeNode, $div) {
            //nAjax.NLPost({
            //    url: '/System/DeleteMenu',
            //    data: JSON.stringify(pData),
            //    listParam: true,
            //    successfn: function (res) {
            //        if (res.Code == 200) {
            //            //请求成功后，重载table
            //            table.reload('LAY-useradmin-menu'); //数据刷新
            //        }
            //        layer.msg(res.Message);
            //    }
            //});
        },
        editTreeNode: function (treeNode, $div) {
            //nAjax.NLPost({
            //    url: '/System/DeleteMenu',
            //    data: JSON.stringify(pData),
            //    listParam: true,
            //    successfn: function (res) {
            //        if (res.Code == 200) {
            //            //请求成功后，重载table
            //            table.reload('LAY-useradmin-menu'); //数据刷新
            //        }
            //        layer.msg(res.Message);
            //    }
            //});
        },
        delTreeNode: function (treeNode, $div) {
            //nAjax.NLPost({
            //    url: '/System/DeleteMenu',
            //    data: JSON.stringify(pData),
            //    listParam: true,
            //    successfn: function (res) {
            //        if (res.Code == 200) {
            //            //请求成功后，重载table
            //            table.reload('LAY-useradmin-menu'); //数据刷新
            //        }
            //        layer.msg(res.Message);
            //    }
            //});
        }
    };

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
                            table.reload('LAY-dropdown-manage');
                        }
                        layer.msg(res.Message);
                    }
                });
            });
        },
        add: function () {
            layer.open({
                type: 2
                , title: '添加下拉项'
                , content: '/System/DropDownAdd'
                , area: ['500px', '480px']
                , btn: ['确定', '取消']
                , yes: function (index, layero) {
                    var iframeWindow = window['layui-layer-iframe' + index]
                        , submit = layero.find('iframe').contents().find("#LAY-drop-down-submit");

                    //监听提交
                    iframeWindow.layui.form.on('submit(LAY-drop-down-submit)', function (data) {
                        var field = data.field; //获取提交的字段
                        //提交 Ajax 成功后，静态更新表格中的数据
                        nAjax.NLPost({
                            url: '/System/AddDropDown',
                            data: field,
                            successfn: function (res) {
                                if (res.Code == 200) {
                                    //请求成功后，重载table
                                    table.reload('LAY-dropdown-manage');
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
    };

    $('.layui-btn.layuiadmin-btn-menu').on('click', function () {
        var type = $(this).data('type');
        active[type] ? active[type].call(this) : '';
    });
});