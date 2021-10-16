using System;
using System.Linq;
using Isu.Models;
using Isu.Services;
using Isu.Tools;
using NUnit.Framework;

namespace Isu.Tests
{
    public class Tests
    {
        private IIsuService _isuService;

        [SetUp]
        public void Setup()
        {
            //TODO: implement
            _isuService = null;
        }

        [Test]
        public void AddStudentToGroup_StudentHasGroupAndGroupContainsStudent()
        {
            _isuService = new IsuDataStore();
            _isuService.AddStudent(new Group<Student>("M3201"), "Васютинская Ксения");
            Assert.AreEqual(_isuService.FindStudent("Васютинская Ксения").GroupIsu, _isuService.FindGroup("M3201"));
            Assert.AreEqual(_isuService.FindStudent("Васютинская Ксения"),
                _isuService.FindGroup("M3201").Students.Single(student => student.Name == "Васютинская Ксения"));
        }

        [Test]
        public void ReachMaxStudentPerGroup_ThrowException()
        {
            _isuService = new IsuDataStore();
            _isuService.AddGroup("M3201");

            for (int i = 0; i < _isuService.FindGroup("M3201").MaxCountStudents ; i++)
            {
                _isuService.AddStudent(_isuService.FindGroup("M3201"), $"student{i + 1}");
            }
            
            Assert.Catch<IsuException>(()=>
            {
                _isuService.AddStudent(new Group<Student>("M3201"), "Халеев Михаил");
            });
        }

        [Test]
        public void CreateGroupWithInvalidName_ThrowException()
        {
            Assert.Catch<IsuException>(() =>
            {
                _isuService = new IsuDataStore();
                _isuService.AddGroup("M3607");
            });
        }

        [Test]
        public void TransferStudentToAnotherGroup_GroupChanged()
        {
            Assert.Catch<IsuException>(() =>
            {
                _isuService = new IsuDataStore();
                _isuService.AddStudent(new Group<Student>("M3201"), "Васютинская Ксения");
                _isuService.ChangeStudentGroup(_isuService.FindStudent("Васютинская Ксения"), new Group<Student>("M3401"));
            });
        }
    }
}