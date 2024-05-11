using Api_7._0.Models;

namespace Api_7._0.Interfaces
{
    public interface IPacienteRepository
    {
        public Task<IEnumerable<Paciente>> Get();
        public Task Insert(Paciente paciente);
        public Task Update(Paciente paciente);

    }
}
