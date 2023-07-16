using System.Security.Claims;

using Money.Web.Models;

namespace Money.Web.Services
{
    internal interface IEntityService<TViewModel>
    {
        Task<bool> Create(ClaimsPrincipal claims, TViewModel viewModel);
        Task<bool> Edit(ClaimsPrincipal claims, TViewModel viewModel);
        Task<List<TViewModel>> Get(ClaimsPrincipal claims);
        Task<TViewModel?> Get(ClaimsPrincipal claims, long id);
    }
}