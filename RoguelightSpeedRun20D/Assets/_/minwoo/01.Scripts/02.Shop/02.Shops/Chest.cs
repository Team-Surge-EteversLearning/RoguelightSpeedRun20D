using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    [SerializeField] int minTier;
    [SerializeField] int maxTier;
    private void Start()
    {
        DungeonShopManager.Instance.CreateChestShop(gameObject, minTier, maxTier, 1);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
            DungeonShopManager.Instance.ResetTargetShops(gameObject);
    }
}
