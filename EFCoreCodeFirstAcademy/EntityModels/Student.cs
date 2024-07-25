using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCoreCodeFirstAcademy.EntityModels;

public class Student
{
    public int StudentId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }

    public int GroupId { get; set; }
    public Group Group { get; set; }

    public int FacultyId { get; set; }
    public Faculty Faculty { get; set; }

    public Student()
    {
        
    }
}
