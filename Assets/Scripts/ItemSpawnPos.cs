using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEditor.Rendering;
using UnityEngine;

public enum SpawnItemType { normal, rare, ammo, food, medical, weapon, parts, custome}

public class ItemSpawnPos : MonoBehaviour
{
    public SpawnItemType spawnItemType = SpawnItemType.normal;
    [Header("Custom List")]
    public string custome;
    
    public void SpawnItem() {
        string[] itempool;
        if (spawnItemType != SpawnItemType.custome) {
            itempool = MapDataLoader.Instance.Get_ItemList(spawnItemType.ToString());
        }
        else {
            itempool = MapDataLoader.Instance.Get_ItemList(custome);
        }

        //�������� �ڵ� �ϳ� �̱�
        int target = int.Parse(itempool[Random.Range(0, itempool.Length)]);

        //�ڵ忡 �ش��ϴ� ������ ������ �����ϱ�
        GameObject prefab = (GameObject)Resources.Load($"ItemPrefab/{target}");
        if(prefab == null ) {
            Debug.LogError($"{target} ������ ����");
        }
        GameObject item = Instantiate(prefab, transform.position, transform.rotation);
        item.GetComponent<Item>().Init(new ItemInfo_compact { itemcode = target, itemcount = 1 });

        Debug.LogWarning($"{target} ������ �Ӽ� ���� �̱���");
    }
}
