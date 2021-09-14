using System;
using System.Collections.Generic;
using Isu.Tools;

namespace Isu.Models
{
    public class Group
    {
        private List<Student> _students;
        public Group(string name)
        {
            if (name.Length != LengthNameGroup)
                throw new IsuException("The group name is incorrect. The group can't be created");

            FullNameGroup = name;
            InformationAboutGroup = new GroupInformation(name);
            _students = new List<Student>();
        }

        public int LengthNameGroup { get; } = 5;
        public string FullNameGroup { get; }
        public GroupInformation InformationAboutGroup { get; }
        public int MaxCountStudent { get; } = 20;

        public void AddStudentsToGroup(Student student)
        {
            _students.Add(student);
        }

        public IReadOnlyList<Student> GetStudentsFromGroup() => this._students;
    }
}