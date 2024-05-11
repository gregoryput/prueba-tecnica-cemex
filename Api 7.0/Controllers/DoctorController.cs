using Api_7._0.Interfaces;
using Api_7._0.Models;
using Api_7._0.Repositories;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Api_7._0.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController : ControllerBase
    {


        protected Respuesta _respuesta;
        private readonly IConfiguration _configuration;
        private readonly IDoctorRepository _doctorRepository;

        public DoctorController(IConfiguration configuration)
        {
            _respuesta = new Respuesta();
            _configuration = configuration;
            _doctorRepository = new DoctorRepository(_configuration);
        }


        //[Authorize]
        [Route("Get")]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var lista = await _doctorRepository.Get();
                _respuesta.Result = lista;
                _respuesta.DisplayMessage = "Listado obtenido con éxito";
            }
            catch (Exception ex)
            {
                _respuesta.IsSuccess = false;
                _respuesta.DisplayMessage = "Error al solicitar la lista de Doctor";
                _respuesta.ErrorMessages = new List<string> { ex.ToString() };
            }

            return Ok(_respuesta);
        }



        [Route("GetPacientesByDoctor")]
        [HttpGet]
        public async Task<IActionResult> GetPacienteByDoctor(int IDDoctor)
        {
            try
            {
                var lista = await _doctorRepository.getPacientesByID(IDDoctor);
                _respuesta.Result = lista;
                _respuesta.DisplayMessage = "Listado  obtenido con exito";
            }
            catch (Exception ex)
            {
                _respuesta.IsSuccess = false;
                _respuesta.DisplayMessage = "Error al solicitar la lista de Paciente";
                _respuesta.ErrorMessages = new List<string> { ex.ToString() };
            }

            return Ok(_respuesta);

        }

        [Route("GetPacienteNotRegistrado")]
        [HttpGet]
        public async Task<IActionResult> GetPacienteNotRegistrado(int IDDoctor)
        {
            try
            {
                var lista = await _doctorRepository.getPacientesNotRegistrado(IDDoctor);
                _respuesta.Result = lista;
                _respuesta.DisplayMessage = "Listado de usuario obtenido con éxito";
            }
            catch (Exception ex)
            {
                _respuesta.IsSuccess = false;
                _respuesta.DisplayMessage = "Error al solicitar la lista de Paciente";
                _respuesta.ErrorMessages = new List<string> { ex.ToString() };
            }

            return Ok(_respuesta);
        }

        [Route("Insert")]
        [HttpPost]
        public async Task<IActionResult> Insert(Doctor doctor)
        {
            try
            {
                await _doctorRepository.Insert(doctor);
                _respuesta.IsSuccess = true;
                _respuesta.DisplayMessage = "Éxito";
                return Ok(_respuesta);
            }
            catch (Exception ex)
            {
                _respuesta.IsSuccess = false;
                _respuesta.DisplayMessage = "Error al insertar Doctor";
                _respuesta.ErrorMessages = new List<string> { ex.ToString() };
                return StatusCode(500, _respuesta);
            }
        }

        [Route("AsignarPaciente")]
        [HttpPost]
        public async Task<IActionResult> AsignarPaciente(int IDPaciente, int IDDoctor)
        {
            try
            {
                await _doctorRepository.AsignarPaciente(IDPaciente, IDDoctor);
                _respuesta.IsSuccess = true;
                _respuesta.DisplayMessage = "Asignado correctamente";
                return Ok(_respuesta);
            }
            catch (Exception ex)
            {
                _respuesta.IsSuccess = false;
                _respuesta.DisplayMessage = "Error al asignar Paciente";
                _respuesta.ErrorMessages = new List<string> { ex.ToString() };
                return StatusCode(500, _respuesta);
            }
        }

        [Route("Update")]
        [HttpPut]
        public async Task<IActionResult> Actualiza(Doctor doctor)
        {
            try
            {
                await _doctorRepository.Update(doctor);
                _respuesta.Result = doctor;
                _respuesta.DisplayMessage = "Actualizado correctamente";
                return Ok(_respuesta);
            }
            catch (Exception ex)
            {
                _respuesta.IsSuccess = false;
                _respuesta.DisplayMessage = "Error al actualizar";
                _respuesta.ErrorMessages = new List<string> { ex.ToString() };
                return StatusCode(500, _respuesta);
            }
        }

      
    }
}
