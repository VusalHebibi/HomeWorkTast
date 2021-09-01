using System.Collections.Generic;
using System.Linq;

namespace DepartmentEmploee
{
    public class Department
    {
        int id;
        private string name;
        private int workerLimit;
        private double salaryLimit;

        public Department(string name, int workerLimit, double salaryLimit)
        {
            id = ++DataBase.IdentityDepartment;
            Name = name;
            WorkerLimit = workerLimit;
            SalaryLimit = salaryLimit;
            Employees = new List<Employee>();
        }

        public string Name
        {
            get => name;
            set
            {
                if (value.Length >= 2)
                {
                    name = value;
                }
            }
        }

        public int WorkerLimit
        {
            get => workerLimit;
            set
            {
                if (value >= 1)
                {
                    workerLimit = value;
                }
            }
        }

        public double SalaryLimit
        {
            get => salaryLimit;
            set
            {
                if (value >= 250.0)
                {
                    salaryLimit = value;
                }
            }
        }

        public List<Employee> Employees { get; set; }

        public double CalcSalaryAverage()
        {
            if (Employees.Any())
            {
                return Employees.Sum(s => s.Salary) / Employees.Count;
            }
            return 0;
        }

        public override string ToString()
        {
            return $"Departament: {Name};\nİşçi sayı: {Employees.Count};\nOrta maaş: {CalcSalaryAverage()};\n\n";
        }
    }
}
