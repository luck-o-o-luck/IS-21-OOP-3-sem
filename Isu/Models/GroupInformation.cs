using System;
using Isu.Tools;

namespace Isu.Models
{
    public class GroupInformation
    {
        public GroupInformation(string name)
        {
            Faculty = char.Parse(name.Substring(0, 1));
            Direction = short.Parse(name.Substring(1, 1));
            GroupNumber = short.Parse(name.Substring(3, 2));
            CourseNumber = short.Parse(name.Substring(2, 1));

            if (CourseNumber < 1 || CourseNumber > 4)
                throw new IsuException("The group can't be created");
        }

        public char Faculty { get; }
        public int Direction { get; }
        public int GroupNumber { get; }
        public int CourseNumber { get; }
    }
}