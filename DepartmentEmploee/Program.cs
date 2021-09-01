using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DepartmentEmploee
{
    class Program
    {
        static string commandText = "1.1 - Departameantlerin siyahisini gostermek\n1.2 - Departamenet yaratmaq\n1.3 - Departmanetde deyisiklik etmek\n2.1 - Iscilerin siyahisini gostermek\n2.2 - Departamentdeki iscilerin siyahisini gostermrek\n2.3 - Isci elave etmek\n2.4 - Isci uzerinde deyisiklik etmek\n2.5 - Departamentden isci silinmesi\n3.1 - Cixis\n\nEmr daxil edin: ";

        static void Main(string[] args)
        {
            HumanResourceManager hr = new HumanResourceManager();
            string command = "";
            string error = "";
            bool result;

            string departmentName;
            int workerLimit;
            double salaryLimit;

            string employeeNo;
            string fullName;
            string position;
            double salary;

            while (command != "3.1")
            {
                Console.Write(commandText);
                command = Console.ReadLine();
                switch (command)
                {
                    //console penceresine sistemedki departamentlerin adlari, isci sayi ve iscilerinin  maas ortalamalari gosterilmelidir
                    case "1.1":
                        Console.WriteLine(hr.Departments() + "\n");
                        break;
                    //yeni departamenet yaratmaq ucun lazim olan deyreler console-dan daxil edilmelidir,yanlis olduqda tekrar istenmelidir
                    case "1.2":
                        result = false;
                        error = "";
                        while (!result)
                        {
                            Console.WriteLine(error);
                            Console.Write("Departament adini daxil edin: ");
                            departmentName = Console.ReadLine();
                            Console.Write("Departament isci limitini daxil edin: ");
                            int.TryParse(Console.ReadLine(), out workerLimit);
                            Console.Write("Departament maas limitini daxil edin: ");
                            double.TryParse(Console.ReadLine(), out salaryLimit);
                            result = hr.AddDepartment(departmentName, workerLimit, salaryLimit, out error);
                        }
                        Console.WriteLine("Departament elave olundu.\n");
                        break;
                    //consoledan deyisiklik edilecek departamaentin adi daxil edilir,eger o adda departament yoxdursa xeta mesaji verir ve proses menusuna qayidir, yox eger varsa departamentin haziki deyerlerini gosterir ve yeni deyerleri daxil etmesini isteyir
                    case "1.3":
                        Console.Write("Deyisiklik edilecek Departament adini daxil edin: ");
                        departmentName = Console.ReadLine();
                        result = hr.GetDepartmentInfo(departmentName, out error);
                        Console.WriteLine(error);
                        if (result)
                        {
                            Console.Write("Yeni Departament adini daxil edin: ");
                            string newDepartmentName = Console.ReadLine();
                            result = hr.EditDepartment(departmentName, newDepartmentName, out error);
                            if (result)
                            {
                                Console.WriteLine("Departament adi deyishdirildi.");
                            }
                            else
                            {
                                Console.WriteLine(error);
                            }
                        }
                        break;
                    //sistemdeki butun iscilerin nomre, ad soyad, departament adi ve maasi gosterilir
                    case "2.1":
                        Console.WriteLine(hr.Employees() + "\n");
                        break;
                    //consoledan departament adi daxil edilir ve hemin departamentdeki iscilerin nomre,ad soyad, vezife ve maas deyerleri gosterilir
                    case "2.2":
                        Console.Write("Departament adini daxil edin: ");
                        departmentName = Console.ReadLine();
                        Console.WriteLine(hr.GetDepartmentEmployees(departmentName));
                        break;
                    //Yeni isci elave edilmesi ucun lazim olan melumatlarin console-dan daxil edilmesi istenilir, deyerler yanlis olduqda tekrar daxil edilmesi istenilir
                    case "2.3":
                        result = false;
                        error = "";
                        while (!result)
                        {
                            Console.WriteLine(error);
                            Console.Write("Tam adinizi daxil edin: ");
                            fullName = Console.ReadLine();
                            Console.Write("Vezifeni daxil edin: ");
                            position = Console.ReadLine();
                            Console.Write("Maas daxil edin: ");
                            double.TryParse(Console.ReadLine(), out salary);
                            Console.Write("Departamenti daxil edin: ");
                            departmentName = Console.ReadLine();
                            result = hr.AddEmployee(fullName, position, salary,departmentName, out error);
                        }
                        Console.WriteLine("Isci elave olundu.\n");
                        break;
                    //console-dan deyisikkik edilecek iscinin nomresi daxil edilir, o nomrede isci yoxdursa xeta mesaji veirr ve proses menusuna qayidir. Eger varsa o iscinin hazirki melumatlarini (fullname, salary, position) ve console-dan salary ve position ucun yeni deyerler teyin etmesi gozlenilir. (iscinin ancaq salary ve positionu editlene bilir)
                    case "2.4":
                        Console.Write("Isci nomresini daxil edin: ");
                        employeeNo = Console.ReadLine();
                        result = hr.GetEmployeeInfo(employeeNo, out error);
                        Console.WriteLine(error);
                        if (result)
                        {
                            Console.Write("Vezifeni daxil edin: ");
                            position = Console.ReadLine();
                            Console.Write("Maas daxil edin: ");
                            double.TryParse(Console.ReadLine(), out salary);
                            result = hr.EditEmployee(employeeNo, position,salary, out error);
                            Console.WriteLine(result ? "Isci melumati deyisdi" : error);
                        }
                        break;
                    //Departamentden isci silinmesi - Parametr olaraq departament adi ve iscinin nomresi qebul edilir.Gonderilen adda departamentden gonderilen adda isci departamentin isciler arrayinden silinir
                    case "2.5":
                        Console.Write("Departamenti daxil edin: ");
                        departmentName = Console.ReadLine();
                        Console.Write("Isci nomresini daxil edin: ");
                        employeeNo = Console.ReadLine();
                        result = hr.RemoveEmployee(employeeNo, departmentName, out error);
                        if (result)
                        {
                            Console.WriteLine("Isci silindi");
                        }
                        else
                        {
                            Console.WriteLine(error);
                        }
                        break;
                    default:
                        Console.WriteLine("Yalnis emr daxil edilib.\n");
                        break;
                }
            }
        }
    }
}
