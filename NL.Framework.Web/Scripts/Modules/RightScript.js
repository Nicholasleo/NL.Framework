layui.config({
    base: '../Scripts/layui/src/layuiadmin/' //静态资源所在路径
}).extend({
    index: 'lib/index' //主入口模块
}).use(['index','table', 'dtree','NLFrameAjax'], function () {
    var $ = layui.$
        , nAjax = layui.NLFrameAjax
        , form = layui.form
        , layer = layui.layer
        , table = layui.table
        , dtree = layui.dtree;

    table.render({
        elem: '#LAY-useradmin-right'
        , url: '/System/GetRoleList'
        , cols: [[
            { type: 'radio', fixed: 'left' }
            , { field: 'FID', width: 10, title: 'ID', hide: true, sort: true }
            , { field: 'RoleName', title: '角色名' }
            , { field: 'Description', title: '具体描述', width: 400 }
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

    var roleid;

    //监听行单击事件（单击事件为：rowDouble）
    table.on('row(LAY-useradmin-right)', function (obj) {
        var data = obj.data;
        // 生成授权树
        roleid = data.Fid;
        var Dtree = dtree.render({
            elem: '#rightTree',
            method: 'GET',
            initLevel: "1",
            checkbarData:'halfChoose',
            checkbarType:'no-all',
            url: "/System/GetMenuFuncTree?fid=" + roleid,
            checkbar: true
        });

        //标注选中样式
        obj.tr.addClass('layui-table-click').siblings().removeClass('layui-table-click');
        obj.tr.find('input[lay-type="layTableRadio"]').prop("checked", true);
        form.render('radio');
    });

    var active = {
        save: function () {
            var params = dtree.getCheckbarNodesParam('rightTree');
            if (params == null || params == {} || roleid == null || roleid == undefined || roleid == "") {
                layer.open({
                    title: '错误'
                    , content: '请选择对应的角色进行授权！'
                });   
                return;
            }
            var roleMenu = [];
            var roleMenuFunction = [];
            params.forEach(function (item, index) {
                if (parseInt(item.level) <= 2) {
                    var obj = {
                        MenuId: item.nodeId
                    }
                    roleMenu.push(obj);
                } else {
                    var obj = {
                        MenuId: item.parentId,
                        FunctionId: item.nodeId
                    }
                    roleMenuFunction.push(obj);
                }
            });
            var postData = {
                RoleId: roleid,
                RoleMenuEnts: roleMenu,
                RoleMenuFunctionEnts: roleMenuFunction
            };
            nAjax.NLPost({
                url: '/System/SaveRightInfo',
                data: postData,
                successfn: function (res) {
                    layer.msg(res.msg);
                }
            });
        }
    }

    $('.layui-btn.layui-nicholas-leo').on('click', function () {
        var type = $(this).data('type');
        active[type] && active[type].call(this);
    });

});