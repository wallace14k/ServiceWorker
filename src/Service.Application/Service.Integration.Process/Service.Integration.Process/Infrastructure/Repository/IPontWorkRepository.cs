using Dapper;
using Service.Integration.Process.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service.Integration.Process.Infrastructure.Repository
{
    public interface IPontWorkRepository
    {
        Task<IEnumerable<PointWork>> GetPointWorkAsync();
        Task AddPointWorksAsync(PointWork entity);
        Task<IList<PointWork>> CheckPointWorksasync(PointWork entity);
    }
}
