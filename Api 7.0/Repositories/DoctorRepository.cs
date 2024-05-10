using Api_7._0.DTOs;
using Api_7._0.Interfaces;
using Api_7._0.Models;
using Dapper;
using System.Data;
using System.Numerics;

namespace Api_7._0.Repositories
{
    public  class DoctorRepository : IDoctorRepository
    {
        private readonly IConfiguration _configuration;
        private readonly ConexionDB _conexionDB;

        public DoctorRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            _conexionDB = new ConexionDB();
        }

        public IEnumerable<Doctor> get()
        {
            string query = @"select IDDoctor, Nombre , Especialidad, Hospital from Doctor";
            var resultSet = _conexionDB.GetConnection(_configuration).Query<Doctor>(query);
            return resultSet.ToList();
        }

        public IEnumerable<PacientesDTO> getPacientesByID(int IDdoctor)
        {
            string query = @"select a.IDDoctor, a.IDPaciente , p.Nombre, Edad, genero, Direccion , Telefono  from Asignacion_Paciente_Doctor a
                                INNER JOIN Paciente p ON p.IDPaciente = a.IDPaciente
                                INNER JOIN Doctor d ON d.IDDoctor = a.IDDoctor
                                WHERE d.IDDoctor =  @IDdoctor";

            var resultSet = _conexionDB.GetConnection(_configuration).Query<PacientesDTO>(query ,new { IDdoctor = IDdoctor });
            return resultSet.ToList();
        }

        public IEnumerable<PacientesDTO> getPacientesNotRegistrado(int IDdoctor)
        {
            string query = @"SELECT a.IDDoctor, p.IDPaciente, p.Nombre, p.Edad, p.Genero, p.Direccion, p.Telefono
                            FROM Paciente p
                            LEFT JOIN Asignacion_Paciente_Doctor a ON p.IDPaciente = a.IDPaciente
                            WHERE (a.IDDoctor IS NULL OR a.IDDoctor <> @IDdoctor)
                            AND p.IDPaciente NOT IN (
                                SELECT IDPaciente
                                FROM Asignacion_Paciente_Doctor
                                WHERE IDDoctor = @IDdoctor 
                            ); ";

            var resultSet = _conexionDB.GetConnection(_configuration).Query<PacientesDTO>(query, new { IDdoctor = IDdoctor });
            return resultSet.ToList();
        }


        public void AsignarPaciente( int IDPaciente,int IDDoctor)
        {
            var connection = _conexionDB.GetConnection(_configuration);
            connection.Open();

            try
            {
                string query = @"INSERT INTO Asignacion_Paciente_Doctor (IDPaciente, IDDoctor)
                                    VALUES
                                    (@IDPaciente, @IDDoctor)";
                connection.Execute(query, new { IDPaciente = IDPaciente, IDDoctor = IDDoctor, }, commandType: CommandType.Text);
            }
            catch (Exception ex)
            {

                connection.Close();
                throw ex;
            }
            connection.Close();
        }

        public void Insert(Doctor doctor)
        {
            var connection = _conexionDB.GetConnection(_configuration);
            connection.Open();

            try
            {
                string query = @"INSERT INTO Doctor (Nombre, Especialidad, Hospital) VALUES (@Nombre, @Especialidad, @Hospital)";
                connection.Execute(query, new { Nombre = doctor.Nombre, Especialidad = doctor.Especialidad, Hospital = doctor.Hospital }, commandType: CommandType.Text);
            }
            catch (Exception ex)
            {

                connection.Close();
                throw ex;
            }
            connection.Close();
        }




        public void Update(Doctor doctor)
        {
            var connection = _conexionDB.GetConnection(_configuration);
            connection.Open();


            try
            {

                string query = @"UPDATE Doctor 
                     SET Nombre = @Nombre, Especialidad = @Especialidad, Hospital = @Hospital
                     WHERE IDDoctor = @IDDoctor";

                connection.Execute(query, new
                {
                    doctor.Nombre,
                    doctor.Especialidad,
                    doctor.Hospital,
                    doctor.IDDoctor
                }, commandType: CommandType.Text);

            }
            catch (Exception ex)
            {

                connection.Close();
                throw ex;
            }
            connection.Close();
        }

    
        public void Delete(int IDdoctor)
        {
            var connection = _conexionDB.GetConnection(_configuration);
            connection.Open();
         

            try
            {
                string query = @"DELETE FROM Doctor WHERE IDdoctor = @IDdoctor";

                connection.Execute(query, new { IDdoctor = IDdoctor }, commandType: CommandType.Text);

               
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
