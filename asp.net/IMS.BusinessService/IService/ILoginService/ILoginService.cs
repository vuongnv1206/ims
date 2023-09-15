
using IMS.Contract.Systems.Tokens;

namespace IMS.BusinessService.IService.ILoginService;

public interface ILoginService
{
    Token GetFromToken(string token);
}
