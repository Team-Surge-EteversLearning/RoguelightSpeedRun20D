using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class Useable : IProduct
{
    public Useable(int itemCode)
    {
        ItemCode = itemCode;
    }

    public int ItemCode { get; set; }
    public void Buy()
    {
    }
}

