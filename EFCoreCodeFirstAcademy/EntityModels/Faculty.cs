using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCoreCodeFirstAcademy.EntityModels;

public class Faculty
{
    public int FacultyId { get; set; }
    public string Name { get; set; }

    public List<Student> Students { get; set; }

    public Faculty()
    {
        Students = new();
    }
}
