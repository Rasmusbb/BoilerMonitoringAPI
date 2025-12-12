using BoilerMonitoringAPI.DTOs;
using Microsoft.AspNetCore.Mvc;
namespace BoilerMonitoringAPI.Interface
{
    public interface IHome : ICRUD<HomesDTO,HomesDTOID>
    {
        Task<UserDTOID> AddUserByEmail(HomeUser HomeUser);
        Task<ActionResult<bool>> RemoveUser(HomeUser HomeUser);
    }
}