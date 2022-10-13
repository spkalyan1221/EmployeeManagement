namespace EmployeeManagement.Models.Domainmodel
{
    public class Employee
    {
        public Guid ID { get; set; }
        public string Name { get; set; }

        public string Department { get; set; }
        public string Email { get; set; }

        public string  Sex { get; set; }

        public string MartialStatus { get; set; }
        public long Salary { get; set; }
        public DateTime DateOfBirth { get; set; }

    }
}
