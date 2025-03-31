
using Microsoft.AspNetCore.Identity;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductsStore.Domain.Entities
{
    public class User : IdentityUser
    {

        public List<Article> Articles { get; set; } = [];
        public List<Comment> Comments { get; set; } = [];
    }
}
