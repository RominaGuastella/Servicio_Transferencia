using AutoMapper;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using Transferencias.App.DTOs;
using Transferencias.Persistence.Entities;
using Transferencias.Persistence.Repositories;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Transferencias.App.Services
{
    public class TransferenciaService: IService <TransferenciaRequestDTO,TransferenciaResponseDTO>
    {
        private readonly TransferenciaRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<TransferenciaService> _logger;

        public TransferenciaService(TransferenciaRepository repository, IMapper mapper, ILogger<TransferenciaService> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        // Se crea una nueva transferencia
        public async Task<TransferenciaResponseDTO> Create(TransferenciaRequestDTO request)
        {
            Transferencia transferencia_PRE = _mapper.Map<Transferencia>(request);
            // ToDo: Validar CUIL de Origen y destino
            if (Validador(transferencia_PRE.cuilOriginante,transferencia_PRE.cuilDestinatario) )
            {
                transferencia_PRE.resultado = "FINALIZADA";
            }
            else
            { 
                transferencia_PRE.resultado = "RECHAZADA";
            }
        
        var trfJson = JsonSerializer.Serialize(transferencia_PRE);

            try
            {
                Transferencia transferencia_POS = await _repository.Add(transferencia_PRE);
                _logger.LogInformation($"Se insertó una nueva Transferencia: {trfJson}");
                return _mapper.Map<TransferenciaResponseDTO>(transferencia_POS);
            }
            catch (Exception)
            {
                _logger.LogError($"Error al insertar la transferencia: {trfJson}");
                throw;
            } 
        }

        // Se borra una transferencia
        public async Task<TransferenciaResponseDTO> Delete(int id)
        {
            try
            {
                Transferencia transferencia = await _repository.Delete(id);
                // ToDo: Validar si existe la transferencia que se intenta borrar o si el resultado del Delete es NULL
                _logger.LogInformation($"Se eliminó la Transferencia: {JsonSerializer.Serialize(transferencia)}");
                return _mapper.Map<TransferenciaResponseDTO>(transferencia);
            }
            catch (Exception)
            {
                _logger.LogError($"Error al eliminar la transferencia por ID: {id}");
                throw;
            }
        }

        // Se obtienen todas las transferencias de la tabla
        public async Task<IEnumerable<TransferenciaResponseDTO>> GetAll()
        {
            try
            {
                IEnumerable<Transferencia> transferencias = await _repository.GetAll();
                _logger.LogInformation($"Se obtuvieron todas las Transferencias: {JsonSerializer.Serialize(transferencias)}");
                return _mapper.Map<List<TransferenciaResponseDTO>>(transferencias);
            }
            catch (Exception) 
            {
                _logger.LogError($"Error al obtener todas las transferencias");
                throw;
            }
        }

        // Se obtiene una sola transferencia por ID
        public async Task<TransferenciaResponseDTO> GetById(int id)
        {
            try
            {
                Transferencia transferencia = await _repository.GetById(id);
                // ToDo: Validar si existe la transferencia que se intenta obtener o si el resultado del GetById es NULL
                _logger.LogInformation($"Se obtuvo la Transferencia: {JsonSerializer.Serialize(transferencia)}");
                return _mapper.Map<TransferenciaResponseDTO>(transferencia);
            }
            catch (Exception) 
            {
                _logger.LogError($"Error al obtener la transferencia por ID: {id}");
                throw;
            }
        }

        // Se modifican los campos de una transferencia por ID
        public async Task<TransferenciaResponseDTO> Update(int id, TransferenciaRequestDTO request)
        {
            Transferencia transferencia_PRE = await _repository.GetById(id);
            // ToDo: Validar si existe la transferencia que se intenta obtener o si el resultado del GetById es NULL
            var trfPreJson = JsonSerializer.Serialize(transferencia_PRE);
            _logger.LogInformation($"Se modifica la Transferencia(Antes): {trfPreJson}");

            try
            {
                transferencia_PRE.cuilOriginante = request.cuilOriginante;
                transferencia_PRE.cuilDestinatario = request.cuilDestinatario;
                transferencia_PRE.cbuDestino = request.cbuDestino;
                transferencia_PRE.cbuOrigen = request.cbuOrigen;
                transferencia_PRE.importe = request.importe;
                transferencia_PRE.concepto = request.concepto;
                transferencia_PRE.descripcion = request.descripcion;

                Transferencia transferencia_POS = await _repository.Update(transferencia_PRE);
                _logger.LogInformation($"Se modifica la Transferencia(Después): {JsonSerializer.Serialize(transferencia_POS)}");
                return _mapper.Map<TransferenciaResponseDTO>(transferencia_POS);
            }
            catch (Exception)
            {
                _logger.LogError($"Error al modificar la transferencia: {trfPreJson}");
                throw;
            }    
        }
        public Boolean Validador(string cuilOrigen, string cuilDestino)
        {
            int validaO = 0;
            int validaD = 0;
            Boolean valida = false;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7063/api/");

                //HTTP GET
                var responseTask = client.GetAsync("clientes");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    var readTask = result.Content.ReadAsAsync<Cliente[]>();
                    readTask.Wait();

                    var clientes = readTask.Result;

                    foreach (var cliente in clientes)
                    {
                        if (cuilOrigen == cliente.cuil)
                        { validaO = 1; }
                        if (cuilDestino == cliente.cuil)
                        { validaD = 1; }    
                    }
                }
            }
            if (validaO + validaD == 2)
                valida = true;
            else
                valida = false;

            return valida;


        }


    }
}