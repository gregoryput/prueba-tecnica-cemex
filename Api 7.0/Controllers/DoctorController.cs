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
        public IActionResult Get()
        {
            try
            {
                var lista = _doctorRepository.get();
                _respuesta.Result = lista;
                _respuesta.DisplayMessage = "Listado de usuario obtenido con exito";
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
        public IActionResult GetPacienteByDoctor(int IDDoctor)
        {
            try
            {
                var lista = _doctorRepository.getPacientesByID(IDDoctor);
                _respuesta.Result = lista;
                _respuesta.DisplayMessage = "Listado de usuario obtenido con exito";
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
        public IActionResult GetPacienteNotRegistrado(int IDDoctor)
        {
            try
            {
                var lista = _doctorRepository.getPacientesNotRegistrado(IDDoctor);
                _respuesta.Result = lista;
                _respuesta.DisplayMessage = "Listado de usuario obtenido con exito";
            }
            catch (Exception ex)
            {
                _respuesta.IsSuccess = false;
                _respuesta.DisplayMessage = "Error al solicitar la lista de Paciente";
                _respuesta.ErrorMessages = new List<string> { ex.ToString() };
            }

            return Ok(_respuesta);

        }


        //[Authorize]
        [Route("Insert")]
        [HttpPost]
        public async Task<IActionResult> Insert(Doctor doctor)
        {

            try
            {

                _doctorRepository.Insert(doctor);
                _respuesta.IsSuccess = true;
                _respuesta.DisplayMessage = "Exito";
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

                _doctorRepository.AsignarPaciente(IDPaciente, IDDoctor);
                _respuesta.IsSuccess = true;
                _respuesta.DisplayMessage = "Asignado correctamente";
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



        [Route("Update")]
        [HttpPut]
        public IActionResult Actualiza(Doctor doctor)
        {

            try
            {

                _doctorRepository.Update(doctor);
                _respuesta.Result = doctor;
                _respuesta.DisplayMessage = " Actualizado correctamente";
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



        //[Authorize]
        [Route("Delete")]
        [HttpDelete]
        public IActionResult eliminar(int IDoctor)
        {
            try
            {
                _doctorRepository.Delete(IDoctor);
                _respuesta.Result = IDoctor;
                _respuesta.DisplayMessage = "Eliminado correctamente";
            }

            catch (Exception ex)
            {
                _respuesta.IsSuccess = false;
                _respuesta.DisplayMessage = "Error al eliminar";
                _respuesta.ErrorMessages = new List<string> { ex.ToString() };
            }

            return Ok(_respuesta);
        }



    }
}
