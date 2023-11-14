using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    [SerializeField] private Animator animator;
    
    [SerializeField] int minTier;
    [SerializeField] int maxTier;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        DungeonShopManager.Instance.CreateChestShop(gameObject, minTier, maxTier, 1);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerBody"))
        {
            DungeonShopManager.Instance.ResetTargetShops(gameObject);
            animator.Play("OpenAnimation");
        }
    }
}
