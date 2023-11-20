using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public class InDungeonShopMB : MonoBehaviour
{

    [SerializeField] int minTier;
    [SerializeField] int maxTier;
    [Header("The total number of all current slots must not exceed 10.")]
    [SerializeField] int equipeAmount;
    [SerializeField] string hpPotionAmount;
    [SerializeField] string mpPotionAmount;
    [SerializeField] float bombProbability;
    [SerializeField] float barrierProbability;

    private int bombAmount;
    private int barrierAmount;

    private void Start()
    {
        bombAmount = Random.value < bombProbability ? 1 : 0;
        barrierAmount = Random.value < barrierProbability ? 1 : 0;
        //Debug.Log($"{barrierAmount} / {barrierAmount}");
        DungeonShopManager.Instance.CreateInDungeonShop(gameObject, minTier, maxTier, equipeAmount, $"{hpPotionAmount},{mpPotionAmount},{bombAmount},{barrierAmount},d");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerBody"))
        {
            DungeonShopManager.CursorToggle(true);
            DungeonShopManager.Instance.ResetTargetShops(gameObject);
        }
    }
}
