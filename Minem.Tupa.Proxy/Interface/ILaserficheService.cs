using Minem.Tupa.Dto.Svc.Laserfiche;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minem.Tupa.Proxy.Interface
{
    public interface ILaserficheService
    {
        Task<DocumentoModel> DownloadDocument(int idDocumento);
        Task<int> UploadDocument(byte[] file, string documentName);
    }
}
