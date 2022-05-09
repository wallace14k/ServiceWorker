using Dapper;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using Service.Integration.Process.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using System.Transactions;

namespace Service.Integration.Process.Infrastructure.Repository
{
    public class PontWorkRepository : IPontWorkRepository
    {
        private readonly IConfiguration _configuration;
        private string conn;
        public PontWorkRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            conn = _configuration.GetConnectionString("Default");
        }

        public async Task<IEnumerable<PointWork>> GetPointWorkAsync()
        {
            try
            {
                var connection = new MySqlConnection(conn);
                connection.Open();


                var result = await connection.QueryAsync<PointWork>($@"SELECT 
                                                                        p.id AS Id,
                                                                        p.inserted AS CreateAt,
                                                                        fun.nome As Name,
                                                                        fun.matricula AS Registration,
                                                                        car.id AS ChargeId,
                                                                        fun.id AS EmployeerId,
                                                                        p.totaltrabalhado AS TotalHours,
                                                                        car.descricao AS Charge
                                                                        FROM ponto P
                                                                        JOIN funcionario fun ON p.funcionarioid = fun.id
                                                                        JOIN funcao car ON car.id = fun.funcaoid 
                                                                        WHERE p.inserted = date(now() - interval 95 day)");
                connection.Close();
                return result;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task AddPointWorksAsync(PointWork entity)
        {
            try
            {
                var connection = new MySqlConnection(conn);

                connection.Open();
                using var scope = new TransactionScope();
                var parameters = new DynamicParameters();

                if (connection.State == ConnectionState.Open)
                {
                    await connection.ExecuteAsync(@$"INSERT INTO 
                                                    pointwork
                                                    (Id,
                                                    CreateAt,
                                                    Name,
                                                    Registration,
                                                    ChargeId,
                                                    EmployeerId,
                                                    ToTalHours,
                                                    Charge) 
                                                    VALUES(
                                                    {entity.Id},
                                                    '{Convert.ToString(entity.CreateAt)}',
                                                    '{entity.Name}',
                                                    {entity.Registration},
                                                    {entity.ChargeId},
                                                    {entity.EmployeerId},
                                                    '{entity.TotalHours}',
                                                    '{entity.Charge}')");

                    connection.Close();
                    scope.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<IList<PointWork>> CheckPointWorksasync(PointWork entity)
        {
            try
            {
                var connection = new MySqlConnection(conn);
                connection.Open();


                var result = await connection.QueryAsync<PointWork>($@"SELECT 
                                                                    p.id AS Id,
                                                                    p.CreateAt AS CreateAt
                                                                    FROM pointwork P 
                                                                    WHERE P.Id = {entity.Id}");
                connection.Close();
                return result.AsList<PointWork>();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
