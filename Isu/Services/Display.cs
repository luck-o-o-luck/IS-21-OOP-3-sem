using System;
using Isu.Models;

namespace Isu.Services
{
    public class Display
    {
        public void PrintStudent(Student student)
        {
            Console.WriteLine($"   id {student.Id} name {student.Name} group {student.GroupIsu.FullNameGroup} " +
                              $"course {student.GroupIsu.InformationAboutGroup.CourseNumber} " +
                              $"group number {student.GroupIsu.InformationAboutGroup.GroupNumber}");
        }

        public void PrintGroup(Group group)
        {
            Console.WriteLine($"  Group number {group.InformationAboutGroup.GroupNumber}");
            foreach (Student student in group.Students)
            {
              PrintStudent(student);
            }
        }

        public void PrintCourse(CourseNumber course)
        {
            Console.WriteLine($"Course number {course.Course}");
            foreach (Group group in course.GroupsFromCourse)
            {
              PrintGroup(group);
            }
        }
    }
}
