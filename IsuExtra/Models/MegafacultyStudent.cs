using System.Collections.Generic;
using System.Linq;
using Isu.Models;
using IsuExtra.Tools;

namespace IsuExtra.Models
{
    public class MegafacultyStudent : Student
    {
        private const int MaxCountOGNP = 2;

        public MegafacultyStudent(string fullName, MegafacultyGroup group, int id)
            : base(fullName, null, id)
        {
            Group = @group ?? throw new IsuExtraException("OGNP is null");
            Ognp = null;
        }

        public MegafacultyGroup Group { get; }
        public StreamOgnp Ognp { get; private set; }
        public void AddOGNPForStudent(StreamOgnp ognp)
        {
            if (ognp is null)
                throw new IsuExtraException("OGNP is null");
            if (ognp.Faculty == Group.InformationAboutGroup.Faculty)
                throw new IsuExtraException("You can't choose the course of your faculty");
            if (Ognp != null)
                throw new IsuExtraException("This student has ognp");
            if (Group.Schedule.Contain(ognp.Schedule.Lessons))
                throw new IsuExtraException("Intersection of schedules");

            Ognp = ognp;
        }

        public StreamOgnp CancelOGNP(Ognp ognp)
        {
            if (Ognp.Name != ognp.NameOGNP)
                throw new IsuExtraException("This student hasn't this ognp");
            if (ognp is null)
                throw new IsuExtraException("This student doesn't exist this ognp");

            Ognp = null;

            return Ognp;
        }
    }
}