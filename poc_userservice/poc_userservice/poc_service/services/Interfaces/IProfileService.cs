using IdentityServer4.Models;
using System.Threading.Tasks;

namespace poc_service.services.Interfaces
{
    public interface IProfileService
    {
        Task GetProfileDataAsync(ProfileDataRequestContext context);
    }
}
