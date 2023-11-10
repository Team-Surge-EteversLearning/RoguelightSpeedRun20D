using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerSM : StateManager
{
    public static readonly float moveSpeed = 0.2f;
    public static readonly float runSpeed = 0.35f;
    public static readonly float speedMaxGap = 0.5f;
    public static readonly float powerWeight = 0.5f;
    public static readonly float powerWeightGap = 1f;

    public static GameObject playerObj;

    [SerializeField]
    private GameObject weaponHanger;
    [SerializeField]
    private List<GameObject> weaponModels;
    [SerializeField]
    private Animator animator;

    private List<GameObject> weaponInstance;
    private GameObject weaponModelNow;
    private AttackAble attackable;

    public static Weapon weaponNow { get; set; }
    public static Armor armorNow { get; set; }
    public static Shoes shoesNow { get; set; }

    public static Weapon basicWeapon;
    public static Armor basicArmor;
    public static Shoes basicShoes;

    public static int hpNow { get; private set; }
    public static float staminaNow { get; set; }
    public static float manaNow { get; private set; }
    public static int hpMax => PlayerStatsManager.HpMax + armorNow.MaxHp + shoesNow.MaxHp;
    public static float staminaMax => PlayerStatsManager.StaminaMax + shoesNow.MaxStamina;
    public static int manaMax => PlayerStatsManager.ManaMax + armorNow.MaxMana;

    public static float attackCooltime;

    private void Awake()
    {
        ManagerStart();
        playerObj = gameObject;
        weaponModelNow = weaponModels[0];
        attackable = weaponModelNow.GetComponent<AttackAble>();
    }
    public override void MakeState()
    {
        State state;
        allStates = new Dictionary<string, State>();

        state = new PlayerState_Idle();
        allStates.Add(state.stateName, state);
        state = new PlayerState_Move();
        allStates.Add(state.stateName, state);
        state = new PlayerState_Damage();
        allStates.Add(state.stateName, state);
        state = new PlayerState_Avoid();
        allStates.Add(state.stateName, state);

        mainState = allStates["Idle"];
    }

    // Start is called before the first frame update
    void Start()
    {
        ResetAfterGameOver();
    }

    // Update is called once per frame
    void Update()
    {
        ManagerUpdate();

        manaNow += armorNow.ManaRegen * Time.deltaTime;
        if (mainState.stateName != "Move" || !InputHandler.running)
            staminaNow += (shoesNow.StaminaRegen + 1) * Time.deltaTime;

        if (manaNow > manaMax)
            manaNow = manaMax;
        if (staminaNow > staminaMax)
            staminaNow = staminaMax;

        if(mainState.stateName != "Avoid")
        {
            if (InputHandler.avoid && staminaNow > 3f)
            {
                attackable.AttackStop();
                ChangeState("Avoid");
                return;
            }

            attackCooltime -= Time.deltaTime;
            if (attackCooltime < 0)
            {
                if (InputHandler.attack)
                {
                    gameObject.GetComponent<Animator>().Play("Attack", 1, 0f);
                    attackCooltime = weaponNow.Cooltime;
                    attackable.AttackStart();
                }
                else
                    attackable.AttackStop();
            }
        }
    }

    public override void Interrupt(string stateName)
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        AttackAble attack = other.gameObject.GetComponent<AttackAble>();
        if (mainState.stateName != "Avoid" && attack != null)
        {
            int damage = attack.GetDamage(gameObject);
            if (damage > 1)
                return;

            hpNow -= damage;
            if (hpNow < 0)
            {
                animator.Play("Death", 0);
                animator.Play("Death", 1);
                enabled = false;
            }
            else
                ChangeState("Damage");
        }
    }

    public static void ResetAfterGameOver()
    {
        if(basicWeapon == null || basicArmor == null || basicShoes == null)
        {
            basicWeapon = new Weapon("BasicWeapon", new BasicEquipments(0, 0, 0, EquipmentType.Weapon, 0), new WeaponData(10, false, 1, 1.5f));
            basicArmor = new Armor("BasicArmor", new BasicEquipments(0, 0, 0, EquipmentType.Armor, 0), new ArmorData(0, false, 0, 0));
            basicShoes = new Shoes("BasicShoes", new BasicEquipments(0, 0, 0, EquipmentType.Shoes, 0), new ShoesData(0, 0, 0, 0));
        }

        weaponNow = basicWeapon;
        armorNow = basicArmor;
        shoesNow = basicShoes;

        attackCooltime = 0;

        hpNow = hpMax;
        staminaNow = staminaMax;
        manaNow = manaMax;
    }
}
