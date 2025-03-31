using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductsStore.Domain.Entities
{
    public class Article
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public DateTime PublishDate { get; set; } = DateTime.UtcNow;

        public string AuthorId{ get; set; }
        public User Author { get; set; }


        public List<Comment> Comments { get; set; } = [];


    }
}
