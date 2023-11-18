using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class GranadeAttackable : AttackAble
{
    [SerializeField]
    private int maxHitCount;
    [SerializeField]
    private float projectileSpeed;
    [SerializeField] private int magicPower;
    private Rigidbody rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {
        AttackStart();
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
        if (obj.gameObject.CompareTag("PlayerBody"))
            return 0;
        //Debug.LogWarning(obj.name);
        if ((!attackedObject.ContainsKey(obj) || attackedObject[obj] < maxHitCount))
        {
            Destroy(gameObject, 2f);
            Debug.Log(magicPower);
            return magicPower;
        }
        return 0;
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.layer != 6)
        {
            Debug.LogWarning(other.transform.name);
            attackTrigger.enabled = true;
            GetComponentInChildren<Light>().enabled = false;
        }
        else
        {
            Debug.LogWarning("Player!");
        }    
        //Destroy(gameObject);
    }
}
