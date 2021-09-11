using System;
using System.Collections.Generic;

namespace Isu.Models
{
    public class Group
    {
        private List<Student> _students;
        public Group(string name)
        {
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

        public List<Student> GetStudentsFromGroup() => this._students;
    }
}