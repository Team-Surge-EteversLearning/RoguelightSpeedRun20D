using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyDBInitializerTester : MonoBehaviour
{
    private void Awake()
    {
        EquipmentDataManager eqdb = new EquipmentDataManager();
        eqdb.Init();
    }
}
