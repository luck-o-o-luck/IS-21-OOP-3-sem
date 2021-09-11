using System;
using System.Collections.Generic;
using System.Linq;
using Isu.Tools;

namespace Isu.Models
{
    public class CourseNumber
    {
        private List<Group> _groups = new List<Group>();

        public CourseNumber(int course, Group groupIsu)
        {
            if (course < 1 || course > 4)
            {
                throw new IsuException("The course can't be created");
            }

            Course = course;
            _groups.Add(groupIsu);
        }

        private int Course { get; }

        public void AddGroupToCourse(Group studentsGroup)
        {
            if (!_groups.Any(group => group == studentsGroup))
                _groups.Add(studentsGroup);
        }

        public List<Group> GetGroupsFromCourse() => this._groups;

        public int GetCourseNumber() => this.Course;
    }
}