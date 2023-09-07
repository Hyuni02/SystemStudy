using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapDataLoader : MonoBehaviour
{
    public static MapDataLoader Instance;

    List<Dictionary<string, object>> MapData = new List<Dictionary<string, object>>();
    List<GameObject> List_ItemSpawnPos = new List<GameObject>();

    private void Awake() {
        Instance = this;
    }
    private void Start() {
        MapData = CSVReader.Read($"MapData/{RaidInfo.instance.MapLevel}");
        if ( MapData == null) {
            Debug.LogWarning($"MapData/{RaidInfo.instance.MapLevel} 데이터가 존재하지 않음");
            MapData = CSVReader.Read($"MapData/TempMapData");
        }
        foreach (var i in GameObject.FindGameObjectsWithTag("ItemSpawnPos")) {
            List_ItemSpawnPos.Add(i);
            i.GetComponent<ItemSpawnPos>().SpawnItem();
        }
    }

    public string[] Get_ItemList(string type) {
        foreach(var i in MapData) {
            if (i["name"].Equals(type)) {
                return i["list"].ToString().Split('/');
            }
        }
        Debug.LogError($"{type} : 데이터 없음");
        return null;
    }
}
