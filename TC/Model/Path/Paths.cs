using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace TC.Model.Path
{
    using Driver;
    class Paths
    {
        string currentPath;
        string CurrentPath { get => currentPath; set => currentPath = value; }

        string[] driversList;
        string[] DriversList { get => driversList; set => driversList = value; }

        List<Path> pathsList;
        Path previousPath;
        bool isRootDirectory;
        public List<Path> ReturnListOfPaths(Path acurrentPath, Drivers adriversList)
        {
            currentPath = acurrentPath.ThisPath;
            driversList = adriversList.DriversList;
            pathsList = new List<Path>();
            previousPath = new Path("default");
            isRootDirectory = false; 

            for (int i=0; i<driversList.Length; i++)
            {
                if(currentPath == driversList[i])
                {
                    previousPath = new Path(currentPath);
                    isRootDirectory = true; //probably?
                }
            }
            if (isRootDirectory == false)
            {
                previousPath = new Path(Directory.GetParent(currentPath).ToString(), false, "..."); 
            }

            pathsList.Add(previousPath);
            string[] directories;
            try
            {
                directories = Directory.GetDirectories(currentPath);
            }
            catch(Exception e)
            {
                directories = Directory.GetDirectories(previousPath.ThisPath);
            }
            
            for (int i=0; i<directories.Length; i++)
            {
                pathsList.Add(new Path(directories[i], true, ""));
            }

            string[] files;
            try
            {
                files = Directory.GetFiles(currentPath);
            }
            catch(Exception e)
            {
                files = Directory.GetFiles(previousPath.ThisPath);
            }
            for(int i=0; i<files.Length; i++)
            {
                pathsList.Add(new Path(files[i]));
            }

            return pathsList;
        }
    }
}
