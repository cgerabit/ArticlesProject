using ProductsStore.Domain.Entities;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductsStore.Application.Contracts
{
    public interface ICommentRepository
    {
        Task AddAsync(Comment comment);
        Task SaveChangesAsync();
    }
}
