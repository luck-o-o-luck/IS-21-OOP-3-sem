using System.Collections.Generic;
using IsuExtra.Models;

namespace IsuExtra.Services
{
    public interface IIsuExtraService
    {
        Ognp AddOgnp(string name, char faculty);
        MegafacultyStudent AddStudent(MegafacultyGroup group, string name);
        MegafacultyGroup AddGroup(string name);
        StreamOgnp AddStreamOgnp(Schedule schedule, char faculty, string name);
        Schedule AddScheduleForGroup(Schedule newShedule, MegafacultyGroup group);

        IReadOnlyList<StreamOgnp> GetStreamsOgnp(Ognp ognp);
        MegafacultyStudent AddStudentToStreamOgnp(MegafacultyStudent student, StreamOgnp streamOgnp);
        MegafacultyStudent CancelStudentsOgnp(MegafacultyStudent student, Ognp ognp);
        IReadOnlyList<MegafacultyStudent> GetStudentsFromOgnp(StreamOgnp ognp);
        IReadOnlyList<MegafacultyStudent> GetUnsubscribedStudents(MegafacultyGroup group);
    }
}