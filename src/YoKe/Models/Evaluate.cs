using System;
using System.Collections.Generic;

namespace YoKe.Models
{
    public partial class Evaluate
    {
        public int ObjId { get; set; }
        public int? TheProduct { get; set; }
        public string Content { get; set; }
        public int? Level { get; set; }

        public virtual Product TheProductNavigation { get; set; }
    }
}
