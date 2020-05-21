using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace TC.Model.Driver
{
    class Drivers
    {
        private string[] driversList;
        public string[] DriversList { get => driversList; set => driversList = value;  }

        public Drivers()
        {
            driversList = Directory.GetLogicalDrives();
        }
    }
}
