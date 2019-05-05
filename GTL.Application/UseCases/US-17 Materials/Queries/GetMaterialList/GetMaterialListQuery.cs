using GTL.Application.ViewModels;
using MediatR;

namespace GTL.Application.UseCases.Queries.GetMaterialList
{
    public class GetMaterialListQuery : IRequest<MaterialListViewModel>
    {
    }
}
