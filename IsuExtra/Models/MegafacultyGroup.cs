using System.Collections.Generic;
using Isu.Models;
using IsuExtra.Tools;

namespace IsuExtra.Models
{
    public class MegafacultyGroup : Group<MegafacultyStudent>
    {
        public MegafacultyGroup(string name)
            : base(name)
        {
            Schedule = null;
        }

        public Schedule Schedule { get; private set; }

        public Schedule AddSchedule(Schedule newSchedule)
        {
            Schedule = newSchedule;

            return Schedule;
        }
    }
}