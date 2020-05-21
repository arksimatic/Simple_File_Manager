using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TC.Model.Panel
{
    using Path;
    using Driver;
    using System.Diagnostics;

    class Panel
    {
        private Path currentPath;
        public Path CurrentPath { get => currentPath; set => currentPath = value; }

        private Paths pathsList;
        public Paths PathsList { get => pathsList; set => pathsList = value; }

        private Drivers driversList;
        public Drivers DriversList { get => driversList; set => driversList = value; }

        public Panel(string path)
        {
            CurrentPath = new Path(path);
            PathsList = new Paths();
            DriversList = new Drivers();
        }

        public List<Path> ReturnPathsList()
        {
            return PathsList.ReturnListOfPaths(CurrentPath, DriversList);
        }

        public string ReturnCurrentPath()
        {
            return CurrentPath.ThisPath;
        }

        public void SetCurrentPath(string path)
        {
            CurrentPath.ThisPath = path;
        }

        public string[] ReturnDriversList()
        {
            Debug.Write($"Drivers {DriversList}\n");
            return DriversList.DriversList;
        }

    }
}
