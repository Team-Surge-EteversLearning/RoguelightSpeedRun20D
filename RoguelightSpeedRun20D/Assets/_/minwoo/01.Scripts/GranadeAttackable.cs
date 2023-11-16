using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GranadeAttackable : AttackAble
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
        //rb = GetComponent<Rigidbody>();
        //rb.velocity = Vector3.zero;
    }

    protected override void _AttackStart()
    {
        rb.velocity = Vector3.zero;
        Vector3 diagonalForce = transform.forward + transform.up; // add forward, up
        diagonalForce.Normalize(); 
        rb.AddForce(diagonalForce * projectileSpeed, ForceMode.Impulse);
    }

    protected override void _AttackStop()
    {

    }

    protected override int _GetDamage(GameObject obj)
    {
        //Debug.LogWarning(obj.name);
        if ((!attackedObject.ContainsKey(obj) || attackedObject[obj] < maxHitCount))
        {
            Destroy(gameObject, 2f);
            Debug.Log(magicFower);
            return magicFower;
        }
        return 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        attackTrigger.enabled = true;
        //Destroy(gameObject);
    }
}
