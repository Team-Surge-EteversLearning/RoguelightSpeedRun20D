using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOutfitSelecter : MonoBehaviour
{
    public static PlayerOutfitSelecter Instance {  get; private set; }
    [SerializeField]
    private List<GameObject> weaponInstance;
    [SerializeField]
    private List<GameObject> armorInstance;

    private int _weaponNow = 0;
    public  Weapon weaponNow
    {
        set
        {
            if (_weaponNow != value.ModelIndex)
                weaponInstance[_weaponNow].SetActive(false);

            _weaponNow = value.ModelIndex;

            GameObject weaponModel = value.MakeInGame(weaponInstance);
            weaponModel.SetActive(true);
        }
    }
    private int _armorNow = 0;
    public Armor armorNow
    {
        set
        {
            if (_armorNow != value.ModelIndex)
                armorInstance[_armorNow].SetActive(false);

            _armorNow = value.ModelIndex;

            GameObject armorModel = value.MakeInGame(armorInstance);
            armorModel.SetActive(true);
        }
    }

    private void Awake()
    {
        if(Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        weaponInstance[0].SetActive(true);
        armorInstance[0].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
