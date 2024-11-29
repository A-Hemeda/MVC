using AutoMapper;
using Store.Core.Dtos;
using Store.Core.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Core.AutoMapping.Auth
{
    public class AuthProfile : Profile
    {

        public AuthProfile()
        {
            CreateMap<Address,AddressDto>().ReverseMap();
        }
    }
}
