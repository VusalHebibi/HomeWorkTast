using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DepartmentEmploee
{
    public class HumanResourceManager : IHumanResourceManager
    {
        public string Departments()
        {
            string result = "";
            if (!GetDepartments().Any())
            {
                result = "Departament yoxdur";
            }
            else
            {
                foreach (var department in GetDepartments())
                {
                    result += department.ToString() + "\n";
                }
            }
            return result;
        }

        public List<Department> GetDepartments()
        {
            return DataBase.Departments.ToList();
        }
        public bool AddDepartment(string name, int workerLimit, double salaryLimit, out string error)
        {
            bool result = true;
            error = "";
            if (DataBase.Departments.Any(d => d.Name == name))
            {
                error += $"{name} adlı departament artıq mövcuddur.\n";
                return false;
            }
            if (name.Length < 2)
            {
                result = false;
                error += "Departament adında en azı 2 simvol olmalıdır.\n";
            }
            if (workerLimit < 1)
            {
                result = false;
                error += "Departamentde işçi sayı en azı 1 olmalıdır.\n";
            }
            if (salaryLimit < 250)
            {
                result = false;
                error += "Departamentde işçilerin maaş limiti en azı 250 olmalıdır.\n";
            }
            if (result)
            {
                var department = new Department(name, workerLimit, salaryLimit);
                DataBase.Departments.Add(department);
            }
            return result;
        }

        public bool EditDepartment(string oldName, string newName, out string error)
        {
            bool result = true;
            error = "";
            var department = DataBase.Departments.FirstOrDefault(d => d.Name == oldName);
            if (department == null)
            {
                error += "Departament tapılmadı.\n";
                return false;
            }
            if (DataBase.Departments.Any(d => d.Name == newName))
            {
                error += $"{newName} adlı departament artıq mövcuddur.\n";
                return false;
            }
            if (result)
            {
                RefreshEmploeesDepartment(oldName, newName);
                department.Name = newName;
                RefreshDepartmentEmployees(newName);
            }
            return result;
        }

        public bool GetDepartmentInfo(string departmentName, out string error)
        {
            var department = DataBase.Departments.FirstOrDefault(d => d.Name == departmentName);
            if (department == null)
            {
                error = $"{departmentName} adlı departament tapılmadı.\n";
                return false;
            }
            error = department.ToString();
            return true;
        }


        public string Employees()
        {
            string result = "";
            if (!DataBase.Employees.Any())
            {
                result = "İşçi yoxdur.\n";
            }
            else
            {
                foreach (var employee in DataBase.Employees)
                {
                    result += employee.ToString() + "\n";
                }
            }
            return result;
        }

        public bool GetEmployeeInfo(string emploeeNo, out string result)
        {
            var emploee = DataBase.Employees.FirstOrDefault(e => e.EmployeeNo == emploeeNo);
            if (emploee == null)
            {
                result = $"{emploeeNo} nömreli işçi tapılmadı.\n";
                return false;
            }
            result = emploee.ToString();
            return true;
        }

        public string GetDepartmentEmployees(string departmentName)
        {
            string result = "";
            var department = DataBase.Departments.FirstOrDefault(d => d.Name == departmentName);
            if (department == null)
            {
                return $"{departmentName} adlı departament tapılmadı.\n";
            }
            if (!DataBase.Employees.Any())
            {
                return "İşçi yoxdur.\n";
            }
            foreach (var employee in DataBase.Employees)
            {
                result += employee.ToString() + "\n";
            }
            return result;
        }

        public bool AddEmployee(string fullName, string position, double salary, string departmentName, out string error)
        {
            bool result = true;
            error = "";
            var department = DataBase.Departments.FirstOrDefault(d => d.Name == departmentName);
            if (position.Length < 2)
            {
                result = false;
                error += "Vezife adında en azı 2 simvol olmalıdır.\n";
            }
            if (salary < 250)
            {
                result = false;
                error += "İşçinin maaş limiti en azı 250 olmalıdır.\n";
            }

            if (department == null)
            {
                result = false;
                error += "Departament tapılmadı.\n";
            }
            else
            {
                if (department.WorkerLimit <= department.Employees.Count)
                {
                    result = false;
                    error += "Departamentde işçi limiti aşır.\n";
                }

                if (department.SalaryLimit <= department.Employees.Sum(s => s.Salary) + salary)
                {
                    result = false;
                    error += "Departamentde maaş limiti aşır.\n";
                }

            }

            if (result)
            {
                var employee = new Employee(fullName, position, salary, departmentName);
                DataBase.Employees.Add(employee);
                RefreshDepartmentEmployees(department.Name);
            }
            return result;
        }

        public bool EditEmployee(string employeeNo, string position, double salary, out string error)
        {
            bool result = true;
            error = "";
            var employee = DataBase.Employees.FirstOrDefault(e => e.EmployeeNo == employeeNo);
            if (employee == null)
            {
                error += "Nömreye uyğun işçi tapılmadı.\n";
                return false;
            }

            var department = DataBase.Departments.FirstOrDefault(d => d.Name == employee.DepartmentName);
            if (position.Length < 2)
            {
                result = false;
                error += "Vezife adında en azı 2 simvol olmalıdır.\n";
            }
            if (salary < 250)
            {
                result = false;
                error += "İşçinin maaş limiti en azı 250 olmalıdır.\n";
            }
            if (department.SalaryLimit <= department.Employees.Sum(s => s.Salary) + salary - employee.Salary)
            {
                result = false;
                error += "Departamentin maaş limiti aşır.\n";
            }
            
            if (result)
            {
                employee.Position = position;
                employee.Salary = salary;
                RefreshDepartmentEmployees(employee.DepartmentName);
            }
            return result;
        }

        public bool RemoveEmployee(string employeeNo, string departmentName, out string error)
        {
            bool result = true;
            error = "";
            var employee = DataBase.Employees.FirstOrDefault(e => e.DepartmentName == departmentName && e.EmployeeNo == employeeNo);
            if (employee == null)
            {
                error = $"İşçi tapılmadı.\n";
                return false;
            }
            if (result)
            {
                DataBase.Employees.Remove(employee);
                RefreshDepartmentEmployees(employee.DepartmentName);
            }
            return result;
        }

        private void RefreshDepartmentEmployees(string departmentName)
        {
            var department = DataBase.Departments.FirstOrDefault(d => d.Name == departmentName);
            if (department != null)
            {
                department.Employees.Clear();
                department.Employees = DataBase.Employees.Where(e => e.DepartmentName == department.Name).ToList();
            }
            
        }

        private void RefreshEmploeesDepartment(string oldName, string newName)
        {
            var employees = DataBase.Employees.Where(e => e.DepartmentName == oldName).ToList();
            if (employees != null)
            {
                employees.ForEach(e => e.DepartmentName = newName);
            }
        }

        public bool EditEmployee(string employeeNo, string fullName, string position, double salary, out string error)
        {
            throw new NotImplementedException();
        }
    }
}
