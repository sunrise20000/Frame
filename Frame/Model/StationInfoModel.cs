using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Frame.Model
{
    public class StationInfoModel : INotifyPropertyChanged
    {
        string stationName = "";
        ObservableCollection<string> infoCollect = new ObservableCollection<string>();
    
        public event PropertyChangedEventHandler PropertyChanged;
        private void RaisePropertyChanged([CallerMemberName]string PropertyName = "")
        {
            PropertyChanged?.Invoke(this,new PropertyChangedEventArgs(PropertyName));
        }

        public StationInfoModel()
        {
            
        }

        public string StationName {
            get { return stationName; }
            set {
                if (stationName != value)
                {
                    stationName = value;
                    RaisePropertyChanged();
                }
            }


        }
        public ObservableCollection<string> InfoCollect {
            get { return infoCollect; }
            set {
                if (infoCollect != value)
                {
                    infoCollect = value;
                    RaisePropertyChanged();
                }
            }
        }
    }
}
