using Api_7._0.Models;

namespace Api_7._0.Interfaces
{
    public interface IPacienteRepository
    {
        public IEnumerable<Paciente> get();
        public void Insert(Paciente paciente);
        public void Update(Paciente paciente);
        public void Delete(int IDpaciente);

    }
}
