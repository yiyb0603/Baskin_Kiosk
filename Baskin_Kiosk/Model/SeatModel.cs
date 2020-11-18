using System;
using System.ComponentModel;
using System.Windows.Threading;

namespace Baskin_Kiosk.ViewModel
{
    public class SeatModel : INotifyPropertyChanged
    {
        DispatcherTimer useTimer = new DispatcherTimer();

        public bool used = false;
        public int seatNumber { get; set; }
        public int _time = 0;

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

        public SeatModel()
        {
            useTimer.Interval = TimeSpan.FromSeconds(1);
            useTimer.Tick += UseTimer_Tick;
        }

        public void UseSeat()
        {
            used = true;
            useTimer.Start();
        }

        private void UseTimer_Tick(object sender, EventArgs e)
        {
            time--;
            if (time <= 0)
            {
                useTimer.Stop();
                used = false;
            }
        }
    }
}
