using System;
using Isu.Models;
using IsuExtra.Tools;

namespace IsuExtra.Models
{
    public class Lesson
    {
        public Lesson(string name, string start, string end, string teacher, Group group, string day, int auditorium)
        {
            if (string.IsNullOrEmpty(name))
                throw new IsuExtraException("String with name is empty");
            if (string.IsNullOrEmpty(start))
                throw new IsuExtraException("String with lessons start is empty");
            if (string.IsNullOrEmpty(end))
                throw new IsuExtraException("String with lessons name is empty");
            if (string.IsNullOrEmpty(teacher))
                throw new IsuExtraException("String with lessons name is empty");

            Name = name;
            StartLessons = new Time(start);
            EndLessons = new Time(end);
            Teacher = teacher;
            Auditorium = auditorium;
            Group = @group ?? throw new IsuExtraException("The group doesn't exist");
            Day = day;
        }

        public Lesson(string name, string start, string end, string teacher, string day, int auditorium)
        {
            if (string.IsNullOrEmpty(name))
                throw new IsuExtraException("String with name is empty");
            if (string.IsNullOrEmpty(start))
                throw new IsuExtraException("String with lessons start is empty");
            if (string.IsNullOrEmpty(end))
                throw new IsuExtraException("String with lessons name is empty");
            if (string.IsNullOrEmpty(teacher))
                throw new IsuExtraException("String with lessons name is empty");
            if (auditorium < 1)
                throw new IsuExtraException("This auditorium goesn't exist");

            Name = name;
            StartLessons = new Time(start);
            EndLessons = new Time(end);
            Teacher = teacher;
            Auditorium = auditorium;
            Day = day;
        }

        public string Name { get; }
        public string Teacher { get; }
        public Group Group { get; }
        public int Auditorium { get; }
        public Time StartLessons { get; private set; }
        public Time EndLessons { get; private set; }
        public string Day { get; }

        public Lesson ChangeTimeLessons(Lesson lesson, string start, string end)
        {
            lesson.StartLessons = new Time(start);
            lesson.EndLessons = new Time(end);

            return lesson;
        }
    }
}