using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class FireBall : ActiveSkill
{
    public FireBall(string name, int mana, float coolTime) : base(name, mana, coolTime)
    {
    }

    protected override void Init()
    {
        base.Init();
    }

    protected override void Use()
    {
        throw new NotImplementedException();
    }
}

