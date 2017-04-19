using System.Collections.Generic;
using System.Linq;
using SignSystem.API.Entities;

namespace SignSystem.API.Services.Signs
{
    public class SignRepository : ISignRepository
    {
        private SignSystemInfoContext _context;
        public SignRepository(SignSystemInfoContext context)
        {
            _context = context;
        }

        public bool SignExists(int signId)
        {
            return _context.Sign.Any(c => c.Id == signId);
        }

        public IEnumerable<Sign> GetSigns()
        {
            return _context.Sign.OrderBy(c => c.Model).ToList();
        }

        public Sign GetSign(int signid)
        {
            return _context.Sign.FirstOrDefault(c => c.Id == signid);
        }


        public bool Save()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}
