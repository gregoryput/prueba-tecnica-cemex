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

        public IEnumerable<Paciente> get()
        {
            string query = @"select IDPaciente , Nombre, Edad, genero, Direccion , Telefono from Paciente ";
            var resultSet = _conexionDB.GetConnection(_configuration).Query<Paciente>(query);
            return resultSet.ToList();
        }

        public void Insert(Paciente paciente)
        {
            var connection = _conexionDB.GetConnection(_configuration);
            connection.Open();

            try
            {
                string query = @"INSERT INTO Paciente (Nombre, Edad, Genero, Direccion, Telefono)
                 VALUES (@Nombre, @Edad, @Genero, @Direccion, @Telefono)";

                connection.Execute(query, new
                {
                   Nombre =  paciente.Nombre,
                   Edad = paciente.Edad,
                   Genero =  paciente.Genero,
                   Direccion =  paciente.Direccion,
                   Telefono =  paciente.Telefono,
                   //IDDoctor = paciente.IDDoctor
                }, commandType: CommandType.Text);
            }
            catch (Exception ex)
            {

                connection.Close();
                throw ex;
            }
            connection.Close();
        }


        public void Update(Paciente paciente)
        {
            var connection = _conexionDB.GetConnection(_configuration);
            connection.Open();


            try
            {
                string query = @"UPDATE Paciente
                 SET Nombre = @Nombre, Edad = @Edad, Genero = @Genero, Direccion = @Direccion, Telefono = @Telefono
                 WHERE IDPaciente = @IDPaciente";

                connection.Execute(query, new
                {
                    paciente.IDPaciente,
                    paciente.Nombre,
                    paciente.Edad,
                    paciente.Genero,
                    paciente.Direccion,
                    paciente.Telefono
                }, commandType: CommandType.Text);



            }
            catch (Exception ex)
            {

                connection.Close();
                throw ex;
            }
            connection.Close();
        }

    
        public void Delete(int IDpaciente)
        {
            var connection = _conexionDB.GetConnection(_configuration);
            connection.Open();
         

            try
            {
                string query = @"DELETE FROM Paciente WHERE IDPaciente = @IDPaciente;";

                connection.Execute(query, new { IDpaciente = IDpaciente }, commandType: CommandType.Text);

               
            }

            catch (Exception ex)
            {
               
                connection.Close();
                throw ex;
            }
            connection.Close();
        }

    }
}
