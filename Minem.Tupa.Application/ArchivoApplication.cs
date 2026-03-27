using AutoMapper;
using Microsoft.Extensions.Configuration;
using Minem.Tupa.Dto.Autenticacion;
using Minem.Tupa.Entity;
using Minem.Tupa.IApplication;
using Minem.Tupa.Infraestructure;
using Minem.Tupa.IRepository;
using Minem.Tupa.Utils;
using System.Security.Claims;

namespace Minem.Tupa.Application
{
    public class ArchivoApplication
    {
        private readonly string _filePath;

        public ArchivoApplication(string filePath)
        {
            _filePath = filePath;
        }

        public async Task<byte[]> GetFileAsync(string fileName)
        {
            var fullPath = Path.Combine(_filePath, fileName);

            if (!File.Exists(fullPath))
            {
                return null;
            }

            return await File.ReadAllBytesAsync(fullPath);
        }
    }
}
