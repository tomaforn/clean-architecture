using AutoMapper;
using Shared.Application.Mappings;

namespace Modules.Todolist.Application.Queries.GetTodos
{
    public class WorkorderDto : IMapFrom<Workorder.Domain.Entities.Workorder>
    {
        public int Id;
        public string Name;
        public int Priority;
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Workorder.Domain.Entities.Workorder, WorkorderDto>()
                .ForMember(d => d.Priority, opt => opt.MapFrom(s => (int)s.Priority));
        }
    }
}