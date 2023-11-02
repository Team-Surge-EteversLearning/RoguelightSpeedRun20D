using Mono.Data.Sqlite;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MySQLTester : MonoBehaviour
{
    SqlAccess sql;

    // Start is called before the first frame update
    void Start()
    {
        sql = SqlAccess.GetAccess(Application.streamingAssetsPath + "/" + "test.db");
        sql.Open();
        sql.SqlRead("SELECT * FROM item JOIN Weapon ON item.name = Weapon.name;");
        
        while (sql.read && sql.dataReader.Read())
        {
            string data = "";
            for (int i = 0; i < sql.dataReader.FieldCount; i++)
            {
                data += sql.dataReader.GetValue(i).ToString() + " "; 
            }
            Debug.Log(data + "\n");
        }
        sql.dataReader.Close(); 
        sql.ShutDown();  
    }
    // Update is called once per frame
    void Update()
    {

    }
}
