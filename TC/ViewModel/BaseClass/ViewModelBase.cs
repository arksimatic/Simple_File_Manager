using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace TC.ViewModel.BaseClass
{
    class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void onPropertyChanged(params string[] namesOfProperties)
        {
            if (PropertyChanged != null)
            {
                foreach (var prop in namesOfProperties)
                {
                    Debug.Write($"Updated element: {prop}\n");
                    PropertyChanged(this, new PropertyChangedEventArgs(prop));
                }
            }
        }
    }
}
