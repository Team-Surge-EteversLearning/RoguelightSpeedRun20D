using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class MagicAttackable : AttackAble
{
    [SerializeField]
    private int maxHitCount;
    [SerializeField]
    private float projectileSpeed;
    [SerializeField] int magicFower;
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
            Destroy(gameObject);
            Debug.Log(magicFower);
            return magicFower;
        }
        return 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(!other.CompareTag("PlayerBody"))
            Destroy(gameObject);
    }
}
