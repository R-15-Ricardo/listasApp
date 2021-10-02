using System;
using System.Collections.Generic;
using System.Text;

namespace listas.Interface
{
    public interface IFileService
    {
        void WriteFile(string filename, string textMessage);
        string ReadFile(string filename);
        void InitializeFile(string filename);
    }
}
