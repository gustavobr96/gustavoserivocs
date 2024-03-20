using AutoMapper;
using SistemaBico.Web.Models;
using SistemaBico.Web.Util;

namespace SistemaBico.Web.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {

            _ = CreateMap<ProfessionalProfileDto, ProfessionalProfile>()
                 .ForMember(dst => dst.Name, map => map.MapFrom(src => src.Name))
                 .ForMember(dst => dst.LastName, map => map.MapFrom(src => src.LastName))
                 .ForMember(dst => dst.Phone, map => map.MapFrom(src => src.Phone))
                 .ForMember(dst => dst.ClientId, map => Guid.NewGuid())
                 .ForMember(dst => dst.About, map => map.MapFrom(src => src.Sobre))
                 .ForMember(dst => dst.Profession, map => map.MapFrom(src => src.Profession))
                 .ForMember(dst => dst.Address, map => map.MapFrom(src =>  
                     src.Logradouro != null ?
                     new Address
                     {
                         Id = Guid.NewGuid(),
                         Logradouro = src.Logradouro,
                         Number = !string.IsNullOrEmpty(src.Number) ? src.Number : "",
                         Bairro = !string.IsNullOrEmpty(src.Bairro) ? src.Bairro : "",
                         Complemento = !string.IsNullOrEmpty(src.Complemento) ? src.Complemento : "",
                         ZipCode = !string.IsNullOrEmpty(src.CEP) ? src.CEP : "",
                         City = !string.IsNullOrEmpty(src.City) ? src.City : "",
                         State = !string.IsNullOrEmpty(src.State) ? src.State : ""
                     } : null
                  ))
                 .ForMember(dst => dst.PerfilPicture, map => map.MapFrom(src => src.File != null ? ConvertGeneric.IFormFileToBase64(src.File) : null))
                 .ForMember(dst => dst.ProfissionalArea, map => map.MapFrom(src => new ProfessionalArea { Codigo = src.Area }))
                 .ForMember(dst => dst.Especiality, map => map.MapFrom(src => src.Especiality.ConvertAll(s => new ProfessionalEspeciality { Id = Guid.NewGuid(), Description = s })));

            _ = CreateMap<ClientDto, ApplicationUser>()
                 .ForMember(dst => dst.Client, map => map.MapFrom(src => 
                 new ClientProfile 
                 { 
                   Name = src.Name,
                   LastName = src.LastName,
                   PerfilPicture = src.File != null ? ConvertGeneric.IFormFileToBase64(src.File) : null
                 }))
                 .ForMember(dst => dst.PhoneNumber, map => map.MapFrom(src => src.PhoneNumber));

            _ = CreateMap<WorkerDto, Worker>()
                .ForMember(dst => dst.Price, map => map.MapFrom(src => src.Price))
                .ForMember(dst => dst.Title, map => map.MapFrom(src => src.Titulo))
                .ForMember(dst => dst.Phone, map => map.MapFrom(src => src.Phone))
                .ForMember(dst => dst.About, map => map.MapFrom(src => src.Sobre))
                .ForMember(dst => dst.Address, map => map.MapFrom(src =>
                    !string.IsNullOrEmpty(src.CEP) || !string.IsNullOrEmpty(src.City) ?
                     new Address
                     {
                         Id = Guid.NewGuid(),
                         ZipCode = !string.IsNullOrEmpty(src.CEP) ? src.CEP : "",
                         City = !string.IsNullOrEmpty(src.City) ? src.City : "",
                         State = !string.IsNullOrEmpty(src.State) ? src.State : ""
                     } : null
                  ))
                .ForMember(dst => dst.ProfessionalArea, map => map.MapFrom(src => new ProfessionalArea { Codigo = src.Area }))
                .ForMember(dst => dst.Profession, map => map.MapFrom(src => src.Profession));
        }
    }
}
