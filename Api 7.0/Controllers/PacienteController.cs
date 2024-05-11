using Api_7._0.Interfaces;
using Api_7._0.Models;
using Api_7._0.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Api_7._0.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PacienteController : ControllerBase
    {
        protected Respuesta _respuesta;
        private readonly IConfiguration _configuration;
        private readonly IPacienteRepository _pacienteRepository;

        public PacienteController(IConfiguration configuration)
        {
            _respuesta = new Respuesta();
            _configuration = configuration;
            _pacienteRepository = new PacienteRepository(_configuration);
        }


        //[Authorize]
        [Route("Get")]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var lista = await _pacienteRepository.Get();
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

        //[Authorize]
        [Route("Insert")]
        [HttpPost]
        public async Task<IActionResult> Insert(Paciente paciente)
        {
            try
            {
                await  _pacienteRepository.Insert(paciente);
                _respuesta.IsSuccess = true;
                _respuesta.DisplayMessage = "Éxito al insertar paciente";
                return Ok(_respuesta);
            }
            catch (Exception ex)
            {
                _respuesta.IsSuccess = false;
                _respuesta.DisplayMessage = "Error al insertar paciente";
                _respuesta.ErrorMessages = new List<string> { ex.ToString() };
                return StatusCode(500, _respuesta);
            }
        }

        [Route("Update")]
        [HttpPut]
        public async Task<IActionResult> ActualizarUsuario(Paciente paciente)
        {
            try
            {
                 await  _pacienteRepository.Update(paciente);
                _respuesta.Result = paciente;
                _respuesta.DisplayMessage = "Actualizado correctamente";
                return Ok(_respuesta);
            }
            catch (Exception ex)
            {
                _respuesta.IsSuccess = false;
                _respuesta.DisplayMessage = "Error al actualizar paciente";
                _respuesta.ErrorMessages = new List<string> { ex.ToString() };
                return StatusCode(500, _respuesta);
            }
        }


    }
}
