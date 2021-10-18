using System.Collections.Generic;
using Isu.Models;

namespace Isu.Services
{
    public interface IIsuService
    {
        Group<Student> AddGroup(string name);
        Student AddStudent(Group<Student> group, string name);

        Student GetStudent(int id);
        Student FindStudent(string name);
        IReadOnlyList<Student> FindStudents(string groupName);
        IReadOnlyList<Student> FindStudents(CourseNumber courseNumber);

        Group<Student> FindGroup(string groupName);
        IReadOnlyList<Group<Student>> FindGroups(CourseNumber courseNumber);

        void ChangeStudentGroup(Student student, Group<Student> newGroup);
        void Print();
    }
}