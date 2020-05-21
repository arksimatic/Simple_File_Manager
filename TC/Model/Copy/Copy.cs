using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace TC.Model.Copy
{
    class Copy
    {
        string name;
        string Name { get => name; set => name = value; }
        string destination;
        string Destination { get => destination; set => destination = value; }
        public void CopyFile(string source, string destination)
        {
            this.name = System.IO.Path.GetFileName(source);
            this.destination = System.IO.Path.Combine(destination, name);
            System.IO.File.Copy(source, this.destination, true);
        }
    }
}
