using System;
using System.Collections.Generic;
using System.Linq;
using Isu.Models;
using Isu.Tools;

namespace Isu.Services
{
    public class IsuDataStore : IIsuService
    {
        private List<Student> _students;
        private List<Group> _groups;
        private List<CourseNumber> _courses;
        private Display _display;
        private int _studentId = 100000;

        public IsuDataStore()
        {
            _students = new List<Student>();
            _groups = new List<Group>();
            _courses = new List<CourseNumber>();
            _display = new Display();
        }

        public bool StudentExists(string name) => _students.Any(student => student.Name.ToLower() == name.ToLower());
        public bool StudentExistsById(int id) => _students.Any(student => student.Id == id);
        public bool GroupExists(string name) => _groups.Any(group => group.FullNameGroup.ToLower() == name.ToLower());
        public bool CourseExists(int courseNumber) => _courses.Any(course => course.GetCourseNumber() == courseNumber);

        public Group AddGroup(string name)
        {
            if (GroupExists(name))
                throw new IsuException("The group already exists");

            var group = new Group(name);
            _groups.Add(group);
            int courseNumber = Convert.ToInt16(name.Substring(2, 1));

            if (!CourseExists(courseNumber))
            {
                _courses.Add(new CourseNumber(courseNumber, group));
            }
            else
            {
                CourseNumber selectedCourse = _courses.Single(course =>
                    course.GetCourseNumber() == group.InformationAboutGroup.CourseNumber);
                selectedCourse.AddGroupToCourse(group);
            }

            return group;
        }

        public Student AddStudent(Group group, string name)
        {
            if (StudentExists(name))
                throw new IsuException("The student already exists");

            if (GroupExists(group.FullNameGroup))
                group = _groups.Single(selectedGroup => selectedGroup.FullNameGroup == group.FullNameGroup);

            if (group.GetStudentsFromGroup().Count == 20)
                throw new IsuException("The student can't be created. Check that the group is correct");

            var student = new Student(name, group, _studentId);
            _studentId++;

            if (!GroupExists(group.FullNameGroup))
                _groups.Add(group);

            if (!CourseExists(group.InformationAboutGroup.CourseNumber))
            {
                _courses.Add(new CourseNumber(group.InformationAboutGroup.CourseNumber, group));
            }
            else
            {
                CourseNumber selectedCourse = _courses.Single(course =>
                    course.GetCourseNumber() == group.InformationAboutGroup.CourseNumber);
                selectedCourse.AddGroupToCourse(group);
            }

            group.AddStudentsToGroup(student);
            _students.Add(student);

            return student;
        }

        public Student GetStudent(int id)
        {
            if (!StudentExistsById(id))
                throw new IsuException("The student doesn't exists with this id");

            Student selectedStudent = _students.Single(student => student.Id == id);

            return selectedStudent;
        }

        public Student FindStudent(string name)
        {
            if (!StudentExists(name))
                throw new IsuException("The student doesn't exists with this name");

            Student selectedStudent = _students.Single(student => student.Name.ToLower() == name.ToLower());

            return selectedStudent;
        }

        public List<Student> FindStudents(string groupName)
        {
            if (!GroupExists(groupName))
                throw new IsuException("The group doesn't exists with this name");

            Group selectedGroup = _groups.Single(group => group.FullNameGroup.ToLower() == groupName.ToLower());

            if (selectedGroup.GetStudentsFromGroup().Count > 0)
                return selectedGroup.GetStudentsFromGroup();
            else
                throw new IsuException("Students from group doesn't exists");
        }

        public List<Student> FindStudents(CourseNumber courseNumber)
        {
            if (!CourseExists(courseNumber.GetCourseNumber()))
                throw new IsuException("The course doesn't exists");

            var selectedStudents = new List<Student>();
            List<Group> selectedGroups = _courses
                .Single(course => course.GetCourseNumber() == courseNumber.GetCourseNumber())
                .GetGroupsFromCourse();

            foreach (Group group in selectedGroups)
                selectedStudents.AddRange(group.GetStudentsFromGroup());

            if (selectedStudents.Count > 0)
                return selectedStudents;
            else
                throw new IsuException("The students doesn't exists");
        }

        public Group FindGroup(string groupName)
        {
            if (!GroupExists(groupName))
                throw new IsuException("The group doesn't exists with this name");

            Group selectedGroup = _groups.Single(group => group.FullNameGroup.ToLower() == groupName.ToLower());

            return selectedGroup;
        }

        public List<Group> FindGroups(CourseNumber courseNumber)
        {
            List<Group> selectedGroups = _courses
                .Single(course => course.GetCourseNumber() == courseNumber.GetCourseNumber())
                .GetGroupsFromCourse();

            return selectedGroups;
        }

        public void ChangeStudentGroup(Student student, Group newGroup)
        {
            if (!StudentExists(student.Name))
                throw new IsuException("The student can't be found");

            if (student.GroupIsu.InformationAboutGroup.CourseNumber != newGroup.InformationAboutGroup.CourseNumber)
                throw new IsuException("The courses don't match");

            student.ChangeGroup(newGroup);
        }

        public void Print()
        {
            foreach (CourseNumber course in _courses)
                _display.PrintCourse(course);
        }
    }
}