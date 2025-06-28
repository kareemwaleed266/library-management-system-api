using AutoMapper;
using Library.Data.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Service.TransactionService.Dtos
{
    public class TransactionProfile : Profile
    {
        public TransactionProfile()
        {
            CreateMap<TransactionResultDto,TransactionDto>().ReverseMap();
            CreateMap<Transaction,TransactionDto>().ReverseMap();
            CreateMap<TransactionResultDto, Transaction>()
                .ForPath(dest =>dest.UserId , options => options.MapFrom(src => src.UserId))
                .ReverseMap();
        }
    }
}
