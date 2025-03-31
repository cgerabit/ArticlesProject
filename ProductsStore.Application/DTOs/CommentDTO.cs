using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductsStore.Application.DTOs
{
    public class CommentDTO
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Text { get; set; }
        public DateTime PublishedAt { get; set; }

        public UserDTO User { get; set; }
    }
}
