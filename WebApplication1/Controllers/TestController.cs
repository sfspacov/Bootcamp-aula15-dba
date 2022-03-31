using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
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
        private string _connectionString;

        public TestController(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("DevConnection");
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    const string cmdText = "BuscarUsuarios";
                    var sqlCommand = new SqlCommand(cmdText, connection);
					sqlCommand.CommandType = CommandType.StoredProcedure;
					
                    var usuarios = new List<Usuario>();

                    using (var reader = sqlCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var usuario = new Usuario
                            {
								Id =  = Convert.ToInt32(reader["Id"]),
                                Idade = Convert.ToInt32(reader["Idade dele"]),
                                Nome = Convert.ToString(reader["Nome do Infeliz"]),
                            };

                            usuarios.Add(usuario);
                        }
                    }
                    return Ok(usuarios
                        .Select(x => new { NomeDoInfeliz = x.Nome, IdadeDele = x.Idade })
                    );
                }
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public IActionResult Post(Usuario usuarioParam)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    const string cmdText = "InserirUsuario";

                    var sqlCommand = new SqlCommand(cmdText, connection);
					sqlCommand.CommandType = CommandType.StoredProcedure;

                    sqlCommand.Parameters.Add("@Nome", SqlDbType.VarChar);
                    sqlCommand.Parameters["@Nome"].Value = usuarioParam.Nome;

                    sqlCommand.Parameters.Add("@Idade", SqlDbType.Int);
                    sqlCommand.Parameters["@Idade"].Value = usuarioParam.Idade;

                    var newId = sqlCommand.ExecuteScalar();

                    return Ok(newId);
                }
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpPut]
        public IActionResult Put(int id, Usuario usuarioParam)
        {
            try
            {
                if (string.IsNullOrEmpty(usuarioParam.Nome) || usuarioParam.Idade < 1)
                    throw new ArgumentNullException();

                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    const string cmdText = @"
                        UPDATE usuario
                        SET 
                            idade = @Idade,
                            nome = @Nome
                        WHERE Id = @Id
                        ";

                    var sqlCommand = new SqlCommand(cmdText, connection);

                    sqlCommand.Parameters.Add("@Id", SqlDbType.Int);
                    sqlCommand.Parameters["@Id"].Value = id;

                    sqlCommand.Parameters.Add("@Nome", SqlDbType.VarChar);
                    sqlCommand.Parameters["@Nome"].Value = usuarioParam.Nome;

                    sqlCommand.Parameters.Add("@Idade", SqlDbType.Int);
                    sqlCommand.Parameters["@Idade"].Value = usuarioParam.Idade;

                    var result = sqlCommand.ExecuteNonQuery();

                    return Ok(result);
                }
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
		
		[HttpDelete]
        public IActionResult Delete(int id)
        {
            try
            {
                if (id < 1)
                    throw new ArgumentNullException();

                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    const string cmdText = @"
                        DELETE usuario
                        WHERE Id = @Id
                        ";

                    var sqlCommand = new SqlCommand(cmdText, connection);

                    sqlCommand.Parameters.Add("@Id", SqlDbType.Int);
                    sqlCommand.Parameters["@Id"].Value = id;

                    var result = sqlCommand.ExecuteNonQuery();

                    return Ok(result);
                }
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
    
	}
}