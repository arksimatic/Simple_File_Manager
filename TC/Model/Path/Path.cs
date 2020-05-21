using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TC.Model.Path
{
    class Path
    {
        private string fileName;
        public string FileName { get { return fileName; } set { fileName = value; } }

        private string thisPath;
        public string ThisPath { get { return thisPath; } set {thisPath = value; } }

        private string representation;
        public string Representation { get { return representation; } set { representation = value; } }

        public bool showPath;
        public bool ShowPath { get { return showPath; } set { showPath = value; } }

        public override string ToString()
        {
            if (ShowPath == true)
                return "<D>" + this.ThisPath;
            else
                return "...";
        }

        public Path (string path, string fileName, bool showPath = true, string representation = null)
        {
            ThisPath = path;
            Representation = representation;
            ShowPath = showPath;
            FileName = fileName;
        }

        public Path (string path, bool showPath = true, string representation = null)
        {
            ThisPath = path;
            Representation = representation;
            ShowPath = showPath;
        }

    }
}
