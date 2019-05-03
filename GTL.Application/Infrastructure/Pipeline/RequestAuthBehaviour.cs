using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using GTL.Application.Exceptions;
using GTL.Application.Helper;
using GTL.Application.Helper.CustomAttributes;
using GTL.Application.Interfaces.Authentication;
using GTL.Application.Interfaces.Repositories;
using GTL.Domain.Enums;
using MediatR;
using Microsoft.Extensions.Caching.Memory;

namespace GTL.Application.Infrastructure.Pipeline
{
    public class RequestAuthBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly ICurrentUser _currentUser;
        private readonly IMemoryCache _cache;
        private readonly IStaffRepository _staffRepo;

        public RequestAuthBehaviour(ICurrentUser currentUser, IStaffRepository staffRepo, IMemoryCache cache)
        {
            _currentUser = currentUser;
            _staffRepo = staffRepo;
            _cache = cache;
        }

        public Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var attr = request.GetType().GetCustomAttribute<Authorize>(false);

            if (attr != null)
            {
                var requiredMinimumRole = attr.Role;

                if (!_currentUser.IsAuthenticated())
                {
                    throw new AuthenticationException();
                }

                var ssn = _currentUser.GetSsn();
                if (string.IsNullOrEmpty(ssn))
                {
                    throw new AuthenticationException();
                }

                var foundInCache = _cache.TryGetValue(ssn, out Role staffRole);

                if (!foundInCache)
                {
                    var staff = _staffRepo.GetBySsn(ssn);
                    staffRole = staff.Role;
                    _cache.Set(ssn, staffRole, CacheHelper.CacheOptions());
                }

                if (staffRole < requiredMinimumRole)
                {
                    throw new AuthorizeException();
                }
            }

            return next();
        }
    }
}
