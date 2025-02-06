using AutoMapper;
using BCity.Models;
using OA.Data.Entities;

namespace BCity.Services
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<ClientViewModel, Client>().ReverseMap();  
            CreateMap<ContactViewModel, Contact>().ReverseMap();          
            CreateMap<LinkClientContactViewModel, ClientContact>().ReverseMap();
        }
    }
}