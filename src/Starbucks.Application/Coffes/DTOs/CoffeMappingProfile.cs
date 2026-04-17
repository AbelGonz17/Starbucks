using Core.Mappy.Interfaces;
using Starbucks.Domain;

namespace Starbucks.Application.Coffes.DTOs;

public class CoffeMappingProfile : IMappingProfile
{
    public void Configure(IMapper mapper)
    {
        mapper.CreateMap<Coffe, CoffeResponse>(cfg =>
        {
            cfg.Map(dest => dest.CoffeId, src => src.Id);
            cfg.Map(dest => dest.Name, src => src.Name);
            cfg.Map(dest => dest.Description, src => src.Description);
            cfg.Map(dest => dest.Price, src => src.Price);
            cfg.Map(dest => dest.CategoryId, src => src.CategoryId);
        });
    }
}
