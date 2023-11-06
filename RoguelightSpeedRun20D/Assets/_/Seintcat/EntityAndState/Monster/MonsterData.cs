using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "", fileName = "")]
public class MonsterData : ScriptableObject
{
    [Header("Stats")]
    public string monsterName;
    public int cashDrop;
    public int hpMax;
    public int attackPower;
    public float attackSpeed;
    public float moveSpeed;
    [Header("Model")]
    public GameObject prefab; 
}
