using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ItemInfo {
    public int code;
    public string name;
    public float weight;
    public float volume;
    public bool stack;

    public ItemInfo(int code, string name, float weight, float volume, bool stack) {
        this.code = code;
        this.name = name;
        this.weight = weight;
        this.volume = volume;
        this.stack = stack;
    }
}

public class LoadItemData : MonoBehaviour
{
    public TextAsset Data_Items;

    [SerializeField]
    public List<ItemInfo> Data_Item = new List<ItemInfo>();

    void Start() {
        string[] line = Data_Items.text.Substring(0, Data_Items.text.Length - 1).Split('\n');
        for (int i = 0; i < line.Length; i++) {
            string[] row = line[i].Split("\t");
            Data_Item.Add(new ItemInfo(int.Parse(row[0]), row[1], float.Parse(row[2]), float.Parse(row[3]), bool.Parse(row[4])));
        }
    }

    
}
