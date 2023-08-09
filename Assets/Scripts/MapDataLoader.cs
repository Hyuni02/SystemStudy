using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapDataLoader : MonoBehaviour
{
    public static MapDataLoader Instance;

    List<Dictionary<string, object>> MapData = new List<Dictionary<string, object>>();

    private void Awake() {
        Instance = this;
    }
    private void Start() {
        MapData = CSVReader.Read($"MapData/{RaidInfo.instance.MapCode}");
        if ( MapData == null) {
            Debug.LogWarning($"MapData/{RaidInfo.instance.MapCode} 데이터가 존재하지 않음");
        }
            
    }
}
