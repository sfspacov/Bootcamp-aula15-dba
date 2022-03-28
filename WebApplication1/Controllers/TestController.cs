using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestController : ControllerBase
    {
        private IConfiguration _configuration;

        public TestController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var connectionString = _configuration.GetConnectionString("DevConnection");

                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    const string cmdText = @"
                        SELECT
                            Nome as [Nome do Infeliz],
                            Idade as [Idade dele]
                        FROM usuario
                        WHERE Idade > 20
                        ORDER BY Nome
                        ";

                    var sqlCommand = new SqlCommand(cmdText, connection);
                    var usuarios = new List<Usuario>();

                    using (var reader = sqlCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var usuario = new Usuario
                            {
                                Idade = Convert.ToInt32(reader["Idade dele"]),
                                Nome = Convert.ToString(reader["Nome do Infeliz"]),
                            };

                            usuarios.Add(usuario);
                        }
                    }
                    return Ok(usuarios
                        .Select(x => new { NomeDoInfeliz = x.Nome, IdadeDele = x.Idade})
                    );
                }
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
    }
}