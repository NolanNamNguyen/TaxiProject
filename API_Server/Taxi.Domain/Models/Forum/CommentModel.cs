using System;
using System.Collections.Generic;
using System.Text;

namespace Taxi.Domain.Models.Forum
{
    public class CommentModel
    {
        public int CommentId { get; set; }
        public int ReviewId { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Content { get; set; }
        public DateTime Created { get; set; }
    }
}
