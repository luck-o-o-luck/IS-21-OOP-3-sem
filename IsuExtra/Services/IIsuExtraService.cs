using System.Collections.Generic;
using IsuExtra.Models;

namespace IsuExtra.Services
{
    public interface IIsuExtraService
    {
        OGNP AddOGNP(string name, char faculty);
        MegafacultyStudent AddStudent(MegafacultyGroup group, string name);
        MegafacultyGroup AddGroup(string name);
        StreamOGNP AddStreamOGNP(Schedule schedule, char faculty, string name);
        Schedule AddScheduleForGroup(Schedule newShedule, MegafacultyGroup group);

        IReadOnlyList<StreamOGNP> GetStreamsOGNP(OGNP ognp);
        MegafacultyStudent AddStudentToStreamOGNP(MegafacultyStudent student, StreamOGNP streamOgnp);
        MegafacultyStudent CancelStudentsOGNP(MegafacultyStudent student, OGNP ognp);
        IReadOnlyList<MegafacultyStudent> GetStudentsFromOGNP(StreamOGNP ognp);
        IReadOnlyList<MegafacultyStudent> GetUnsubscribedStudents(MegafacultyGroup group);
    }
}