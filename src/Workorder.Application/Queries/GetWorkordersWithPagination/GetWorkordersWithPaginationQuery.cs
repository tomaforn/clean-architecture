using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Modules.Workorder.Application.Interfaces;
using Shared.Application.Mappings;
using Shared.Application.Models;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Modules.Workorder.Application.Queries
{
    public class GetWorkordersWithPaginationQuery : IRequest<PaginatedList<WorkorderDto>>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }

    public class GetWorkordersWithPaginationQueryHandler : IRequestHandler<GetWorkordersWithPaginationQuery, PaginatedList<WorkorderDto>>
    {
        private readonly IWorkorderDbContext _context;
        private readonly IMapper _mapper;

        public GetWorkordersWithPaginationQueryHandler(IWorkorderDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PaginatedList<WorkorderDto>> Handle(GetWorkordersWithPaginationQuery request, CancellationToken cancellationToken)
        {
            return await _context.Workorders
                .OrderBy(x => x.Name)
                .ProjectTo<WorkorderDto>(_mapper.ConfigurationProvider)
                .PaginatedListAsync(request.PageNumber, request.PageSize);
        }
    }
}
