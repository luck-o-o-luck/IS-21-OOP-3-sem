using System.Collections.Generic;
using Isu.Models;
using Isu.Tools;
using IsuExtra.Tools;

namespace IsuExtra.Models
{
    public class StreamOGNP
    {
        private const int MaxCountStudent = 25;
        private List<MegafacultyStudent> _students;

        public StreamOGNP(Schedule schedule, OGNP ognp, char faculty, string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new IsuExtraException("String is null or empty");

            _students = new List<MegafacultyStudent>();
            OGNP = ognp ?? throw new IsuExtraException("OGNP is null");
            Faculty = faculty;
            Name = name;
            Schedule = schedule;
        }

        public OGNP OGNP { get; }
        public Schedule Schedule { get; }
        public char Faculty { get; }
        public string Name { get; }
        public IReadOnlyList<MegafacultyStudent> Students => _students;

        public void AddStudentToOGNPGroup(MegafacultyStudent student)
        {
            if (_students.Count == MaxCountStudent)
                throw new IsuExtraException("This OGNP already has max count of students");

            _students.Add(student);
        }

        public void RemoveStudentFromOGNP(MegafacultyStudent student)
        {
            if (student is null)
                throw new IsuException("This student hasn't this ognp");

            _students.Remove(student);
        }
    }
}