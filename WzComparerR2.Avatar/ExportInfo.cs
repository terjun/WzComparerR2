using System;
using System.Collections.Generic;
using System.Text;

namespace WzComparerR2.Avatar
{
    class ExportInfo
    {
        public ExportInfo(AvatarCanvas _avatar)
        {
            this.avatar = _avatar;
        }

        public AvatarCanvas avatar { get; set; }
        public bool animated { get; set; }
        public int bodyFrame { get; set; }
        public int emoFrame { get; set; }
        public string path { get; set; }
    }
}
