using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Service.Integration.Process.Domain.Entities;
using Service.Integration.Process.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Integration.Process.Service
{
    public class PointWorkService : IPointWorkService
    {
        private readonly IPontWorkRepository _pointWorkRepository;
        private readonly IConfiguration _configuration;
        public PointWorkService(IPontWorkRepository pontWorkRepository,
            IConfiguration configuration)
        {
            _pointWorkRepository = pontWorkRepository;
            _configuration = configuration;
        }

        public async Task IntegrationPointWork()
        {
            try
            {
                var pointWork = await _pointWorkRepository.GetPointWorkAsync();

                if (pointWork != null)
                {
                    foreach (var item in pointWork)
                    {
                        var verificPoint = await _pointWorkRepository.CheckPointWorksasync(item);
                        
                        if (verificPoint.Count == 0)
                        {
                            await _pointWorkRepository.AddPointWorksAsync(item);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
