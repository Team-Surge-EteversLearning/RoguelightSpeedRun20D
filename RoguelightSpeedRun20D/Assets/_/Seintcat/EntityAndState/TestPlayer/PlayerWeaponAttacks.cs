using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponAttacks : AttackAble
{
    public int damagePoint { private get; set; }

    public GameObject projectile;
    public Transform projectileSpawnPoint;

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
    }

    protected override void _AttackStop()
    {

    }

    protected override int _GetDamage(GameObject obj)
    {
        //Debug.LogWarning(PlayerStatsManager.PowerWeight);
        return (int)(PlayerStatsManager.PowerWeight * PlayerSM.weaponNow.Damage);
    }
}
