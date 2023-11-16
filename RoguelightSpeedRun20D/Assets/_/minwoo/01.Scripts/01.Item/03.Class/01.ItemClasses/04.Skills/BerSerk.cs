using System.Threading.Tasks;

public class BerSerk : ActiveSkill
{
    public BerSerk(string name, int mana, float coolTime) : base(name, mana, coolTime)
    {
        SkillDecription += " : I'm angry";
    }

    protected override void Init()
    {
        base.Init();
    }

    public override void _Use()
    {
        PlayerStatsManager.PowerWeight *= 2;
        PlayerSM.hpNow = (int)(PlayerSM.hpNow * 0.7f);

        DelayedResetWeight(2000);
    }

    private async void DelayedResetWeight(int delayMilliseconds)
    {
        await Task.Delay(delayMilliseconds);
        ResetWeight();
    }

    private void ResetWeight()
    {
        PlayerStatsManager.PowerWeight /= 2;
    }
}
