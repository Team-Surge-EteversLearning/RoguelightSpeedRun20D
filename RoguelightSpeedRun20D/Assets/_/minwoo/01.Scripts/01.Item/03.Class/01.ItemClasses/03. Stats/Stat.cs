using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class Stat : IProduct
{
    public Stat(string name)
    {
        Name = name;
    }

    public string Name { get; set; }
    public void Buy()
    {
    }
}
