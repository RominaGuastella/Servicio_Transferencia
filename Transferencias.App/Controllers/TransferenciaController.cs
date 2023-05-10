using Microsoft.AspNetCore.Mvc;
using Transferencias.App.DTOs;
using Transferencias.App.Services;

namespace Transferencias.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TransferenciaController : ControllerBase
    {
        private readonly TransferenciaService _service;
        public TransferenciaController(TransferenciaService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<TransferenciaResponseDTO> Post(TransferenciaRequestDTO transferenciaRequestDTO)
        {
            return await _service.Create(transferenciaRequestDTO);
        }

        [HttpGet]
        public async Task<IEnumerable<TransferenciaResponseDTO>> Get()
        {
            return await _service.GetAll();
        }

        [HttpDelete("{id}")]
        public async Task<TransferenciaResponseDTO> Eliminar(int id)
        {
            return await _service.Delete(id);
        }

        [HttpGet("{id}")]
        public async Task<TransferenciaResponseDTO> GetById(int id)
        {
            return await _service.GetById(id);
        }

        [HttpPut("{id}")]
        public async Task<TransferenciaResponseDTO> Update(int id,TransferenciaRequestDTO transferenciaRequestDTO)
        { 
            return await _service.Update(id, transferenciaRequestDTO);
        }
    }
}