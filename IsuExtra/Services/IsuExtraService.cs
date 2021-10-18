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
        private List<Ognp> _ognps;
        private List<MegafacultyStudent> _students;
        private List<MegafacultyGroup> _groups;
        private int _studentId = 100000;

        public IsuExtraService()
        {
            _ognps = new List<Ognp>();
            _groups = new List<MegafacultyGroup>();
            _students = new List<MegafacultyStudent>();
        }

        public Ognp FindOGNP(string name) => _ognps.SingleOrDefault(ognp => ognp.NameOGNP == name);
        public MegafacultyStudent FindStudent(string name) => _students.SingleOrDefault(student => student.Name == name);
        public MegafacultyGroup FindGroup(string name) => _groups.SingleOrDefault(group => group.FullNameGroup == name);

        public bool StudentExistsInOGNP(string name, StreamOgnp ognp) =>
            ognp.Students.Any(student => student.Name == name);
        public bool OGNPExists(string name) => _ognps.Any(ognp => string.Equals(ognp.NameOGNP, name, StringComparison.CurrentCultureIgnoreCase));
        public bool StudentExists(string name) => _students.Any(student => string.Equals(student.Name, name, StringComparison.CurrentCultureIgnoreCase));
        public bool GroupExists(string name) => _groups.Any(group => string.Equals(@group.FullNameGroup, name, StringComparison.CurrentCultureIgnoreCase));

        public Ognp AddOgnp(string name, char faculty)
        {
            if (!OGNPExists(name))
            {
                var ognp = new Ognp(name, faculty);
                _ognps.Add(ognp);
                return ognp;
            }

            throw new IsuExtraException("This OGNP already exists");
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

            group.AddStudentsToGroup(student);
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

        public StreamOgnp AddStreamOgnp(Schedule schedule, char faculty, string name)
        {
            var selectedOGNP = FindOGNP(name);
            var streamOGNP = new StreamOgnp(schedule, selectedOGNP, faculty, name);

            selectedOGNP.AddStreamOfOGNP(streamOGNP);

            return streamOGNP;
        }

        public IReadOnlyList<StreamOgnp> GetStreamsOgnp(Ognp ognp)
        {
            if (ognp is null)
                throw new IsuExtraException("OGNP is null");

            if (ognp.Streams is null)
                throw new IsuExtraException("Streams don't exist");

            return ognp.Streams;
        }

        public MegafacultyStudent AddStudentToStreamOgnp(MegafacultyStudent student, StreamOgnp streamOgnp)
        {
            if (student is null)
                throw new IsuExtraException("Student is null or doesn't exist");
            if (streamOgnp is null)
                throw new IsuExtraException("Stream OGNP is null or doesn't exist");
            if (StudentExistsInOGNP(student.Name, streamOgnp))
                throw new IsuExtraException("This student already has this OGNP");

            student.AddOGNPForStudent(streamOgnp);
            streamOgnp.AddStudentToOgnpGroup(student);

            return student;
        }

        public MegafacultyStudent CancelStudentsOgnp(MegafacultyStudent student, Ognp ognp)
        {
            if (ognp is null)
                throw new IsuExtraException("OGNP doesn't exist");
            if (student is null)
                throw new IsuExtraException("Student doesn't exist");

            StreamOgnp streamOgnp = student.Ognp;
            student.CancelOGNP(ognp);
            streamOgnp.RemoveStudentFromOgnp(student);

            return student;
        }

        public IReadOnlyList<MegafacultyStudent> GetStudentsFromOgnp(StreamOgnp ognp)
        {
            if (ognp is null)
                throw new IsuExtraException("OGNP doesn't exist");

            return ognp.Students;
        }

        public IReadOnlyList<MegafacultyStudent> GetUnsubscribedStudents(MegafacultyGroup group)
        {
            if (group is null)
                throw new IsuExtraException("Group doesn't exist");

            IReadOnlyList<MegafacultyStudent> selectedStudents = group.Students.Where(student => student.Ognp is null).ToList();

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