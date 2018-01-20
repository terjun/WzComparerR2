using System;
using System.Collections.Generic;
using System.Text;

namespace WzComparerR2.Avatar
{
    class ExportInfo
    {
        public ExportInfo(AvatarCanvas avatar)
        {
            this.Avatar = avatar;
        }

        public AvatarCanvas Avatar { get; set; }
        public bool IsAnimated { get; set; }
        public int BodyFrame { get; set; }
        public int EmotionFrame { get; set; }
        public string Path { get; set; }
    }
}
