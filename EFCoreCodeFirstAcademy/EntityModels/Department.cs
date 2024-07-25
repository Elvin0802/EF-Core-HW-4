using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCoreCodeFirstAcademy.EntityModels;

public class Department
{
    public int DepartmentId { get; set; }
    public decimal Financing { get; set; }
    public string Name { get; set; }

    public List<Teacher> Teachers { get; set; }

    public Department()
    {
        Teachers = new();
    }
}
