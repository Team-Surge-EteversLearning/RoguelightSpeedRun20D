using System.Threading.Tasks;
using UnityEngine;

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
        PlayerSM playerSM = PlayerSM.playerObj.GetComponent<PlayerSM>();
        GameObject aura = playerSM.skillPrefabs[4];
        GameObject instantiatedObject = UnityEngine.Object.Instantiate(aura, playerSM.skillTransform.position, Quaternion.identity);

        instantiatedObject.transform.parent = playerSM.transform;

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
