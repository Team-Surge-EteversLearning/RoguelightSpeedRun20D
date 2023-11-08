using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerEyeOnlyFindPlayer : MonoBehaviour
{
    [SerializeField]
    private MonsterSM stateManager;

    private void Update()
    {
        //transform.position = stateManager.transform.position;
        //transform.rotation = stateManager.transform.rotation;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PlayerBody")
            ((ITargetCatch)stateManager).TargetChanged(new List<GameObject> { PlayerSM.playerObj });
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "PlayerBody")
            ((ITargetCatch)stateManager).TargetChanged(new List<GameObject> ());
    }
}
