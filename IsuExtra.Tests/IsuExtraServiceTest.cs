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
            lessons.Add(new Lesson("ООП", new Time("08:20"), new Time("09:50"), "Fredi Cats, Anchous", group, "Tuesday", 151));
            _isuService.AddScheduleForGroup(new Schedule(lessons), group);

            Assert.AreEqual(group.Schedule.Lessons[0].Name, "ООП");
            Assert.AreEqual(group.Schedule.Lessons[0].Teacher, "Fredi Cats, Anchous");
            Assert.AreEqual(group.Schedule.Lessons[0].Day, "Tuesday");
        }

        [Test]
        public void AddStreamOGNP_OGNPHasStreamAndStreamHasOGNP()
        {
            _isuService = new IsuExtraService();
            
            var ognp = _isuService.AddOgnp("Прикладная математика и программирование", 'M');
            
            List<Lesson> lessons = new List<Lesson>();
            lessons.Add(new Lesson("Функциональный анализ", new Time("08:20"), new Time("09:50"), "Noname", "Monday", 1000));
            var schedule = new Schedule(lessons);
            
            var streamOGNP = _isuService.AddStreamOgnp(schedule, 'M', "Прикладная математика и программирование");
            var selectedStreams = _isuService.GetStreamsOgnp(ognp);
            
            Assert.AreEqual(ognp.Streams[0], streamOGNP);
            Assert.AreEqual(streamOGNP.Ognp, ognp);
            Assert.AreEqual(selectedStreams[0], streamOGNP);
        }

        [Test]
        public void AddOGNPForStudent_ThrowException()
        {
            _isuService = new IsuExtraService();
            
            var group =_isuService.AddGroup("M3201");
            var lessons = new List<Lesson>();
            var student = _isuService.AddStudent(group, "Васютинская Ксения");
            lessons.Add(new Lesson("ООП", new Time("08:20"), new Time("09:50"), "Fredi Cats, Anchous", group, "Tuesday", 151));
            _isuService.AddScheduleForGroup(new Schedule(lessons), group);
            
            var ognp = _isuService.AddOgnp("Прикладная математика и программирование", 'M');
            
            List<Lesson> lessonsForOGNP = new List<Lesson>();
            lessonsForOGNP.Add(new Lesson("Функциональный анализ", new Time("08:20"), new Time("09:50"), "Noname", "Tuesday", 1000));
            var schedule = new Schedule(lessonsForOGNP);
            
            var streamOGNP = _isuService.AddStreamOgnp(schedule, 'M', "Прикладная математика и программирование");

            Assert.Catch<IsuExtraException>(()=>
            {
                _isuService.AddStudentToStreamOgnp(student, streamOGNP);
            });
        }

        [Test]
        public void GetGroupOfStudentsWhoDidntEnrollAndGetStudentInOGNP()
        {
            _isuService = new IsuExtraService();
            
            MegafacultyGroup group =_isuService.AddGroup("M3201");
            var lessons = new List<Lesson>();
            var student = _isuService.AddStudent(group, "Васютинская Ксения");
            lessons.Add(new Lesson("ООП", new Time("08:20"), new Time("09:50"), "Fredi Cats, Anchous", group, "Tuesday", 151));
            _isuService.AddScheduleForGroup(new Schedule(lessons), group);
            
            var ognp = _isuService.AddOgnp("Предпринимательство и инноватика", 'P');
            
            List<Lesson> lessonsForOGNP = new List<Lesson>();
            lessonsForOGNP.Add(new Lesson("Экономика приоритетных отраслей", new Time("10:00"), new Time("11:30"), "Noname", "Tuesday", 1000));
            var schedule = new Schedule(lessonsForOGNP);
            
            var streamOGNP = _isuService.AddStreamOgnp(schedule, 'P', "Предпринимательство и инноватика");

            var students = _isuService.GetUnsubscribedStudents(group);
            
            Assert.AreEqual(students[0].Name, "Васютинская Ксения");
            
            _isuService.AddStudentToStreamOgnp(student, streamOGNP);

            students = _isuService.GetStudentsFromOgnp(streamOGNP);
            
            Assert.AreEqual(students[0].Name, "Васютинская Ксения");
        }

        [Test]
        public void RemoveFromCourseRecord_StudentHasntOGNP()
        {
            _isuService = new IsuExtraService();
            
            var group =_isuService.AddGroup("M3201");
            var lessons = new List<Lesson>();
            var student = _isuService.AddStudent(group, "Васютинская Ксения");
            lessons.Add(new Lesson("ООП", new Time("08:20"), new Time("09:50"), "Fredi Cats, Anchous", group, "Tuesday", 151));
            _isuService.AddScheduleForGroup(new Schedule(lessons), group);
            
            var ognp = _isuService.AddOgnp("Предпринимательство и инноватика", 'P');
            
            List<Lesson> lessonsForOGNP = new List<Lesson>();
            lessonsForOGNP.Add(new Lesson("Экономика приоритетных отраслей", new Time("10:00"), new Time("11:30"), "Noname", "Tuesday", 1000));
            var schedule = new Schedule(lessonsForOGNP);
            
            var streamOGNP = _isuService.AddStreamOgnp(schedule, 'P', "Предпринимательство и инноватика");

            _isuService.AddStudentToStreamOgnp(student, streamOGNP);

            _isuService.CancelStudentsOgnp(student, ognp);
            Assert.AreEqual(student.Ognp, null);
        }
    }
}