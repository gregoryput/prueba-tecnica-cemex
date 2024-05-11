using Api_7._0.Interfaces;
using Api_7._0.Models;
using Dapper;
using System.Data;
using System.Numerics;

namespace Api_7._0.Repositories
{
    public  class PacienteRepository : IPacienteRepository
    {
        private readonly IConfiguration _configuration;
        private readonly ConexionDB _conexionDB;

        public PacienteRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            _conexionDB = new ConexionDB();
        }

        public async Task<IEnumerable<Paciente>> Get()
        {
            string query = @"SELECT IDPaciente, Nombre, Edad, Genero, Direccion, Telefono FROM Paciente";
            IEnumerable<Paciente> resultSet;

            using (var connection = _conexionDB.GetConnection(_configuration))
            {
                resultSet = await connection.QueryAsync<Paciente>(query);
            }

            return resultSet.ToList();
        }

        public async Task Insert(Paciente paciente)
        {
            using (var connection = _conexionDB.GetConnection(_configuration))
            {
                await connection.OpenAsync();

                try
                {
                    string query = @"INSERT INTO Paciente (Nombre, Edad, Genero, Direccion, Telefono)
                             VALUES (@Nombre, @Edad, @Genero, @Direccion, @Telefono)";

                    await connection.ExecuteAsync(query, new
                    {
                        paciente.Nombre,
                        paciente.Edad,
                        paciente.Genero,
                        paciente.Direccion,
                        paciente.Telefono
                    }, commandType: CommandType.Text);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public async Task Update(Paciente paciente)
        {
            using (var connection = _conexionDB.GetConnection(_configuration))
            {
                await connection.OpenAsync();

                try
                {
                    string query = @"UPDATE Paciente
                             SET Nombre = @Nombre, Edad = @Edad, Genero = @Genero, Direccion = @Direccion, Telefono = @Telefono
                             WHERE IDPaciente = @IDPaciente";

                    await connection.ExecuteAsync(query, new
                    {
                        paciente.IDPaciente,
                        paciente.Nombre,
                        paciente.Edad,
                        paciente.Genero,
                        paciente.Direccion,
                        paciente.Telefono
                    }, commandType: CommandType.Text);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

       


    }
}
