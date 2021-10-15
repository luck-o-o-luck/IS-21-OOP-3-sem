using System.Collections.Generic;
using Isu.Models;
using IsuExtra.Tools;

namespace IsuExtra.Models
{
    public class MegafacultyGroup : Group
    {
        private List<MegafacultyStudent> _students;

        public MegafacultyGroup(string name)
            : base(name)
        {
            Schedule = null;
            _students = new List<MegafacultyStudent>();
        }

        public Schedule Schedule { get; private set; }
        public IReadOnlyList<MegafacultyStudent> MegafacultyStudents => _students;

        public void AddStudentToGroup(MegafacultyStudent student)
        {
            _students.Add(student);
        }

        public Schedule AddSchedule(Schedule newSchedule)
        {
            Schedule = newSchedule;

            return Schedule;
        }
    }
}