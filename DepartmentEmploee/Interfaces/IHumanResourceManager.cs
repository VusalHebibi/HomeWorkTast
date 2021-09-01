using System.Collections.Generic;

namespace DepartmentEmploee
{
    public interface IHumanResourceManager
    {
        string Departments();

        bool AddDepartment(string name, int workerLimit, double salaryLimit, out string error);

        List<Department> GetDepartments();

        bool EditDepartment(string oldName, string newName, out string error);

        bool AddEmployee(string fullName, string position, double salary, string departmentName, out string error);

        bool RemoveEmployee(string employeeNo, string departmentName, out string error);

        bool EditEmployee(string employeeNo, string fullName, string position, double salary, out string error);

    }
}
