//***********************************************************
//    作者：Nicholas Leo
//    E-Mail:nicholasleo1030@163.com
//    GitHub:https://github.com/nicholasleo
//    时间：2019-07-25 18:51:05
//    说明：
//    版权所有：个人
//***********************************************************
using NL.Framework.Model.System;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NL.Framework.DAL
{
    public class NLFrameContext : DbContext,IDisposable//,IDbContext
    {
        public NLFrameContext() : base("name=DbConn")
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<NLFrameContext>());
        }

        public DbSet<FunctionModel> Functions { get; set; }
        public DbSet<MenuModel> Menus { get; set; }
        public DbSet<RoleModel> Roles { get; set; }
        public DbSet<UserModel> Users { get; set; }
        public DbSet<MenuFunctionModel> MenuFunctions { get; set; }
        public DbSet<RoleMenuFunctionModel> RoleMenuFunctions { get; set; }
        public DbSet<UserRoleModel> UserRoles { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            var typesToRegister = Assembly.GetExecutingAssembly().GetTypes()
                .Where(type => !string.IsNullOrEmpty(type.Namespace))
                .Where(type => type.BaseType != null && type.BaseType.IsGenericType && type.BaseType.GetGenericTypeDefinition() == typeof(EntityTypeConfiguration<>));

            foreach (var item in typesToRegister)
            {
                dynamic configurationInstance = Activator.CreateInstance(item);
                modelBuilder.Configurations.Add(configurationInstance);
            }
            base.OnModelCreating(modelBuilder);

        }
    }
}
