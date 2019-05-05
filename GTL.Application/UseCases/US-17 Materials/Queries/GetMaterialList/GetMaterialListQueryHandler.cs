using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using GTL.Application.Interfaces.Repositories;
using GTL.Application.ViewModels;
using MediatR;

namespace GTL.Application.UseCases.Queries.GetMaterialList
{
    public class GetMaterialListQueryHandler : IRequestHandler<GetMaterialListQuery, MaterialListViewModel>
    {
        private readonly IMaterialRepository _materialRepository;
        private readonly IMapper _mapper;

        public GetMaterialListQueryHandler(IMaterialRepository materialRepository, IMapper mapper)
        {
            _materialRepository = materialRepository;
            _mapper = mapper;
        }

        public async Task<MaterialListViewModel> Handle(GetMaterialListQuery request, CancellationToken cancellationToken)
        {
            var materials = await _materialRepository.GetAsync(cancellationToken);
            var viewModel = _mapper.Map<MaterialListViewModel>(materials);
            return viewModel;
        }
    }
}
