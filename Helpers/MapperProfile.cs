
using ArmazemAPI.Dto;
using ArmazemAPI.Models;
using AutoMapper;


namespace ArmazemAPI.Helpers
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Produto, ProdutoDto>().ReverseMap();
            CreateMap<Usuario, UsuarioDto>().ReverseMap();
            CreateMap<ClienteFornecedor, CliForDto>().ReverseMap();
            CreateMap<CompraVenda, CompraVendaDto>().ReverseMap();
            CreateMap<Item, ItemDto>().ReverseMap();
        }
    }
}