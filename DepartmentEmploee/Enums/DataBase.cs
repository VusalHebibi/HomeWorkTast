using System.Collections.Generic;

namespace DepartmentEmploee
{
    public static class DataBase
    {
        public static int IdentityDepartment { get; set; }

        public static int IdentityEmploee { get; set; }

        public static List<Department> Departments { get; set; } = new List<Department>();

        public static List<Employee> Employees { get; set; } = new List<Employee>();
    }
}
