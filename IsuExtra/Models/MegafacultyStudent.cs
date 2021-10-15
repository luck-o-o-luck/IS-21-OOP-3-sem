using System.Collections.Generic;
using System.Linq;
using Isu.Models;
using IsuExtra.Tools;

namespace IsuExtra.Models
{
    public class MegafacultyStudent : Student
    {
        private const int MaxCountOGNP = 2;
        private int _countOgnp;
        private List<StreamOGNP> _ognp;

        public MegafacultyStudent(string fullName, MegafacultyGroup group, int id)
            : base(fullName, group, id)
        {
            Group = @group ?? throw new IsuExtraException("OGNP is null");
            _ognp = new List<StreamOGNP>();
            _countOgnp = 0;
        }

        public MegafacultyGroup Group { get; }
        public int CountOGNP => _countOgnp;
        public IReadOnlyList<StreamOGNP> OGNPs => _ognp;
        public void AddOGNPForStudent(StreamOGNP ognp)
        {
            if (ognp is null)
                throw new IsuExtraException("OGNP is null");
            if (ognp.Faculty == Group.InformationAboutGroup.Faculty)
                throw new IsuExtraException("You can't choose the course of your faculty");
            if (_ognp.Count == MaxCountOGNP)
                throw new IsuExtraException("This student has a two OGNP");
            if (_countOgnp == MaxCountOGNP)
                throw new IsuExtraException("This student has max count ognp");
            if (Group.Schedule.Contain(ognp.Schedule.Lessons))
                throw new IsuExtraException("Intersection of schedules");

            _ognp.Add(ognp);
            _countOgnp += 1;
        }

        public StreamOGNP CancelOGNP(OGNP ognp)
        {
            var selectedOGNP = _ognp.SingleOrDefault(x => x.OGNP.NameOGNP == ognp.NameOGNP);

            if (selectedOGNP is null)
                throw new IsuExtraException("This student doesn't exist this ognp");

            _ognp.Remove(selectedOGNP);
            _countOgnp -= 1;

            return selectedOGNP;
        }
    }
}