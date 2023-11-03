using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class Stat : IProduct
{
    public Stat(string name, int index)
    {
        Name = name;
        StatIndex = index;
    }

    public Stat(string name)
    {
        Name = name;
    }

    public string Name { get; set; }
    protected int statIndex;
    protected int StatIndex { get => statIndex; set => statIndex = value; }


    public virtual void Buy()
    {
        PlayerStatsManager.AddPrice(statIndex);
    }
}
