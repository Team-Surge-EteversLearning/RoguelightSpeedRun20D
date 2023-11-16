using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerSM : StateManager
{
    public static readonly float moveSpeed = 2f; // 0.2
    public static readonly float runSpeed = 3.5f; // 0.35
    public static readonly float speedMaxGap = 0.5f;
    public static readonly float powerWeight = 0.5f;
    public static readonly float powerWeightGap = 1f;

    public static GameObject playerObj;

    [SerializeField]
    private List<GameObject> weaponInstance;
    [SerializeField]
    private List<GameObject> armorInstance;
    [SerializeField]
    private List<GameObject> shoesInstance;
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private Animator shieldAnimator;
    [SerializeField]
    private GameObject bomb;

    private GameObject weaponModelNow;
    private AttackAble attackable;

    private static Weapon _weaponNow;
    public static Weapon weaponNow
    {
        get { return _weaponNow; }
        set
        {
            if (playerObj != null)
                playerObj.GetComponent<PlayerSM>().weaponInstance[_weaponNow.ModelIndex].SetActive(false);

            _weaponNow = value;

            if (playerObj != null)
            {
                GameObject weaponModel = _weaponNow.MakeInGame(playerObj.GetComponent<PlayerSM>().weaponInstance);
                weaponModel.SetActive(true);
            }
        }
    }
    private static Armor _armorNow;
    public static Armor armorNow
    {
        get { return _armorNow; }
        set
        {
            if (playerObj != null)
                playerObj.GetComponent<PlayerSM>().armorInstance[armorNow.ModelIndex].SetActive(false);

            _armorNow = value;

            if (playerObj != null)
            {
                GameObject armorModel = _armorNow.MakeInGame(playerObj.GetComponent<PlayerSM>().armorInstance);
                armorModel.SetActive(true);
            }
        }
    }
    private static Shoes _shoesNow;
    public static Shoes shoesNow
    {
        get { return _shoesNow; }
        set
        {
            if (playerObj != null)
                playerObj.GetComponent<PlayerSM>().shoesInstance[shoesNow.ModelIndex].SetActive(false);

            _shoesNow = value;

            if (playerObj != null)
            {
                GameObject shoesModel = _shoesNow.MakeInGame(playerObj.GetComponent<PlayerSM>().shoesInstance);
                shoesModel.SetActive(true);
            }
        }
    }

    public static Weapon basicWeapon;
    public static Armor basicArmor;
    public static Shoes basicShoes;

    public static int hpNow { get; set; }
    public static float staminaNow { get; set; }
    public static float manaNow { get; set; }
    public static int hpMax => PlayerStatsManager.HpMax + armorNow.MaxHp + shoesNow.MaxHp;
    public static float staminaMax => PlayerStatsManager.StaminaMax + shoesNow.MaxStamina;
    public static int manaMax => PlayerStatsManager.ManaMax + armorNow.MaxMana;

    public static float attackCooltime;

    public static bool isDefence { get; private set; }
    public static string skill1Index { get; set; }
    public static string skill2Index { get; set; }

    public static float skill1CoolTime;
    public static float skill2CoolTime;

    public Transform skillTransform;
    public List<GameObject> skillPrefabs;

    public static int barrierNow;

    private void Awake()
    {
        ManagerStart();
        playerObj = gameObject;
        weaponModelNow = weaponInstance[0];
        attackable = weaponModelNow.GetComponent<AttackAble>();

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
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

        if (InputHandler.skill1 && skill1Index != null && skill1Index != "" && skill1CoolTime < 0)
            SkillDataModel.UnlockActive[skill1Index].Use(true);
        if (InputHandler.skill2 && skill2Index != null && skill2Index != "" && skill2CoolTime < 0)
            SkillDataModel.UnlockActive[skill2Index].Use(false);

        skill1CoolTime -= Time.deltaTime;
        skill2CoolTime -= Time.deltaTime;

        Vector3 rotation = transform.rotation.eulerAngles;
        rotation.y = rotation.y + (InputHandler.camRotate * Time.deltaTime * 30);
        //Debug.LogWarning(rotation);
        transform.rotation = Quaternion.Euler(rotation);

        manaNow += armorNow.ManaRegen * Time.deltaTime;
        if (!(mainState.stateName == "Move" && InputHandler.running) && !(mainState.stateName != "Move" && isDefence))
            staminaNow += (shoesNow.StaminaRegen + 1) * Time.deltaTime;

        if (manaNow > manaMax)
            manaNow = manaMax;
        if (staminaNow > staminaMax)
            staminaNow = staminaMax;

        attackCooltime -= Time.deltaTime;
        if (isDefence && !InputHandler.defence)
        {
            isDefence = false;
            gameObject.GetComponent<Animator>().Play("Idle", 1, 0f);
        }

        BattleUI.hpBar = ((float)hpNow / hpMax);
        BattleUI.mpBar = ((float)manaNow / manaMax);
        BattleUI.stBar = ((float)staminaNow / staminaMax);

        if (InputHandler.item1 && DungeonItemManager.hpPotionNow > 0 && hpNow < hpMax)
        {
            DungeonItemManager.hpPotionNow--;
            hpNow += (hpMax / 2);
            if(hpNow > hpMax)
                hpNow = hpMax;
        }
        if (InputHandler.item2 && DungeonItemManager.manaPotionNow > 0 && manaNow < manaMax)
        {
            DungeonItemManager.manaPotionNow--;
            manaNow = manaMax;
        }
        if (InputHandler.item3 && DungeonItemManager.bombNow > 0)
        {
            DungeonItemManager.bombNow--;
            AttackAble bombNow = Instantiate(bomb).GetComponent<AttackAble>();
            bombNow.transform.position = skillTransform.position;
            bombNow.transform.rotation = skillTransform.rotation;
            bombNow.AttackStart();
        }
        if (InputHandler.item4 && DungeonItemManager.barrierNow > 0)
        {
            DungeonItemManager.barrierNow--;

            if(barrierNow == 0)
                shieldAnimator.Play("ShieldOn");

            barrierNow += 3;
        }


        if (mainState.stateName != "Avoid")
        {
            if (InputHandler.avoid && staminaNow > 3f)
            {
                attackable.AttackStop();
                ChangeState("Avoid");
                return;
            }

            if (!animator.GetCurrentAnimatorStateInfo(1).IsName("Attack") && InputHandler.defence)
            {
                isDefence = true;
                animator.Play("Defence", 1, 0f);
                return;
            }

            if (attackCooltime < 0 && !isDefence)
            {
                if (InputHandler.attack)
                {
                    animator.Play("Attack", 1, 0f);
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
        if (attack == null || mainState.stateName == "Avoid")
            return;

        int damage = attack.GetDamage(gameObject);
        if (damage < 1)
            return;

        if(barrierNow > 0)
        {
            barrierNow--;

            if (barrierNow == 0)
                shieldAnimator.Play("ShieldOff");
            return;
        }

        if (isDefence && (damage / 2) < staminaNow)
        {
            staminaNow -= (int)((float)damage / 2);

            if (QuestSystem.currentQuests != null)
                foreach (Quest quest in QuestSystem.currentQuests)
                    if (quest.Key == "Guard")
                        ((SuccessGuardQuest)quest).UpdateCurrentCount(damage);

            if (staminaNow < 0)
            {
                staminaNow = 0;
            }

            return;
        }

        hpNow -= damage;
        if (hpNow < 0)
        {
            animator.Play("Death", 0);
            animator.Play("Death", 1);
            enabled = false;
        }
        else
        {
            ChangeState("Damage");

            foreach (EquipmentOption option in armorNow.usableOptions)
                option.UseOption();
        }

    }

    public static void ResetAfterGameOver()
    {
        if(basicWeapon == null || basicArmor == null || basicShoes == null)
        {
            basicWeapon = new Weapon("BasicWeapon", new BasicEquipments(0, 0, 0, EquipmentType.Weapon, 0), new WeaponData(1000, false, 1, 1.5f), new List<EquipmentOption>());
            basicArmor = new Armor("BasicArmor", new BasicEquipments(0, 0, 0, EquipmentType.Armor, 0), new ArmorData(9999, false, 0, 0), new List<EquipmentOption>());
            basicShoes = new Shoes("BasicShoes", new BasicEquipments(0, 0, 0, EquipmentType.Shoes, 0), new ShoesData(0, 0, 0, 0), new List<EquipmentOption>());
        }

        _weaponNow = basicWeapon;
        _armorNow = basicArmor;
        _shoesNow = basicShoes;

        attackCooltime = 0;

        hpNow = hpMax;
        staminaNow = staminaMax;
        manaNow = manaMax;
    }
}
