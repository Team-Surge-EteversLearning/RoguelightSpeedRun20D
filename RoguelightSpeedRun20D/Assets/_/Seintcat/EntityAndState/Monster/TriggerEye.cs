using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerEye : MonoBehaviour
{
    [SerializeField]
    private StateManager stateManager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PlayerBody")
            stateManager.Interrupt("EyeEnter");
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "PlayerBody")
            stateManager.Interrupt("EyeExit");
    }
}
