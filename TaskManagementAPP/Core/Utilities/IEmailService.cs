using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities
{
    public interface IEmailService
    {
        Task ConfirmationMailAsync(string link, string email);
        Task ForgetPasswordMailAsync(string link, string email);
    }
}
