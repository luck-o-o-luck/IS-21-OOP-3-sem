using System;
using System.Collections.Generic;
using Isu.Tools;

namespace Isu.Models
{
    public class Group<TStudent>
        where TStudent : class
    {
        private const int LengthNameGroup = 5;
        private const int MaxCountStudent = 20;

        private List<TStudent> _students;
        public Group(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new IsuException("String is null or empty");
            if (name.Length != LengthNameGroup)
                throw new IsuException("The group name is incorrect. The group can't be created");

            FullNameGroup = name;
            InformationAboutGroup = new GroupInformation(name);
            _students = new List<TStudent>();
        }

        public string FullNameGroup { get; }
        public GroupInformation InformationAboutGroup { get; }
        public IReadOnlyList<TStudent> Students => _students;
        public int MaxCountStudents => MaxCountStudent;
        public void AddStudentsToGroup(TStudent student)
        {
            _students.Add(student);
        }
    }
}