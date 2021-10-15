using System;
using System.Collections.Generic;
using System.Linq;
using Isu.Models;
using Isu.Services;
using Isu.Tools;
using IsuExtra.Models;
using IsuExtra.Tools;

namespace IsuExtra.Services
{
    public class IsuExtraService : IIsuExtraService
    {
        private List<OGNP> _ognps;
        private List<MegafacultyStudent> _students;
        private List<MegafacultyGroup> _groups;
        private int _studentId = 100000;

        public IsuExtraService()
        {
            _ognps = new List<OGNP>();
            _groups = new List<MegafacultyGroup>();
            _students = new List<MegafacultyStudent>();
        }

        public OGNP FindOGNP(string name) => _ognps.SingleOrDefault(ognp => ognp.NameOGNP == name);
        public MegafacultyStudent FindStudent(string name) => _students.SingleOrDefault(student => student.Name == name);
        public MegafacultyGroup FindGroup(string name) => _groups.SingleOrDefault(group => group.FullNameGroup == name);

        public bool StudentExistsInOGNP(string name, StreamOGNP ognp) =>
            ognp.Students.Any(student => student.Name == name);
        public bool OGNPExists(string name) => _ognps.Any(ognp => string.Equals(ognp.NameOGNP, name, StringComparison.CurrentCultureIgnoreCase));
        public bool StudentExists(string name) => _students.Any(student => string.Equals(student.Name, name, StringComparison.CurrentCultureIgnoreCase));
        public bool GroupExists(string name) => _groups.Any(group => string.Equals(@group.FullNameGroup, name, StringComparison.CurrentCultureIgnoreCase));

        public OGNP AddOGNP(string name, char faculty)
        {
            if (!OGNPExists(name))
            {
                var ognp = new OGNP(name, faculty);
                _ognps.Add(ognp);
                return ognp;
            }
            else
            {
                throw new IsuExtraException("This OGNP already exists");
            }
        }

        public MegafacultyStudent AddStudent(MegafacultyGroup group, string name)
        {
            if (StudentExists(name))
                throw new IsuException("The student already exists");

            if (GroupExists(group.FullNameGroup))
                group = _groups.Single(selectedGroup => selectedGroup.FullNameGroup == group.FullNameGroup);

            if (group.Students.Count == group.MaxCountStudents)
                throw new IsuException("The student can't be created. Check that the group is correct");

            var student = new MegafacultyStudent(name, group, _studentId);
            _studentId++;

            if (!GroupExists(group.FullNameGroup))
                _groups.Add(group);

            group.AddStudentToGroup(student);
            _students.Add(student);

            return student;
        }

        public MegafacultyGroup AddGroup(string name)
        {
            if (GroupExists(name))
                throw new IsuException("The group already exists");

            var group = new MegafacultyGroup(name);
            _groups.Add(group);

            return group;
        }

        public StreamOGNP AddStreamOGNP(Schedule schedule, char faculty, string name)
        {
            var selectedOGNP = FindOGNP(name);
            var streamOGNP = new StreamOGNP(schedule, selectedOGNP, faculty, name);

            selectedOGNP.AddStreamOfOGNP(streamOGNP);

            return streamOGNP;
        }

        public IReadOnlyList<StreamOGNP> GetStreamsOGNP(OGNP ognp)
        {
            if (ognp is null)
                throw new IsuExtraException("OGNP is null");

            if (ognp.Streams is null)
                throw new IsuExtraException("Streams don't exist");

            return ognp.Streams;
        }

        public MegafacultyStudent AddStudentToStreamOGNP(MegafacultyStudent student, StreamOGNP streamOgnp)
        {
            if (student is null)
                throw new IsuExtraException("Student is null or doesn't exist");
            if (streamOgnp is null)
                throw new IsuExtraException("Stream ognp is null or doesn't exist");
            if (StudentExistsInOGNP(student.Name, streamOgnp))
                throw new IsuExtraException("This student already has this OGNP");

            student.AddOGNPForStudent(streamOgnp);
            streamOgnp.AddStudentToOGNPGroup(student);

            return student;
        }

        public MegafacultyStudent CancelStudentsOGNP(MegafacultyStudent student, OGNP ognp)
        {
            if (ognp is null)
                throw new IsuExtraException("OGNP doesn't exist");
            if (student is null)
                throw new IsuExtraException("Student doesn't exist");

            StreamOGNP streamOGNP = student.CancelOGNP(ognp);
            streamOGNP.RemoveStudentFromOGNP(student);

            return student;
        }

        public IReadOnlyList<MegafacultyStudent> GetStudentsFromOGNP(StreamOGNP ognp)
        {
            if (ognp is null)
                throw new IsuExtraException("OGNP doesn't exist");

            return ognp.Students;
        }

        public IReadOnlyList<MegafacultyStudent> GetUnsubscribedStudents(MegafacultyGroup group)
        {
            if (group is null)
                throw new IsuExtraException("Group doesn't exist");

            IReadOnlyList<MegafacultyStudent> selectedStudents = group.MegafacultyStudents.Where(student => student.CountOGNP < 2).ToList();

            return selectedStudents;
        }

        public Schedule AddScheduleForGroup(Schedule newShedule, MegafacultyGroup group)
        {
            if (newShedule is null)
                throw new IsuExtraException("Schedule is null");

            var schedule = group.AddSchedule(newShedule);

            return schedule;
        }
    }
}