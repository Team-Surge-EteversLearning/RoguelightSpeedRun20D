using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeForTesting : MonoBehaviour
{
    [SerializeField]
    private MonsterSM stateManager;

    private void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PlayerBody")
            ((ITargetCatch)stateManager).TargetChanged(new List<GameObject> { other.gameObject });
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "PlayerBody")
            ((ITargetCatch)stateManager).TargetChanged(new List<GameObject>());
    }
}