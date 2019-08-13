
 
layui.define(function(exports){
  var $ = layui.$
  ,layer = layui.layer
  ,laytpl = layui.laytpl
  ,setter = layui.setter
  ,view = layui.view
  ,admin = layui.admin
  
  //公共业务的逻辑处理可以写在此处，切换任何页面都会执行
  //……
    //时间戳的处理
    layui.laytpl.toDateString = function (d, format) {
        var date = new Date();
        if (d == null)
            return "";
        else
            date = new Date(d.replace("/Date(", "").replace(")/", "") || new Date())
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

  //退出
  admin.events.logout = function(){
    //执行退出接口
    admin.req({
      url: '/Login/LogOut'
      ,type: 'get'
      ,data: {}
      ,done: function(res){ //这里要说明一下：done 是只有 response 的 code 正常才会执行。而 succese 则是只要 http 为 200 就会执行
        
        //清空本地记录的 token，并跳转到登入页
        admin.exit(function(){
          location.href = '/Login/Index';
        });
      }
    });
  };

  
  //对外暴露的接口
  exports('common', {});
});