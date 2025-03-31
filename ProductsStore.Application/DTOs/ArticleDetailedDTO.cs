using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductsStore.Application.DTOs
{
    public class ArticleDetailsDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime PublishDate { get; set; }
        public string AuthorId { get; set; }
        public UserDTO Author { get; set; }

        public List<CommentDTO> Comments { get; set; } = new();
    }
}
