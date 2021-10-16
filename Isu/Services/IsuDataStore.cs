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
        private List<Group<Student>> _groups;
        private List<CourseNumber> _courses;
        private Display _display;
        private int _studentId = 100000;

        public IsuDataStore()
        {
            _students = new List<Student>();
            _groups = new List<Group<Student>>();
            _courses = new List<CourseNumber>();
            _display = new Display();
        }

        public bool StudentExists(string name) => _students.Any(student => string.Equals(student.Name, name, StringComparison.CurrentCultureIgnoreCase));
        public bool StudentExistsById(int id) => _students.Any(student => student.Id == id);
        public bool GroupExists(string name) => _groups.Any(group => string.Equals(@group.FullNameGroup, name, StringComparison.CurrentCultureIgnoreCase));
        public bool CourseExists(int courseNumber) => _courses.Any(course => course.Course == courseNumber);

        public Group<Student> AddGroup(string name)
        {
            if (GroupExists(name))
                throw new IsuException("The group already exists");

            var group = new Group<Student>(name);
            _groups.Add(group);

            if (!CourseExists(short.Parse(name.Substring(2, 1))))
            {
                _courses.Add(new CourseNumber(short.Parse(name.Substring(2, 1)), group));
            }
            else
            {
                CourseNumber selectedCourse = _courses.Single(course =>
                    course.Course == group.InformationAboutGroup.CourseNumber);
                selectedCourse.AddGroupToCourse(group);
            }

            return group;
        }

        public Student AddStudent(Group<Student> group, string name)
        {
            if (StudentExists(name))
                throw new IsuException("The student already exists");

            if (GroupExists(group.FullNameGroup))
                group = _groups.Single(selectedGroup => selectedGroup.FullNameGroup == group.FullNameGroup);

            if (group.Students.Count == group.MaxCountStudents)
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
                    course.Course == group.InformationAboutGroup.CourseNumber);
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

        public IReadOnlyList<Student> FindStudents(string groupName)
        {
            if (!GroupExists(groupName))
                throw new IsuException("The group doesn't exists with this name");

            Group<Student> selectedGroup = _groups.Single(group => group.FullNameGroup.ToLower() == groupName.ToLower());

            if (selectedGroup.Students.Count == 0)
                throw new IsuException("Students from group doesn't exists");

            return selectedGroup.Students;
        }

        public IReadOnlyList<Student> FindStudents(CourseNumber courseNumber)
        {
            if (!CourseExists(courseNumber.Course))
                throw new IsuException("The course doesn't exists");

            IReadOnlyList<Group<Student>> selectedGroups = _courses
                .Single(course => course.Course == courseNumber.Course)
                .GroupsFromCourse;

            IReadOnlyList<Student> selectedStudents = selectedGroups.SelectMany(group => @group.Students).ToList();

            if (!selectedStudents.Any())
                throw new IsuException("The students doesn't exists");

            return selectedStudents;
        }

        public Group<Student> FindGroup(string groupName)
        {
            if (!GroupExists(groupName))
                throw new IsuException("The group doesn't exists with this name");

            Group<Student> selectedGroup = _groups.Single(group => group.FullNameGroup.ToLower() == groupName.ToLower());

            return selectedGroup;
        }

        public IReadOnlyList<Group<Student>> FindGroups(CourseNumber courseNumber)
        {
            IReadOnlyList<Group<Student>> selectedGroups = _courses
                .Single(course => course.Course == courseNumber.Course)
                .GroupsFromCourse;

            return selectedGroups;
        }

        public void ChangeStudentGroup(Student student, Group<Student> newGroup)
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