using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCoreCodeFirstAcademy.EntityModels;

public class Group
{
    public int GroupId { get; set; }
    public string Name { get; set; }
    public int Rating { get; set; }
    public int Year { get; set; }

    public List<Student> Students { get; set; }
    public List<Teacher> Teachers { get; set; }

    public Group()
    {
        Students = new();
        Teachers = new();
    }
}
