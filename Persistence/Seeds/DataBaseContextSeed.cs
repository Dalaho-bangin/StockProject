using Domain.Roles;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Seeds
{
    public class DataBaseContextSeed
    {
        public static void PermissionSeed(ModelBuilder modelBuilder)
        {
            foreach (var permission in GetPermissioms())
            {
                modelBuilder.Entity<Permission>().HasData(permission);
            }
        }

        private static IEnumerable<Permission> GetPermissioms()
        {
            return new List<Permission>()
            {
                new Permission(){PermissionId=1,PermissionTitle="مدیر سایت",ParentPermissionId=null},
                new Permission(){PermissionId=2,PermissionTitle="مدیریت کاربران",ParentPermissionId=1},
                new Permission(){PermissionId=3,PermissionTitle="افزودن کاربر",ParentPermissionId=2},
                new Permission(){PermissionId=4,PermissionTitle="ویرایش کاربر",ParentPermissionId=2},
                new Permission(){PermissionId=5,PermissionTitle="حذف کاربر",ParentPermissionId=2},
                new Permission(){PermissionId=6,PermissionTitle="افزودن نقش به کاربر",ParentPermissionId=2},
                new Permission(){PermissionId=7,PermissionTitle="مدیریت نقش ها",ParentPermissionId=1},
                new Permission(){PermissionId=8,PermissionTitle="افزودن نقش",ParentPermissionId=7},
                new Permission(){PermissionId=9,PermissionTitle="ویرایش نقش",ParentPermissionId=7},
                new Permission(){PermissionId=10,PermissionTitle="حذف نقش",ParentPermissionId=7},

            };
        }
    }
}
