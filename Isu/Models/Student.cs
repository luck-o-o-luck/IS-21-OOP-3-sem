using System.Collections.Generic;
using System.Runtime;
using Isu.Tools;

namespace Isu.Models
{
    public class Student
    {
        public Student(string fullName, Group<Student> groupIsu, int id)
        {
            if (string.IsNullOrWhiteSpace(fullName))
                throw new IsuException("String is null or empty");

            Name = fullName;
            GroupIsu = groupIsu;
            Id = id;
        }

        public string Name { get; }
        public Group<Student> GroupIsu { get; set; }
        public int Id { get; }

        public void ChangeGroup(Group<Student> newGroup)
        {
            this.GroupIsu = newGroup;
        }
    }
}