using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.IO;
using System.Xml.Linq;
using UnityEngine;

public static class PlayerSaveManager
{
    private static readonly string originData = Application.streamingAssetsPath + "/" + "PlayerDataOrigin.db";
    private static readonly string savePath = Application.streamingAssetsPath + "/" + "PlayerData.db";

    private static SqlAccess sql;

    public static void SaveData(string name, int cashNow, int hpMax, int staminaMax, int manaMax, int powerWeight, string activeSkill1, string activeSkill2, List<string> itemUnlock)
    {
        if (!File.Exists(savePath))
            File.Copy(originData, savePath, true);


        sql = SqlAccess.GetAccess(savePath);
        sql.Open();
        sql.SqlRead($"SELECT COUNT(name) FROM PlayerData WHERE name = '{name}';");
        if (sql.read && sql.dataReader.Read())
        {
            if (sql.dataReader.GetDecimal(0) > 0)
                sql.SqlExecute(
                    $"UPDATE PlayerData SET " +
                    $"cashNow = {cashNow}, " +
                    $"hpMax = {hpMax}, " +
                    $"staminaMax = {staminaMax}, " +
                    $"manaMax = {manaMax}, " +
                    $"powerWeight = {powerWeight}, " +
                    $"activeSkill1 = '{activeSkill1}', " +
                    $"activeSkill2 = '{activeSkill2}' " +
                    $"WHERE name = '{name}'");
            else
                sql.SqlExecute(
                    $"INSERT INTO PlayerData(name, cashNow, hpMax, staminaMax, manaMax, powerWeight, activeSkill1, activeSkill2) " +
                    $"VALUES ('{name}', {cashNow}, {hpMax}, {staminaMax}, {manaMax}, {powerWeight}, '{activeSkill1}', '{activeSkill2}');");
        }
        else
        {
            Debug.LogError("SqlRead fail");
            return;
        }

        foreach (string unlockName in itemUnlock)
        {
            sql.SqlRead($"SELECT COUNT(name) FROM ItemUnlock WHERE playerName = '{name}' AND name = '{unlockName}';");
            if (sql.read && sql.dataReader.Read())
            {
                if (!(sql.dataReader.GetDecimal(0) > 0))
                    sql.SqlExecute(
                        $"INSERT INTO ItemUnlock(name, playerName) " +
                        $"VALUES ('{unlockName}', '{name}');");
            }
            else
            {
                Debug.LogError("SqlRead fail");
                return;
            }
        }

        sql.ShutDown();
    }

    public static List<string> PlayerList()
    {
        List<string> list = new List<string>();

        if (!File.Exists(savePath))
            return list;
        sql = SqlAccess.GetAccess(savePath);

        sql.SqlRead($"SELECT name FROM PlayerData;");
        if (sql.read)
        {
            while (sql.dataReader.Read())
                list.Add(sql.dataReader.GetString(0));
        }
        else
        {
            Debug.LogError("SqlRead fail");
            return list;
        }

        sql.ShutDown();
        return list;
    }

    public static void LoadData(string name)
    {
        if (!File.Exists(savePath))
            return;

        sql = SqlAccess.GetAccess(savePath);
        sql.Open();
        sql.SqlRead($"SELECT COUNT(name) FROM PlayerData WHERE name = '{name}';");

        Debug.LogWarning($"SELECT COUNT(name) FROM PlayerData WHERE name = '{name}';");

        if (sql.read && sql.dataReader.Read() && sql.dataReader.GetDecimal(0) > 0)
        {
            sql.SqlRead($"SELECT name, cashNow, hpMax, staminaMax, manaMax, powerWeight, activeSkill1, activeSkill2 FROM PlayerData WHERE name = '{name}';");
            if (sql.read && sql.dataReader.Read())
            {
                PlayerStatsManager.Set(
                    sql.dataReader.GetString(0),
                    (int)sql.dataReader.GetDecimal(1),
                    (int)sql.dataReader.GetDecimal(2),
                    (int)sql.dataReader.GetDecimal(3),
                    (int)sql.dataReader.GetDecimal(4),
                    (int)sql.dataReader.GetDecimal(5));

                PlayerSM.skill1Index = sql.dataReader.GetString(6);
                PlayerSM.skill2Index = sql.dataReader.GetString(7);
            }
        }
        else
        {
            Debug.LogError("SqlRead fail");
            return;
        }

        List<string> itemUnlock = new List<string>();
        sql.SqlRead($"SELECT COUNT(name) FROM ItemUnlock WHERE playerName = '{name}';");
        if (sql.read && sql.dataReader.Read())
        {
            int count = (int)sql.dataReader.GetDecimal(0);
            for (int i = 0; i < count; i++)
            {
                sql.SqlRead($"SELECT name FROM ItemUnlock WHERE playerName = '{name}';");

                if (sql.read && sql.dataReader.Read())
                    itemUnlock.Add(sql.dataReader.GetString(0));
                else
                {
                    Debug.LogError("SqlRead fail");
                    return;
                }
            }
        }
        sql.ShutDown();

        EquipmentDataManager.Load(itemUnlock);
        SkillDataModel.Load(itemUnlock);
    }
    public static List<string> WrappingUnlocks()
    {
        List<string> list = new List<string>();
        foreach (var item in EquipmentDataManager.unlocks.Keys)
        {
            list.Add(item);
        }
        foreach (var item in SkillDataModel.UnlockActive.Keys)
        {
            list.Add(item);
        }
        return list;
    }
}
