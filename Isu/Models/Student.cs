using System.Collections.Generic;
using System.Runtime;
using Isu.Tools;

namespace Isu.Models
{
    public class Student
    {
        public Student(string fullName, Group groupIsu, int id)
        {
            if (string.IsNullOrWhiteSpace(fullName))
                throw new IsuException("String is null or empty");

            Name = fullName;
            GroupIsu = groupIsu;
            Id = id;
        }

        public string Name { get; }
        public Group GroupIsu { get; set; }
        public int Id { get; }

        public void ChangeGroup(Group newGroup)
        {
            this.GroupIsu = newGroup;
        }
    }
}