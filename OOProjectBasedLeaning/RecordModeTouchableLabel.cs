using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOProjectBasedLeaning
{
    public class RecordModeTouchableLabel : TouchableLabel,Observer
    {
        private TimeTracker timeTracker = NullTimeTracker.Instance;
        private string clockInText = "出　勤";
        private string clockOutText = "退　勤";

        public RecordModeTouchableLabel(TimeTracker timeTracker)
        {

            if (timeTracker is NotifierModel)
            {

                (timeTracker as NotifierModel).AddObserver(this);

            }

            this.timeTracker = timeTracker;

            InitializeComponent();

        }

        private void InitializeComponent()
        {

            Font = new Font("Arial", 16, FontStyle.Bold);
            Dock = DockStyle.Fill;
            TextAlign = ContentAlignment.MiddleCenter;

            Update(this);

        }

        public string ClockInText { get { return clockInText; } set { clockInText = value; } }

        public string ClockOutText { get { return clockOutText; } set { clockOutText = value; } }

        public bool IsClockInMode()
        {

            return timeTracker.IsClockInMode();

        }

        public bool IsClockOutMode()
        {

            return timeTracker.IsClockOutMode();

        }

        protected override void OnTouch()
        {

            if (IsClockInMode())
            {

                ChangeClockOutMode();

            }
            else if (IsClockOutMode())
            {

                ChangeClockInMode();

            }

        }

        public void ChangeClockInMode()
        {

            timeTracker.ChangeClockInMode();

        }

        public void ChangeClockOutMode()
        {

          

        }

        public void Update(object sender)
        {

            if (IsClockInMode())
            {

                Text = clockInText;
                ForeColor = Color.White;
                BackColor = Color.SkyBlue;

            }
            else if (IsClockOutMode())
            {

                Text = clockOutText;
                ForeColor = Color.White;
                BackColor = Color.LightSalmon;

            }

        }

    }
}
