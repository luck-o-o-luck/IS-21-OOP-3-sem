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
            try
            {
                var io = new IsuDataStore();
                io.AddStudent(new Group("M3201"), "Васютинская Ксения");
                io.Print();
            }
            catch
            {
                Assert.Fail();  
            }
        }

        [Test]
        public void ReachMaxStudentPerGroup_ThrowException()
        {
            Assert.Catch<IsuException>(() =>
            {
                var io = new IsuDataStore();
                io.AddStudent(new Group("M3201"), "Андреев Михаил");
                io.AddStudent(new Group("M3201"), "Васютинская Ксения");
                io.AddStudent(new Group("M3201"), "Гайнутдинов Самат");
                io.AddStudent(new Group("M3201"), "Головин Максим");
                io.AddStudent(new Group("M3201"), "Григорович Вячеслав");
                io.AddStudent(new Group("M3201"), "Евтюхов Дмитрий");
                io.AddStudent(new Group("M3201"), "Иванов Никита");
                io.AddStudent(new Group("M3201"), "Корчагин Артём");
                io.AddStudent(new Group("M3201"), "Кудашев Искандер");
                io.AddStudent(new Group("M3201"), "Кулябин Денис");
                io.AddStudent(new Group("M3201"), "Либченко Михаил");
                io.AddStudent(new Group("M3201"), "Мамедов Мансур Солтан-Махмуд Оглы");
                io.AddStudent(new Group("M3201"), "Мирошниченко Александр");
                io.AddStudent(new Group("M3201"), "Муров Глеб");
                io.AddStudent(new Group("M3201"), "Перевощиков Радомир");
                io.AddStudent(new Group("M3201"), "Пискуровский Матвей");
                io.AddStudent(new Group("M3201"), "Сергеев Егор");
                io.AddStudent(new Group("M3201"), "Солдатов Константин");
                io.AddStudent(new Group("M3201"), "Сухов Владимир");
                io.AddStudent(new Group("M3201"), "Хакимов Руслан");
                io.AddStudent(new Group("M3201"), "Халеев Михаил");
                io.Print(); 
            });
        }

        [Test]
        public void CreateGroupWithInvalidName_ThrowException()
        {
            Assert.Catch<IsuException>(() =>
            {
                var io = new IsuDataStore();
                io.AddGroup("M3607");
            });
        }

        [Test]
        public void TransferStudentToAnotherGroup_GroupChanged()
        {
            Assert.Catch<IsuException>(() =>
            {
                var io = new IsuDataStore();
                io.AddStudent(new Group("M3201"), "Васютинская Ксения");
                io.ChangeStudentGroup(io.FindStudent("Васютинская Ксения"), new Group("M3401"));
            });
        }
    }
}