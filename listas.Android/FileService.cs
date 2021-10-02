using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using listas.Droid;
using listas.Interface;

[assembly: Xamarin.Forms.Dependency(typeof(FileService))]
namespace listas.Droid
{
    public class FileService : IFileService
    {
        public string GetRootPath()
        {
            return Application.Context.GetExternalFilesDir(null).ToString();
        }

        public void WriteFile(string filename, string textMessage)
        {
            string destination = Path.Combine(GetRootPath(), filename);
            File.WriteAllText(destination, textMessage);
        }

        public string ReadFile(string filename)
        {
            string src = Path.Combine(GetRootPath(), filename);

            return File.ReadAllText(src);
        }

        public void InitializeFile(string filename)
        {
            string src = Path.Combine(GetRootPath(), filename);
            if (!File.Exists(src))
            {
                File.Create(src);
            }
        }
    }
}