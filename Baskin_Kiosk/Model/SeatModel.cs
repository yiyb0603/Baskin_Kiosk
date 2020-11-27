using System;
using System.ComponentModel;
using System.Windows.Threading;

namespace Baskin_Kiosk.ViewModel
{
    public class SeatModel : INotifyPropertyChanged
    {
        DispatcherTimer useTimer = new DispatcherTimer();

        public int _time = 0;
        public bool _isEmpty = true;
        public string _paidTime = "";

        public int seatNumber { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propetryName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propetryName));
        }

        public int time
        {
            get
            {
                return _time;
            }
            set
            {
                _time = value;
                OnPropertyChanged(nameof(time));
            }
        }

        public string paidTime
        {
            get
            {
                return _paidTime;
            }
            set
            {
                _paidTime = value;
                OnPropertyChanged(nameof(paidTime));
            } 
        }

        public bool isEmpty
        {
            get
            {
                return _isEmpty;
            }
            set
            {
                _isEmpty = value;
                OnPropertyChanged(nameof(isEmpty));
            }
        }

        public SeatModel()
        {
            useTimer.Interval = TimeSpan.FromSeconds(1);
            useTimer.Tick += UseTimer_Tick;
        }

        public void UseSeat()
        {
            isEmpty = false;
            useTimer.Start();
            paidTime = "결제시간: " + DateTime.Now.ToString("HH시 mm분 ss초");
        }

        private void UseTimer_Tick(object sender, EventArgs e)
        {
            time--;
            if (time <= 0)
            {
                useTimer.Stop();
                isEmpty = true;
                paidTime = "";
            }
        }
    }
}
