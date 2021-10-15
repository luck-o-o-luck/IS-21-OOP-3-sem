using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using IsuExtra.Tools;

namespace IsuExtra.Models
{
    public class Schedule
    {
        private List<Lesson> _lessons;

        public Schedule()
        {
            _lessons = new List<Lesson>();
        }

        public Schedule(List<Lesson> lessons)
        {
            _lessons = lessons;
            OrderByTime();
        }

        public IReadOnlyList<Lesson> Lessons => _lessons;

        public void OrderByTime()
        {
            _lessons.OrderBy(lesson => lesson.StartLessons.Hours);
        }

        public void AddLesson(Lesson lesson)
        {
            if (lesson is null)
                throw new IsuExtraException("The lesson doesn't exist");

            _lessons.Add(lesson);
            OrderByTime();
        }

        public bool Contain(IReadOnlyList<Lesson> lessons)
        {
            if (lessons is null)
                throw new IsuExtraException("Schedule is null");

            return _lessons.Any(lesson => lessons.Any(x => x.Day == lesson.Day && x.StartLessons.Hours == lesson.StartLessons.Hours));
        }
    }
}