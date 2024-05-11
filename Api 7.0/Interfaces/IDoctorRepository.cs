using Api_7._0.DTOs;
using Api_7._0.Models;

namespace Api_7._0.Interfaces
{
    public interface IDoctorRepository
    {
        public  Task<IEnumerable<Doctor>> Get();
        public Task<IEnumerable<PacientesDTO>> getPacientesByID(int IDdoctor);
        public Task<IEnumerable<PacientesDTO>> getPacientesNotRegistrado(int IDdoctor);
        public Task AsignarPaciente(int IDPaciente, int IDDoctor);
        public Task Insert(Doctor doctor);
        public Task Update(Doctor doctor);
     

    }
}
