
layui.define(['table', 'form'], function(exports){
  var $ = layui.$
  ,table = layui.table
  ,form = layui.form;

  //用户管理
  table.render({
    elem: '#LAY-user-manage'
      , url: '/System/GetUserList' //模拟接口
      , cellMinWidth:150
    ,cols: [[
      {type: 'checkbox', fixed: 'left'}
        , { field: 'Fid', width: 100, hide:true, title: 'ID', sort: true}
        , { field: 'UserCode', title: '登录名'}
        , { field: 'UserName', title: '用户名'}
        , { field: 'UserPwd', title: '密码'}
        , {
            field: 'Gender', width: 80, title: '性别', templet: '#GenderTpl'
        }
        , { field: 'UserAge', width: 80, title: '年龄' }
        , { field: 'IdCard',title: '身份证' }
        , { field: 'Email', title: '电子邮件'}
        , { field: 'WeChat', title: '微信' }
        , { field: 'QQ', title: 'QQ' }
        , { field: 'MobilePhone', title: '手机' }
        , { field: 'Address', title: '地址' }
        , { field: 'IsAdmin', title: '超级管理员', templet: '#IsAdminTpl'}
        , { field: 'FirstLoginTime', title: '第一次登录', templet: '<div>{{ layui.laytpl.toDateString(d.FirstLoginTime) }}</div>' }
        , { field: 'LastLoginTime', title: '最近一次登录', templet: '<div>{{ layui.laytpl.toDateString(d.LastLoginTime) }}</div>' }
        , { field: 'State', title: '状态', width: 80,  templet: '#StateTpl'}
        , { field: 'Description', title: '描述' }
        , { field: 'CreateTime', title: '创建时间', templet: '<div>{{ layui.laytpl.toDateString(d.CreateTime) }}</div>' }
        , { field: 'CreatePerson', title: '创建人' }
        , { field: 'ModifyTime', title: '修改时间', templet: '<div>{{ layui.laytpl.toDateString(d.ModifyTime) }}</div>' }
        , { field: 'ModifyPerson', title: '修改人' }
      ,{title: '操作', width: 150, align:'center', fixed: 'right', toolbar: '#table-useradmin-webuser'}
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
  table.on('tool(LAY-user-manage)', function(obj){
    var data = obj.data;
      if (obj.event === 'delete') {
          layer.confirm('真的删除行么', function (index) {
              $.ajax({
                  url: '/System/DeleteUser',
                  type: 'POST',
                  dataType: 'JSON',
                  data: data,
                  success: function (res) {
                      if (res.code > 0) {
                          obj.del();
                          //请求成功后，重载table
                          table.reload('LAY-user-manage'); //数据刷新
                      }
                      layer.msg(res.msg);
                  }
              });
              layer.close(index);
      });
    } else if(obj.event === 'edit'){
      var tr = $(obj.tr);
      var index = layer.open({
        type: 2
        ,title: '编辑用户'
        ,content: '/System/UserEdit?fid='+data.Fid
        ,maxmin: true
        ,area: ['900px', '800px']
        ,btn: ['确定', '取消']
        ,yes: function(index, layero){
          var iframeWindow = window['layui-layer-iframe'+ index]
          ,submitID = 'LAY-user-front-submit'
          ,submit = layero.find('iframe').contents().find('#'+ submitID);
          
          //监听提交
          iframeWindow.layui.form.on('submit('+ submitID +')', function(data){
            var field = data.field; //获取提交的字段
            
            //提交 Ajax 成功后，静态更新表格中的数据
            //$.ajax({});
              $.ajax({
                  url: '/System/UpdateUser',
                  type: 'POST',
                  dataType: 'JSON',
                  data: field,
                  success: function (res) {
                      if (res.code > 0) {
                          //请求成功后，重载table
                          table.reload('LAY-user-manage'); //数据刷新
                      }
                      layer.msg(res.msg);
                  }
              });
            layer.close(index); //关闭弹层
          });  
          submit.trigger('click');
        }
        ,success: function(layero, index){
          
        }
      });
          layer.full(index);
    }
  });

  //菜单管理
  table.render({
    elem: '#LAY-useradmin-menu'
      , url: '/System/GetMenuList'
    ,cols: [[
        { type: 'checkbox', fixed: 'left' }
        , { field: 'Fid', width: 80, title: 'ID', hide: true, sort: true }
        , { field: 'MenuName', title: '菜单名称',align:'center'}
        , { field: 'MenuUrl', title: '菜单地址', width: 240 }
        , { field: 'MenuIcon', title: '菜单图标', width: 100, templet: '<div><i class="layui-icon {{d.MenuIcon}}"></i></div>', unresize: true, align: 'center' }
        , { field: 'MenuIndex', title: '菜单顺序', sort: true, width: 100, unresize: true, align: 'center' }
        , { field: 'MenuIsShow', title: '菜单状态', templet: '#buttonTpl', width: 100, align: 'center', unresize: true, align: 'center' }
        , { field: 'CreatePerson', title: '创建人', align: 'center'}
        , { field: 'CreateTime', title: '创建时间', templet: '<div>{{ layui.laytpl.toDateString(d.CreateTime) }}</div>', align: 'center'}
        , { field: 'ModifyPerson', title: '修改人', align: 'center'}
        , { field: 'ModifyTime', title: '修改时间', templet: '<div>{{ layui.laytpl.toDateString(d.ModifyPerson) }}</div>', align: 'center'}
        , { title: '操作', width: 150, align: 'center', fixed: 'right', toolbar: '#table-useradmin-menu'}
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
    table.on('tool(LAY-useradmin-menu)', function(obj){
    var data = obj.data;
      if (obj.event === 'delete'){
          layer.confirm('确定删除此菜单？', function (index) {
              $.ajax({
                  url: '/System/DeleteMenu',
                  type: 'POST',
                  dataType: 'JSON',
                  data: data,
                  success: function (res) {
                      if (res.code > 0) {
                          //请求成功后，重载table
                          obj.del();
                          table.reload('LAY-useradmin-menu'); //数据刷新
                      }
                      layer.msg(res.msg);
                  }
              });
              layer.close(index);
          });
    }else if(obj.event === 'edit'){
      var tr = $(obj.tr);

      var index = layer.open({
        type: 2
          , title: '编辑菜单'
          , content: '/System/MenuEdit?fid=' + data.Fid
        ,area: ['550px', '450px']
        ,btn: ['确定', '取消']
        ,yes: function(index, layero){
          var iframeWindow = window['layui-layer-iframe'+ index]
              , submitID = 'LAY-user-menu-submit'
          ,submit = layero.find('iframe').contents().find('#'+ submitID);

          //监听提交
          iframeWindow.layui.form.on('submit('+ submitID +')', function(data){
            var field = data.field; //获取提交的字段
            
            //提交 Ajax 成功后，静态更新表格中的数据
            //$.ajax({});
              $.ajax({
                  url: '/System/UpdateMenu',
                  type: 'POST',
                  dataType: 'JSON',
                  data: field,
                  success: function (res) {
                      if (res.code > 0) {
                          //请求成功后，重载table
                          table.reload('LAY-useradmin-menu'); //数据刷新
                      }
                      layer.msg(res.msg);
                  }
              });
            layer.close(index); //关闭弹层
          });  
          
          submit.trigger('click');
        }
        ,success: function(layero, index){           
          
        }
      })
          //layer.full(index);
    }
  });

  //角色管理
  table.render({
    elem: '#LAY-user-back-role'
      , url: '/System/GetRoleList' //模拟接口
    ,cols: [[
      {type: 'checkbox', fixed: 'left'}
        , { field: 'FID', width: 10, title: 'ID', hide:true, sort: true}
        , { field: 'RoleName', title: '角色名' }
        , { field: 'Description', title: '具体描述' ,width:400 }
        , { field: 'CreatePerson', title: '创建人' ,width:150 }
        , { field: 'CreateTime', title: '创建时间', templet: '<div>{{ layui.laytpl.toDateString(d.CreateTime) }}</div>'}
        , { field: 'ModifyPerson', title: '修改人' ,width:150}
        , { field: 'ModifyTime', title: '修改时间', templet: '<div>{{ layui.laytpl.toDateString(d.ModifyPerson) }}</div>'}
      ,{title: '操作', width: 150, align: 'center', fixed: 'right', toolbar: '#table-useradmin-admin'}
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
  table.on('tool(LAY-user-back-role)', function(obj){
      var data = obj.data;
    if(obj.event === 'delete'){
        layer.confirm('确定删除此角色？', function (index) {
            $.ajax({
                url: '/System/DeleteRole',
                type: 'POST',
                dataType: 'JSON',
                data: data,
                success: function (res) {
                    if (res.code > 0) {
                        //请求成功后，重载table
                        obj.del();
                        table.reload('LAY-user-back-role');
                    }
                    layer.msg(res.msg);
                }
            });
        layer.close(index);
      });
    }else if(obj.event === 'edit'){
        var tr = $(obj.tr);
      layer.open({
        type: 2
        ,title: '编辑角色'
        ,content: '/System/RoleEdit?fid=' + data.Fid
        ,area: ['500px', '480px']
        ,btn: ['确定', '取消']
        ,yes: function(index, layero){
          var iframeWindow = window['layui-layer-iframe'+ index]
          ,submit = layero.find('iframe').contents().find("#LAY-user-role-submit");

          //监听提交
          iframeWindow.layui.form.on('submit(LAY-user-role-submit)', function(data){
            var field = data.field; //获取提交的字段
            
            //提交 Ajax 成功后，静态更新表格中的数据
            //$.ajax({});
              $.ajax({
                  url: '/System/UpdateRole',
                  type: 'POST',
                  dataType: 'JSON',
                  data: field,
                  success: function (res) {
                      if (res.code > 0) {
                          //请求成功后，重载table
                          table.reload('LAY-user-back-role'); //数据刷新
                      }
                      layer.msg(res.msg);
                  }
              });
            layer.close(index); //关闭弹层
          });  
          
          submit.trigger('click');
        }
        ,success: function(layero, index){
        
        }
      })
    }
  });

    //时间戳的处理
    layui.laytpl.toDateString = function (d, format) {
        var date = new Date();
        if (d == null)
            return "";
        else
            date = new Date(parseInt(d.replace("/Date(", "").replace(")/", ""), 10) || new Date())
            , ymd = [
                this.digit(date.getFullYear(), 4)
                , this.digit(date.getMonth() + 1)
                , this.digit(date.getDate())
            ]
            , hms = [
                this.digit(date.getHours())
                , this.digit(date.getMinutes())
                , this.digit(date.getSeconds())
            ];

        format = format || 'yyyy-MM-dd HH:mm:ss';

        return format.replace(/yyyy/g, ymd[0])
            .replace(/MM/g, ymd[1])
            .replace(/dd/g, ymd[2])
            .replace(/HH/g, hms[0])
            .replace(/mm/g, hms[1])
            .replace(/ss/g, hms[2]);
    };

    //数字前置补零
    layui.laytpl.digit = function (num, length, end) {
        var str = '';
        num = String(num);
        length = length || 2;
        for (var i = num.length; i < length; i++) {
            str += '0';
        }
        return num < Math.pow(10, length) ? str + (num | 0) : num;
    };
  exports('useradmin', {})
});