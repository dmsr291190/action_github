using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minem.Tupa.IApplication
{
    public interface IEmailApplication
    {
        Task<bool> SendMail(string correoremitente, string correodestino, string asunto, string bodycorreo, bool EnableHTML, bool EnableSSL, string rutaarhivoadjunto = null);
    }
}
