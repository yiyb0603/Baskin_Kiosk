using System;
using System.Windows.Threading;

namespace Baskin_Kiosk.ViewModel
{
    class SeatViewModel
    {
        DispatcherTimer useTimer = new DispatcherTimer();

        public bool used = false;
        public int seatNumber { get; set; }
        public int _time = 0;
        public int time
        {
            get
            {
                return _time;
            }
            set
            {
                _time = value;
            }
        }

        public SeatViewModel()
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
