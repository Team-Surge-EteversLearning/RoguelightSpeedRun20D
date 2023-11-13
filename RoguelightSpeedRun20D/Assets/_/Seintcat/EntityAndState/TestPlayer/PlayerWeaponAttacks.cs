using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponAttacks : AttackAble
{
    public GameObject projectile;
    public Transform projectileSpawnPoint;

    private void Awake()
    {
        attackTrigger.enabled = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    protected override void _AttackStart()
    {
        if (projectile != null)
        {
            GameObject _projectile = Instantiate(projectile);
            _projectile.transform.position = projectileSpawnPoint.position;

            AttackAble attackAble = _projectile.GetComponent<AttackAble>();
            if(attackAble != null)
            {
                attackAble.AttackStart();
            }
        }

        foreach (EquipmentOption option in PlayerSM.weaponNow.usableOptions)
            option.UseOption();
    }

    protected override void _AttackStop()
    {

    }

    protected override int _GetDamage(GameObject obj)
    {
        float powerWeight = (float)PlayerStatsManager.PowerWeight / (PlayerStatsManager.PowerWeight + 10);
        return (int)((0.5f + powerWeight) * PlayerSM.weaponNow.Damage);
    }
}
