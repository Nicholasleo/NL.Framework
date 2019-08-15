//***********************************************************
//    作者：Nicholas Leo
//    E-Mail:nicholasleo1030@163.com
//    GitHub:https://github.com/nicholasleo
//    时间：2019-08-15 12:28:30
//    说明：
//    版权所有：个人
//***********************************************************
using NL.Framework.Model.System;

namespace NL.Framework.DAL.Map.System
{
    public class UserImageModelMap:BaseModelMap<UserImageModel>
    {
        public UserImageModelMap() : base()
        {
            ToTable(TableName._USERIMAGE);

            Property(t => t.Fid).HasColumnName("UserId");

            Property(t => t.UserIcon).HasColumnType("IMAGE");

            //HasOptional(t => t.UserModel).WithRequired(t => t.UserImage);
        }
    }
}
