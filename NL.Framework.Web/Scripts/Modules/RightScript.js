layui.config({
    base: '../Scripts/layui/src/layuiadmin/' //静态资源所在路径
}).extend({
    index: 'lib/index' //主入口模块
}).use(['index', 'useradmin', 'table', 'eleTree'], function () {
    var $ = layui.$
        , form = layui.form
        , layer = layui.layer
        , table = layui.table
        , eleTree = layui.eleTree
        , admin = layui.admin;

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

    var flag = true;
    //监听行单击事件（单击事件为：rowDouble）
    table.on('row(LAY-useradmin-right)', function (obj) {
        var data = obj.data;
        //layer.alert(JSON.stringify(data), {
        //    title: '当前行数据：'
        //});
        //标注选中样式
        var treeObj = {
            elem: '#rightTree',
            url: "/System/GetMenuFuncTree?fid=" + data.Fid,
            renderAfterExpand: true,
            showCheckbox: true,
            defaultExpandAll: true,
            autoExpandParent: true,
            emptText: "暂无数据", // 内容为空的时候展示的文本
            expandOnClickNode: true,
            defaultCheckedKeys: [23]
        }
        var el1 = eleTree.render(treeObj);

        obj.tr.addClass('layui-table-click').siblings().removeClass('layui-table-click');
        obj.tr.find('input[lay-type="layTableRadio"]').prop("checked", true);
        form.render('radio');
    });

    $('.layui-btn.layuiadmin-btn-right').on('click', function () {
        var type = $(this).data('type');
        active[type] ? active[type].call(this) : '';
    });
});