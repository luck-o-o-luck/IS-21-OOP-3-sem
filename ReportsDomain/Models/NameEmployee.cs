namespace ReportsDomain.Models
{
    public class NameEmployee
    {
        public NameEmployee() {}
        public NameEmployee(string name, string surname, string positionName)
        {
            Name = name;
            Surname = surname;
            PositionName = positionName;
        }
        public string Name { get; private set; }
        public string Surname { get; private set; }
        public string PositionName { get; private set; }
    }
}