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
    public int stack;
    public string[] etc;

    public ItemInfo(int code, string name, float weight, float volume, int stack, string[] etc) {
        this.code = code;
        this.name = name;
        this.weight = weight;
        this.volume = volume;
        this.stack = stack;
        this.etc = etc;
    }
}

public class LoadItemData : MonoBehaviour
{
    public static LoadItemData instance;
    public TextAsset Data_Items;

    [SerializeField]
    public List<ItemInfo> Data_Item = new List<ItemInfo>();

    private void Awake() {
        if(instance == null)
            instance = this;
        else
            Destroy(this);
    }

    public ItemInfo GetItemData(int code) {
        ItemInfo info = Data_Item.Find(x => x.code == code);
        //Debug.LogWarning("Init Item Data is not implemented");
        return info;
    }

    void Start() {
        LoadData();   
    }

    public void LoadData() {
        string[] line = Data_Items.text.Substring(0, Data_Items.text.Length - 1).Split('\n');
        for (int i = 0; i < line.Length; i++) {
            string[] row = line[i].Split("\t");
            string[] etc = new string[row.Length - 5];
            for (int j = 0; j < row.Length - 5; j++) {
                etc[j] = row[5 + j];
            }
            Data_Item.Add(new ItemInfo(int.Parse(row[0]), row[1], float.Parse(row[2]), float.Parse(row[3]), int.Parse(row[4]), etc));
        }
        print("Load Item Data");
    }

    
}
