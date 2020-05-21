using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace TC.Model
{
    using Panel;
    using Path;
    using Copy;
    using Driver;
    class TC
    {
        public Panel.Panel LPanel;
        public Panel.Panel RPanel;
        public Copy.Copy Copy;
        public TC()
        {
            LPanel = new Panel.Panel(Directory.GetCurrentDirectory());
            RPanel = new Panel.Panel(Directory.GetCurrentDirectory());
            Copy = new Copy.Copy();
        }

        public TC(string path)
        {
            LPanel = new Panel.Panel(path);
            RPanel = new Panel.Panel(path);
            Copy = new Copy.Copy();
        }
    }
}
