using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class ChainLightningAttackAble : AttackAble
{
    [SerializeField]
    private int maxHitCount;
    [SerializeField]
    private float projectileSpeed;
    [SerializeField] int magicPower;
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnEnable()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = Vector3.zero;
    }

    protected override void _AttackStart()
    {
        rb.AddForce(transform.forward * projectileSpeed, ForceMode.Impulse);
    }

    protected override void _AttackStop()
    {
        
    }

    protected override int _GetDamage(GameObject obj)
    {
        if (obj.tag == "EnemyBody" && (!attackedObject.ContainsKey(obj) || attackedObject[obj] < maxHitCount))
        {
            Debug.Log(magicPower);
            return magicPower;
        }
        return 0;
    }

    void ApplyChainDamage(Vector3 center, int radius)
    {
        Collider[] colliders = Physics.OverlapSphere(center, radius);
        foreach (Collider collider in colliders)
        {
            AttackAble attackableComponent = collider.GetComponent<AttackAble>();

            if (attackableComponent != null)
            {
                GameObject newChain = Instantiate(gameObject);
                int damage = attackableComponent.GetDamage(gameObject); 
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        ApplyChainDamage(transform.position, 5);
    }
}