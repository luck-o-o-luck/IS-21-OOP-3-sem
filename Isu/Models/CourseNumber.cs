using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Isu.Tools;

namespace Isu.Models
{
    public class CourseNumber
    {
        private List<Group<Student>> _groups = new List<Group<Student>>();

        public CourseNumber(int course, Group<Student> groupIsu)
        {
            if (course < 1 || course > 4)
            {
                throw new IsuException("The course can't be created");
            }

            Course = course;
            _groups.Add(groupIsu);
        }

        public int Course { get; }
        public IReadOnlyList<Group<Student>> GroupsFromCourse => _groups;

        public void AddGroupToCourse(Group<Student> studentsGroup)
        {
            if (_groups.All(group => @group.FullNameGroup != studentsGroup.FullNameGroup))
                _groups.Add(studentsGroup);
        }
    }
}