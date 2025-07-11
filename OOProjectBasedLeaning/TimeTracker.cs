using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOProjectBasedLeaning
{

    public interface TimeTracker
    {
        void ChangeClockInMode();

        void ChangeClockOutMode();

        /// <summary>
        /// 出勤の時間を記録する。
        /// </summary>
        /// <param name="employeeId">従業員のID</param>
        /// <exception cref="InvalidOperationException">従業員がすでに仕事中の場合</exception>"
        void PunchIn(int employeeId);

        /// <summary>
        /// 退勤の時間を記録する。
        /// </summary>
        /// <param name="employeeId">従業員のID</param>
        /// <exception cref="InvalidOperationException">従業員が仕事中でない場合</exception>""
        void PunchOut(int employeeId);

        bool IsClockInMode();
        bool IsClockOutMode();

        /// <summary>
        /// 仕事中かどうかを判定する。
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns>仕事中の場合 true</returns>
        bool IsAtWork(int employeeId);

    }

    public class TimeTrackerModel : NotifierModelEntity, TimeTracker
    {

        /// <summary>
        /// Represents the company associated with the current context.
        /// </summary>
        /// <remarks>This field is initialized to a default instance of <see cref="NullCompany"/> to
        /// ensure         a non-null value. Use this field to access or modify the company information as
        /// needed.</remarks>
        private Company company = NullCompany.Instance;
        /// <summary>
        /// <DateTime.Today, <Employee.Id, DateTime.Now>>
        /// </summary>
        private Dictionary<DateTime, List<Dictionary<int, DateTime>>> timestamp4PunchIn = new Dictionary<DateTime,List< Dictionary<int, DateTime>>>();
        /// <summary>
        /// <DateTime.Today, <Employee.Id, DateTime.Now>>
        /// </summary>
        private Dictionary<DateTime, List<Dictionary<int, DateTime>>> timestamp4PunchOut = new Dictionary<DateTime, List<Dictionary<int, DateTime>>>();
        /// <summary>
        /// Represents the current operational mode of the system.
        /// </summary>
        /// <remarks>The mode determines the behavior of the system, such as whether it operates in "Punch
        /// In" mode or other modes.</remarks>
        //private Mode mode = Mode.PunchIn;
        private TimeRecordMode mode = TimeRecordMode.ClockIn;

        private enum Mode
        {
            PunchIn, // default
            PunchOut
        };

        public TimeTrackerModel(Company company)
        {

            this.company = company.AddTimeTracker(this);

        }

        public void ChangeClockInMode()
        {

            mode = TimeRecordMode.ClockIn;

            // Notify observers that the mode has changed
            Notify();

        }

        public void ChangeClockOutMode()
        {

            mode = TimeRecordMode.ClockOut;

            // Notify observers that the mode has changed
            Notify();

        }

        public void PunchIn(int employeeId)
        {

            if (IsAtWork(employeeId))
            {

                // TODO
                throw new InvalidOperationException("Employee is already at work.");

            }

            DateTime today = DateTime.Today;

            if (!timestamp4PunchIn.ContainsKey(today))
            {

                // 今日の出勤打刻がない場合は、新規に作成する
                timestamp4PunchIn.Add(today, new List<Dictionary<int, DateTime>>());

            }

            if (AcquirePunchedInTimestamp(today, employeeId) is NullTimestamp)
            {

                // 今日の出勤打刻に従業員の打刻を追加する
                timestamp4PunchIn[today].Add(CreateTimestamp(employeeId));

            }

            // Notify observers that the mode has changed
            Notify();

        }

        public void PunchOut(int employeeId)
        {
            if (!IsAtWork(employeeId))
            {

                // TODO
                throw new InvalidOperationException("Employee is not at work.");

            }

            DateTime today = DateTime.Today;

            if (!timestamp4PunchOut.ContainsKey(today))
            {

                // 今日の退勤打刻がない場合は、新規に作成する
                timestamp4PunchOut.Add(today, new List<Dictionary<int, DateTime>>());

            }

            if (AcquirePunchedOutTimestamp(today, employeeId) is NullTimestamp)
            {

                // 今日の退勤打刻に従業員の打刻を追加する
                timestamp4PunchOut[today].Add(CreateTimestamp(employeeId));

            }

            // Notify observers that the mode has changed
            Notify();
        }

        /// <summary>
        /// 打刻用の時間を生成します。
        /// </summary>
        /// <param name="employeeId">従業員のID</param>
        /// <returns>生成された Dictionary<int, DateTime> のオブジェクト</returns>
        private Dictionary<int, DateTime> CreateTimestamp(int employeeId)
        {

            Dictionary<int, DateTime> timestamp = new Dictionary<int, DateTime>();
            timestamp.Add(employeeId, DateTime.Now);

            return timestamp;

        }

        public bool IsClockInMode()
        {

            return mode is TimeRecordMode.ClockIn;

        }
        public bool IsClockOutMode()
        {

            return mode is TimeRecordMode.ClockOut;

        }

        public bool IsAtWork(int employeeId)
        {

            DateTime today = DateTime.Today;

            return AcquirePunchedInTimestamp(today, employeeId) is not NullTimestamp
                && AcquirePunchedOutTimestamp(today, employeeId) is NullTimestamp;

        }
        private Dictionary<int, DateTime> AcquirePunchedInTimestamp(DateTime today, int employeeId)
        {

            if (timestamp4PunchIn.ContainsKey(today))
            {

                return AcquirePunchedTimestamp(timestamp4PunchIn[today], employeeId);

            }

            // 従業員の打刻がない場合は、NullTimestamp.Instance の打刻を返す
            return NullTimestamp.Instance;

        }

        private Dictionary<int, DateTime> AcquirePunchedOutTimestamp(DateTime today, int employeeId)
        {

            if (timestamp4PunchOut.ContainsKey(today))
            {

                return AcquirePunchedTimestamp(timestamp4PunchOut[today], employeeId);

            }

            // 従業員の打刻がない場合は、NullTimestamp.Instance の打刻を返す
            return NullTimestamp.Instance;

        }

        private Dictionary<int, DateTime> AcquirePunchedTimestamp(List<Dictionary<int, DateTime>> list, int employeeId)
        {

            foreach (Dictionary<int, DateTime> timestamp in list)
            {

                if (timestamp.ContainsKey(employeeId))
                {

                    return timestamp;

                }

            }

            // 従業員の打刻がない場合は、NullTimestamp.Instance の打刻を返す
            return NullTimestamp.Instance;

        }
        private class NullTimestamp : Dictionary<int, DateTime>, NullObject
        {

            private static Dictionary<int, DateTime> instance = new NullTimestamp();

            private NullTimestamp() : base(1)
            {

                Add(NullEmployee.Instance.Id, DateTime.MinValue);

            }

            public static Dictionary<int, DateTime> Instance { get { return instance; } }

        }

    }

    public class NullTimeTracker : TimeTracker, NullObject
    {

        private static NullTimeTracker instance = new NullTimeTracker();

        private NullTimeTracker()
        {

        }

        public static TimeTracker Instance { get { return instance; } }

        public void ChangeClockInMode()
        {

        }

        public void ChangeClockOutMode()
        {

        }

        public void PunchIn(int employeeId)
        {

        }

        public void PunchOut(int employeeId)
        {

        }

        public bool IsClockInMode()
        {

            return false;

        }
        public bool IsClockOutMode()
        {

            return false;

        }
        public bool IsAtWork(int employeeId)
        {

            return false;

        }



    }

}
