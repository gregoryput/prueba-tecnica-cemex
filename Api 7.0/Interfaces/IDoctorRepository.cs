using Api_7._0.DTOs;
using Api_7._0.Models;

namespace Api_7._0.Interfaces
{
    public interface IDoctorRepository
    {
        public IEnumerable<Doctor> get();
        public IEnumerable<PacientesDTO> getPacientesByID(int IDdoctor);
        public IEnumerable<PacientesDTO> getPacientesNotRegistrado(int IDdoctor);
        public void AsignarPaciente(int IDPaciente, int IDDoctor);

        public void Insert(Doctor doctor);
        public void Update(Doctor doctor);
        public void Delete(int IDdoctor);

    }
}
