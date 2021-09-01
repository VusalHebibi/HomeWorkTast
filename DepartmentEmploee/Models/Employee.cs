using System.Linq;

namespace DepartmentEmploee
{
    public class Employee
    {
        private int id;
        private string no;
        private string fullName;
        private string position;
        private double salary;
        private string departmentName;

        public Employee(string fullName, string position, double salary, string departmentName)
        {
            id = ++DataBase.IdentityEmploee;
            FullName = fullName;
            Position = position;
            Salary = salary;
            if (DataBase.Departments.Any(d => d.Name == departmentName))
            {
                this.departmentName = departmentName;
            }
            no = string.Concat(DepartmentName.Substring(0, 2), id < 1000 ? (1000 + id).ToString() : id.ToString());
        }

        public string EmployeeNo { get => no; }

        public string FullName 
        { 
            get => fullName; 
            set
            {
                fullName = value;
            }
        }

        public string Position
        {
            get => position;
            set
            {
                if (value.Length >= 2)
                {
                    position = value;
                }
            }
        }

        public double Salary
        {
            get => salary;
            set
            {
                if (value >= 250)
                {
                    salary = value;
                }
            }
        }

        public string DepartmentName 
        { 
            get => departmentName; 
            set => departmentName = value; 
        }


        public override string ToString()
        {
            return $"İşçi No: {no};\nİşçi: {fullName};\nDepartament: {departmentName};\nVezife:{position};\nMaaş:{salary};\n\n";
        }
    }
}
