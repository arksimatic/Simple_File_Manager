using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TC.ViewModel
{
    using Model;
    using Model.Path;
    using BaseClass;
    using System.Collections.ObjectModel;
    using System.Windows.Input;
    using System.Diagnostics;

    class ViewModelTC : ViewModelBase
    {
        private TC TC = new TC(System.IO.Directory.GetCurrentDirectory().ToString());

        private string currentPathLeft;
        public string CurrentPathLeft
        {
            get { return currentPathLeft; }
            set { currentPathLeft = value; onPropertyChanged(nameof(CurrentPathLeft)); }
        }

        private string currentPathRight;
        public string CurrentPathRight
        {
            get { return currentPathRight; }
            set { currentPathRight = value; onPropertyChanged(nameof(CurrentPathRight)); }
        }

        private string selectedDiscLeft;
        public string SelectedDiscLeft 
        { 
            get { return selectedDiscLeft; } 
            set { selectedDiscLeft = value; onPropertyChanged(nameof(SelectedDiscLeft)); } 
        }

        private string selectedDiscRight;
        public string SelectedDiscRight 
        { 
            get { return selectedDiscRight; } 
            set { selectedDiscRight = value; onPropertyChanged(nameof(SelectedDiscRight)); } 
        }

        private Path nextPathLeft;
        public Path NextPathLeft
        {
            get { return nextPathLeft; }
            set { nextPathLeft = value; onPropertyChanged(nameof(NextPathLeft)); }
        }

        private Path nextPathRight;
        public Path NextPathRight
        {
            get { return nextPathRight; }
            set { nextPathRight = value; onPropertyChanged(nameof(NextPathRight)); }
        }

        private Path nextPath;
        public Path NextPath
        {
            get { return nextPath; }
            set { nextPath = value; onPropertyChanged(nameof(NextPath)); }
        }

        private ObservableCollection<Path> pathsListLeft;
        public ObservableCollection<Path> PathsListLeft
        { 
            get => pathsListLeft; 
            set { pathsListLeft = value; onPropertyChanged(nameof(PathsListLeft)); } 
        }

        private ObservableCollection<Path> pathsListRight;
        public ObservableCollection<Path> PathsListRight 
        { 
            get => pathsListRight; 
            set { pathsListRight = value; onPropertyChanged(nameof(PathsListRight)); } 
        }


        private string[] driversListLeft;
        public string[] DriversListLeft 
        {
            get { driversListLeft = TC.LPanel.ReturnDriversList(); return driversListLeft; }
            set { driversListLeft = value; onPropertyChanged(nameof(DriversListLeft)); }
        }

        private string[] driversListRight;
        public string[] DriversListRight
        {
            get { driversListRight = TC.RPanel.ReturnDriversList(); return driversListRight; }
            set { driversListRight = value; onPropertyChanged(nameof(DriversListRight)); }
        }

        private ObservableCollection<Path> ToObservable(List<Path> pathList)
        {
            ObservableCollection<Path> pathObservableCollection = new ObservableCollection<Path>();
            for(int i=0; i<pathList.Count; i++)
            {
                pathObservableCollection.Add(pathList[i]);
            }
            return pathObservableCollection;
        }

        private void UpdateLeftPanel(object sender)
        {
            if(NextPathLeft != null)
            {
                //TC.LPanel.CurrentPath.ThisPath = NextPathLeft.ThisPath; the same result, to delete
                TC.LPanel.CurrentPath = NextPathLeft;
                if (NextPathLeft.Representation != null)
                {
                    PathsListLeft = ToObservable(TC.LPanel.ReturnPathsList());
                    CurrentPathLeft = TC.LPanel.ReturnCurrentPath();
                }
                else
                {
                    CurrentPathLeft = TC.LPanel.ReturnCurrentPath();
                    NextPathRight = null;
                }
            }
        }
        private void UpdateRightPanel(object sender)
        {
            if (NextPathRight != null)
            {
                TC.RPanel.CurrentPath = NextPathRight;
                if (NextPathRight.Representation != null)
                {
                    PathsListRight = ToObservable(TC.RPanel.ReturnPathsList());
                    CurrentPathRight = TC.RPanel.ReturnCurrentPath();
                }
                else
                {
                    CurrentPathRight = TC.RPanel.ReturnCurrentPath();
                    NextPathLeft = null;
                }
            }
        }

        private void Copy(object sender)
        {
            if(NextPathLeft == null && NextPathRight != null)
            {
                string source = NextPathRight.ThisPath;
                string destination = TC.LPanel.ReturnCurrentPath();
                TC.Copy.CopyFile(source, destination);
                PathsListLeft = ToObservable(TC.LPanel.ReturnPathsList());
            }
            else if(NextPathLeft != null && NextPathRight == null)
            {
                string source = NextPathLeft.ThisPath;
                string destination = TC.RPanel.ReturnCurrentPath();
                TC.Copy.CopyFile(source, destination);
                PathsListRight = ToObservable(TC.RPanel.ReturnPathsList());
            }
            else
            {
                //brak zaznaczenia
            }
        }

        private void LoadDiscLeft(object sender)
        {
            if(SelectedDiscLeft != null)
            {
                TC.LPanel.SetCurrentPath(SelectedDiscLeft);
                PathsListLeft = ToObservable(TC.LPanel.ReturnPathsList());
                CurrentPathLeft = TC.LPanel.ReturnCurrentPath();
            }
        }
        private void LoadDiscRight(object sender)
        {
            if (SelectedDiscRight != null)
            {
                TC.RPanel.SetCurrentPath(SelectedDiscRight);
                PathsListRight = ToObservable(TC.RPanel.ReturnPathsList());
                CurrentPathRight = TC.RPanel.ReturnCurrentPath();
            }
        }

        private void UpdateLeftDiscs(object sender)
        {
            Debug.Write("selectedDiscLeft\n");
            DriversListLeft = TC.LPanel.ReturnDriversList();
        }

        private void UpdateRightDiscs(object sender)
        {
            Debug.Write("selectedDiscRight\n");
            DriversListRight = TC.RPanel.ReturnDriversList();
        }

        #region Commands

        private ICommand copy = null;
        public ICommand CopyCommand
        {
            get
            {
                if (copy == null)
                {
                    copy = new RelayCommand(Copy, arg => true);
                }
                return copy;
            }
        }


        private ICommand loadLeft = null;
        public ICommand LoadLeftCommand
        {
            get
            {
                if(loadLeft == null)
                {
                    loadLeft = new RelayCommand(UpdateLeftPanel, arg => true);
                }
                return loadLeft;
            }
        }

        private ICommand loadRight = null;
        public ICommand LoadRightCommand
        {
            get
            {
                if (loadRight == null)
                {
                    loadRight = new RelayCommand(UpdateRightPanel, arg => true);
                }
                return loadRight;
            }
        }


        private ICommand loadLeftDisc = null;
        public ICommand LoadLeftDiscCommand
        {
            get
            {
                if(loadLeftDisc == null)
                {
                    loadLeftDisc = new RelayCommand(LoadDiscLeft, arg => true);
                }
                return loadLeftDisc;
            }
        }

        private ICommand loadRightDisc = null;
        public ICommand LoadRightDiscCommand
        {
            get
            {
                if (loadRightDisc == null)
                {
                    loadRightDisc = new RelayCommand(LoadDiscRight, arg => true);
                }
                Debug.Write("loadRightDiscsCommand activated \n");
                return loadRightDisc;
            }
        }


        private ICommand updateLeftDiscs = null;
        public ICommand UpdateLeftDiscsCommand
        {
            get
            {
                if(updateLeftDiscs == null)
                {
                    updateLeftDiscs = new RelayCommand(UpdateLeftDiscs, arg => true);
                }
                return updateLeftDiscs;
            }
        }

        private ICommand updateRightDiscs = null;
        public ICommand UpdateRightDiscsCommand
        {
            get
            {
                if (updateRightDiscs == null)
                {
                    updateRightDiscs = new RelayCommand(UpdateRightDiscs, arg => true);
                    Debug.Write("initialized command\n");
                }
                Debug.Write("updateRightDiscsCommand activated \n");
                return updateRightDiscs;
            }
        }

        #endregion

        public ViewModelTC()
        {
            PathsListLeft = ToObservable(TC.LPanel.ReturnPathsList());
            PathsListRight = ToObservable(TC.RPanel.ReturnPathsList());
            CurrentPathLeft = TC.LPanel.ReturnCurrentPath();
            CurrentPathRight = TC.RPanel.ReturnCurrentPath();

            DriversListLeft = TC.LPanel.ReturnDriversList();
            DriversListRight = TC.RPanel.ReturnDriversList();

            
        }
    }
}
