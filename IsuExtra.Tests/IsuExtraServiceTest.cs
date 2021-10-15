using System;
using System.Collections.Generic;
using IsuExtra.Models;
using IsuExtra.Services;
using IsuExtra.Tools;
using NUnit.Framework;

namespace IsuExtra.Tests
{
    public class IsuExtraServiceTest
    {
        private IIsuExtraService _isuService;

        [SetUp]
        public void Setup()
        {
            //TODO: implement
            _isuService = null;
        }

        [Test]
        public void AddScheduleForGroup_GroupHasSchedule()
        {
            _isuService = new IsuExtraService();
            
            var group =_isuService.AddGroup("M3201");
            var lessons = new List<Lesson>();
            lessons.Add(new Lesson("ООП", "08:20", "09:50", "Fredi Cats, Anchous", group, "Tuesday", 151));
            _isuService.AddScheduleForGroup(new Schedule(lessons), group);

            Assert.AreEqual(group.Schedule.Lessons[0].Name, "ООП");
            Assert.AreEqual(group.Schedule.Lessons[0].Teacher, "Fredi Cats, Anchous");
            Assert.AreEqual(group.Schedule.Lessons[0].Day, "Tuesday");
        }

        [Test]
        public void AddStreamOGNP_OGNPHasStreamAndStreamHasOGNP()
        {
            _isuService = new IsuExtraService();
            
            var ognp = _isuService.AddOGNP("Прикладная математика и программирование", 'M');
            
            List<Lesson> lessons = new List<Lesson>();
            lessons.Add(new Lesson("Функциональный анализ", "08:20", "09:50", "Noname", "Monday", 1000));
            var schedule = new Schedule(lessons);
            
            var streamOGNP = _isuService.AddStreamOGNP(schedule, 'M', "Прикладная математика и программирование");
            var selectedStreams = _isuService.GetStreamsOGNP(ognp);
            
            Assert.AreEqual(ognp.Streams[0], streamOGNP);
            Assert.AreEqual(streamOGNP.OGNP, ognp);
            Assert.AreEqual(selectedStreams[0], streamOGNP);
        }

        [Test]
        public void AddOGNPForStudent_ThrowException()
        {
            _isuService = new IsuExtraService();
            
            var group =_isuService.AddGroup("M3201");
            var lessons = new List<Lesson>();
            var student = _isuService.AddStudent(group, "Васютинская Ксения");
            lessons.Add(new Lesson("ООП", "08:20", "09:50", "Fredi Cats, Anchous", group, "Tuesday", 151));
            _isuService.AddScheduleForGroup(new Schedule(lessons), group);
            
            var ognp = _isuService.AddOGNP("Прикладная математика и программирование", 'M');
            
            List<Lesson> lessonsForOGNP = new List<Lesson>();
            lessonsForOGNP.Add(new Lesson("Функциональный анализ", "08:20", "09:50", "Noname", "Tuesday", 1000));
            var schedule = new Schedule(lessonsForOGNP);
            
            var streamOGNP = _isuService.AddStreamOGNP(schedule, 'M', "Прикладная математика и программирование");

            Assert.Catch<IsuExtraException>(()=>
            {
                _isuService.AddStudentToStreamOGNP(student, streamOGNP);
            });
        }

        [Test]
        public void GetGroupOfStudentsWhoDidntEnrollAndGetStudentInOGNP()
        {
            _isuService = new IsuExtraService();
            
            var group =_isuService.AddGroup("M3201");
            var lessons = new List<Lesson>();
            var student = _isuService.AddStudent(group, "Васютинская Ксения");
            lessons.Add(new Lesson("ООП", "08:20", "09:50", "Fredi Cats, Anchous", group, "Tuesday", 151));
            _isuService.AddScheduleForGroup(new Schedule(lessons), group);
            
            var ognp = _isuService.AddOGNP("Предпринимательство и инноватика", 'P');
            
            List<Lesson> lessonsForOGNP = new List<Lesson>();
            lessonsForOGNP.Add(new Lesson("Экономика приоритетных отраслей", "10:00", "11:30", "Noname", "Tuesday", 1000));
            var schedule = new Schedule(lessonsForOGNP);
            
            var streamOGNP = _isuService.AddStreamOGNP(schedule, 'P', "Предпринимательство и инноватика");

            _isuService.AddStudentToStreamOGNP(student, streamOGNP);

            var students = _isuService.GetUnsubscribedStudents(group);
            
            Assert.AreEqual(students[0].Name, "Васютинская Ксения");

            students = _isuService.GetStudentsFromOGNP(streamOGNP);
            
            Assert.AreEqual(students[0].Name, "Васютинская Ксения");
        }

        [Test]
        public void RemoveFromCourseRecord_StudentHasntOGNP()
        {
            _isuService = new IsuExtraService();
            
            var group =_isuService.AddGroup("M3201");
            var lessons = new List<Lesson>();
            var student = _isuService.AddStudent(group, "Васютинская Ксения");
            lessons.Add(new Lesson("ООП", "08:20", "09:50", "Fredi Cats, Anchous", group, "Tuesday", 151));
            _isuService.AddScheduleForGroup(new Schedule(lessons), group);
            
            var ognp = _isuService.AddOGNP("Предпринимательство и инноватика", 'P');
            
            List<Lesson> lessonsForOGNP = new List<Lesson>();
            lessonsForOGNP.Add(new Lesson("Экономика приоритетных отраслей", "10:00", "11:30", "Noname", "Tuesday", 1000));
            var schedule = new Schedule(lessonsForOGNP);
            
            var streamOGNP = _isuService.AddStreamOGNP(schedule, 'P', "Предпринимательство и инноватика");

            _isuService.AddStudentToStreamOGNP(student, streamOGNP);

            _isuService.CancelStudentsOGNP(student, ognp);
            Assert.AreEqual(student.CountOGNP, 0);
            Assert.AreEqual(student.OGNPs.Count, 0);
        }
    }
}