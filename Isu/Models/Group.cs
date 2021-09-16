using System;
using System.Collections.Generic;
using Isu.Tools;

namespace Isu.Models
{
    public class Group
    {
        private const int LengthNameGroup = 5;
        private const int MaxCountStudent = 20;

        private List<Student> _students;
        public Group(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new IsuException("String is null or empty");
            if (name.Length != LengthNameGroup)
                throw new IsuException("The group name is incorrect. The group can't be created");

            FullNameGroup = name;
            InformationAboutGroup = new GroupInformation(name);
            _students = new List<Student>();
        }

        public string FullNameGroup { get; }
        public GroupInformation InformationAboutGroup { get; }
        public void AddStudentsToGroup(Student student)
        {
            _students.Add(student);
        }

        public IReadOnlyList<Student> StudentsFromGroup() => this._students;
        public int MaxCountStudents() => Group.MaxCountStudent;
    }
}