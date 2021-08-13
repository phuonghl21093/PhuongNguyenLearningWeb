using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Web365Domain
{
    public class CommentItem : BaseModel
    {
        public int CustomerId { get; set; }
        public int Parent { get; set; }
        public string Url { get; set; }
        public string Detail { get; set; }
        public DateTime DateCreated { get; set; }
        public bool IsApprove { get; set; }
        public bool IsShow { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsReslove { get; set; }
        public List<CommentItem> Comment { get; set; }
        public CustomerItem Customer { get; set; }
    }
}
