using System.Collections.Generic;
using SignSystem.API.Entities;

namespace SignSystem.API.Services.Signs
{
    public interface ISignRepository
    {
        bool SignExists(int cityId);
        IEnumerable<Sign> GetSigns();
        Sign GetSign(int signId);
        bool Save();
    }
}
