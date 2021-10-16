using System;
using Isu.Models;
using IsuExtra.Tools;

namespace IsuExtra.Models
{
    public class Lesson
    {
        public Lesson(string name, Time start, Time end, string teacher, Group<MegafacultyStudent> group, string day, int auditorium)
        {
            if (string.IsNullOrEmpty(name))
                throw new IsuExtraException("String with name is empty");
            if (string.IsNullOrEmpty(teacher))
                throw new IsuExtraException("String with lessons name is empty");

            Name = name;
            StartLessons = start ?? throw new IsuExtraException("String with lessons start is empty");
            EndLessons = end ?? throw new IsuExtraException("String with lessons name is empty");
            Teacher = teacher;
            Auditorium = auditorium;
            Group = group;
            Day = day;
        }

        public Lesson(string name, Time start, Time end, string teacher, string day, int auditorium)
            : this(name, start, end, teacher, null, day, auditorium)
        {
        }

        public string Name { get; }
        public string Teacher { get; }
        public Group<MegafacultyStudent> Group { get; }
        public int Auditorium { get; }
        public Time StartLessons { get; }
        public Time EndLessons { get; }
        public string Day { get; }
    }
}