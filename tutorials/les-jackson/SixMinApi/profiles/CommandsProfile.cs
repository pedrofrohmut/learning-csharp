using AutoMapper;
using SixMinApi.Models;
using SixMinApi.Dtos;

namespace SixMinApi.Profiles
{
    public class CommandsProfile : Profile
    {
        public CommandsProfile()
        {
            // <Source, Target>
            CreateMap<Command, CommandReadDto>();
            CreateMap<CommandCreateDto, Command>();
            CreateMap<CommandUpdateDto, Command>();
            CreateMap<CommandReadDto, Command>();
        }
    }
}
