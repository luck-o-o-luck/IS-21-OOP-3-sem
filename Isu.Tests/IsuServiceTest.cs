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
                _isuService = new IsuDataStore();
                _isuService.AddStudent(new Group("M3201"), "Васютинская Ксения");
                _isuService.Print();
            }
            catch
            {
                Assert.Fail();  
            }
        }

        [Test]
        public void ReachMaxStudentPerGroup_ThrowException()
        {
            _isuService = new IsuDataStore();
            _isuService.AddStudent(new Group("M3201"), "Андреев Михаил");
            _isuService.AddStudent(new Group("M3201"), "Васютинская Ксения");
            _isuService.AddStudent(new Group("M3201"), "Гайнутдинов Самат");
            _isuService.AddStudent(new Group("M3201"), "Головин Максим");
            _isuService.AddStudent(new Group("M3201"), "Григорович Вячеслав");
            _isuService.AddStudent(new Group("M3201"), "Евтюхов Дмитрий");
            _isuService.AddStudent(new Group("M3201"), "Иванов Никита");
            _isuService.AddStudent(new Group("M3201"), "Корчагин Артём");
            _isuService.AddStudent(new Group("M3201"), "Кудашев Искандер");
            _isuService.AddStudent(new Group("M3201"), "Кулябин Денис");
            _isuService.AddStudent(new Group("M3201"), "Либченко Михаил");
            _isuService.AddStudent(new Group("M3201"), "Мамедов Мансур Солтан-Махмуд Оглы");
            _isuService.AddStudent(new Group("M3201"), "Мирошниченко Александр");
            _isuService.AddStudent(new Group("M3201"), "Муров Глеб");
            _isuService.AddStudent(new Group("M3201"), "Перевощиков Радомир");
            _isuService.AddStudent(new Group("M3201"), "Пискуровский Матвей");
            _isuService.AddStudent(new Group("M3201"), "Сергеев Егор");
            _isuService.AddStudent(new Group("M3201"), "Солдатов Константин");
            _isuService.AddStudent(new Group("M3201"), "Сухов Владимир");
            _isuService.AddStudent(new Group("M3201"), "Хакимов Руслан");
            
            Assert.Catch<IsuException>(()=>
            {
                _isuService.AddStudent(new Group("M3201"), "Халеев Михаил");
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
                _isuService.AddStudent(new Group("M3201"), "Васютинская Ксения");
                _isuService.ChangeStudentGroup(_isuService.FindStudent("Васютинская Ксения"), new Group("M3401"));
            });
        }
    }
}