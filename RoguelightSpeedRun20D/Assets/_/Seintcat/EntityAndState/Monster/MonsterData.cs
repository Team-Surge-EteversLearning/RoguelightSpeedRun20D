using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "", fileName = "")]
public class MonsterData : ScriptableObject
{
    [Header("Model")]
    public GameObject prefab;
    [Header("Stats")]
    public string monsterName;
    public int cashDrop;
    public int hpMax;
    [Header("Attack")]
    public int attackPower;
    public float motionTime;
    public float attackCoolTime;
    public float attackRange;
    [Header("Patrol")]
    public float idleTime;
    public float moveSpeed;
    public float chaseSpeed;
    public bool isFly;
    public PatrolMode patrolMode;
    [Header("Damage")]
    public float damagedStaggerTime;
    public float deadTime;
}

public enum PatrolMode
{
    Line,
    Circle,
    Random
}
