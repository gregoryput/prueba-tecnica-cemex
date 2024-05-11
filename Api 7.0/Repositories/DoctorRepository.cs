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

        public async Task<IEnumerable<Doctor>> Get()
        {
            string query = @"SELECT IDDoctor, Nombre, Especialidad, Hospital FROM Doctor";
            IEnumerable<Doctor> response;

            using (var connection = _conexionDB.GetConnection(_configuration))
            {
                response = await connection.QueryAsync<Doctor>(query);
            }

            return response.ToList();
        }

        public async Task<IEnumerable<PacientesDTO>>  getPacientesByID(int IDdoctor)
        {
            string query = @"select a.IDDoctor, a.IDPaciente , p.Nombre, Edad, genero, Direccion , Telefono  from Asignacion_Paciente_Doctor a
                                INNER JOIN Paciente p ON p.IDPaciente = a.IDPaciente
                                INNER JOIN Doctor d ON d.IDDoctor = a.IDDoctor
                                WHERE d.IDDoctor =  @IDdoctor";

            IEnumerable<PacientesDTO> response;
            using (var connection = _conexionDB.GetConnection(_configuration))
            {
                response = await _conexionDB.GetConnection(_configuration).QueryAsync<PacientesDTO>(query, new { IDdoctor = IDdoctor });

            }

            return response.ToList();
        }

        public async Task<IEnumerable<PacientesDTO>> getPacientesNotRegistrado(int IDdoctor)
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

            IEnumerable<PacientesDTO> response;

            using (var connection = _conexionDB.GetConnection(_configuration))
            {
                response = await _conexionDB.GetConnection(_configuration).QueryAsync<PacientesDTO>(query, new { IDdoctor = IDdoctor });

            }

            return response.ToList();
        }


        public async Task AsignarPaciente(int IDPaciente, int IDDoctor)
        {
            using (var connection = _conexionDB.GetConnection(_configuration))
            {
                await connection.OpenAsync();

                try
                {
                    string query = @"INSERT INTO Asignacion_Paciente_Doctor (IDPaciente, IDDoctor)
                             VALUES
                             (@IDPaciente, @IDDoctor)";
                    await connection.ExecuteAsync(query, new { IDPaciente = IDPaciente, IDDoctor = IDDoctor }, commandType: CommandType.Text);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public async Task Insert(Doctor doctor)
        {
            using (var connection = _conexionDB.GetConnection(_configuration))
            {
                await connection.OpenAsync();

                try
                {
                    string query = @"INSERT INTO Doctor (Nombre, Especialidad, Hospital) VALUES (@Nombre, @Especialidad, @Hospital)";
                    await connection.ExecuteAsync(query, new { doctor.Nombre, doctor.Especialidad, doctor.Hospital }, commandType: CommandType.Text);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public async Task Update(Doctor doctor)
        {
            using (var connection = _conexionDB.GetConnection(_configuration))
            {
                await connection.OpenAsync();

                try
                {
                    string query = @"UPDATE Doctor 
                             SET Nombre = @Nombre, Especialidad = @Especialidad, Hospital = @Hospital
                             WHERE IDDoctor = @IDDoctor";

                    await connection.ExecuteAsync(query, new
                    {
                        doctor.Nombre,
                        doctor.Especialidad,
                        doctor.Hospital,
                        doctor.IDDoctor
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
