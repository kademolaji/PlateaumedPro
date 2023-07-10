using PlateaumedPro.Contracts;
using PlateaumedPro.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlateaumedPro.Services
{
    public class BasicAuthService : IBasicAuthService
    {
        private readonly DataContext _context;
        public BasicAuthService(DataContext context)
        {
            _context = context;
        }
        public bool CheckValidUserKey(string reqkey)
        {
            var channelExist = _context.Users.FirstOrDefault(x => x.ApiKey == reqkey);
            if (channelExist != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
