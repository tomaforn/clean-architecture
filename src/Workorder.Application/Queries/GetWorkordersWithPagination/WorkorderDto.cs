using AutoMapper;
using Shared.Application.Mappings;

namespace Modules.Workorder.Application.Queries
{
    public class WorkorderDto : IMapFrom<Domain.Entities.Workorder>
    {
        public int Id;
        public string Name;
        public int Priority;
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.Entities.Workorder, WorkorderDto>()
                .ForMember(d => d.Priority, opt => opt.MapFrom(s => (int)s.Priority));
        }
    }
}