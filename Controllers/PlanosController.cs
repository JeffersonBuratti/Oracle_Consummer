using Microsoft.AspNetCore.Mvc;
using Oracle.ManagedDataAccess.Client;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace Oracle_Consummer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlanosController : ControllerBase
    {
        private readonly string _connectionString;

        // Construtor que recebe a string de conexão do appsettings.json
        public PlanosController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("OracleDbConnection");
        }

        // GET api/planos
        [HttpGet]
        public IActionResult GetPlanosDisponiveis()
        {
            try
            {
                var planos = new List<object>();

                using (var conn = new OracleConnection(_connectionString))
                {
                    conn.Open();

                    // Ajusta o fuso horário da sessão para 'America/Sao_Paulo'
                    // Isso garante que o Oracle use o mesmo fuso que você configurou no banco.
                    using (var cmdTimezone = new OracleCommand("ALTER SESSION SET TIME_ZONE = 'GMT'", conn))
                    {
                        cmdTimezone.ExecuteNonQuery();  // Executa a configuração do fuso horário da sessão
                    }

                    // Consulta para obter os dados da view PLANOS_DISPONIVEIS
                    using (var cmd = new OracleCommand("SELECT NNUMEPLAN, CCODIPLAN, CDESCPLAN FROM PLANOS_DISPONIVEIS", conn))
                    {
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var plano = new
                                {
                                    Nnumeplan = reader["NNUMEPLAN"],  // Tipo INT
                                    Ccodiplan = reader["CCODIPLAN"],  // Tipo INT
                                    Cdescplan = reader["CDESCPLAN"],  // Tipo VARCHAR
                                };
                                planos.Add(plano);
                            }
                        }
                    }
                }

                return Ok(planos);
            }
            catch (OracleException ex)
            {
                return StatusCode(500, $"Erro ao acessar o banco de dados: {ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro inesperado: {ex.Message}");
            }
        }
    }
}
