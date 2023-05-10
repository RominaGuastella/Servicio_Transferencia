using AutoMapper;
using Transferencias.App.DTOs;
using Transferencias.Persistence.Entities;
using Transferencias.Persistence.Repositories;

namespace Transferencias.App.Util
{
    public class MappingProfile : Profile
    {
        public MappingProfile() {
            CreateMap<TransferenciaRequestDTO, Transferencia>();
            CreateMap<Transferencia, TransferenciaResponseDTO>()
                .ForMember(dest => dest.cuentaOrigen,  opt => opt.MapFrom(src => $"{src.cbuOrigen.Substring(3,4)}-{src.cbuOrigen.Substring(8, 13)}"))
                .ForMember(dest => dest.cuentaDestino, opt => opt.MapFrom(src => $"{src.cbuDestino.Substring(3, 4)}-{src.cbuDestino.Substring(8, 13)}"));
        }
    }
}
