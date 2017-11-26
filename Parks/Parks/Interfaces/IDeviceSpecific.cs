using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parks.Interfaces
{
    public interface IDeviceSpecific
    {
        Task<string> LerTextoArquivo(string filename);
    }
}
