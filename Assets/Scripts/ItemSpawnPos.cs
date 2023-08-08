using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;

public enum SpawnItemType { normal, rare, ammo, food, medical, weapon, parts, custom}

public class ItemSpawnPos : MonoBehaviour
{
    public SpawnItemType spawnItemType = SpawnItemType.normal;
    [Header("Custom List")]
    public List<int> ints = new List<int>();
    
    public void SpawnItem() {
        Debug.LogWarning($"아이템 스폰 미구현");
    }
}
