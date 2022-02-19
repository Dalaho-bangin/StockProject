using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Utils
{
    public static class PathExtension
    {

        #region Defalt Avatar


        public static string DefaultAvatar = "/Admin/assets/img/90x90.jpg";

        #endregion

        #region user avatar

        public static string UserAvatarOrigin = "/Content/Images/UserAvatar/origin/";

        public static string UserAvatarOriginServer =
            Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Content/Images/UserAvatar/origin/");

        public static string UserAvatarThumb = "/Content/Images/UserAvatar/Thumb/";

        public static string UserAvatarThumbServer =
            Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Content/Images/UserAvatar/Thumb/");

        #endregion

    }
}
