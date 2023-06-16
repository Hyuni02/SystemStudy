using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item_Armor : Item_Equipment
{
    [field: SerializeField]
    public int level { get; protected set; }
    [field: SerializeField]
    public float maxhealth { get; protected set; }
    [field: SerializeField]
    public float health { get; set; }

    public override void Init(ItemInfo_compact item) {
        base.Init(item);
        string[] etc = LoadItemData.instance.GetItemData(item.itemcode).etc;
        level = int.Parse(etc[0]);
        maxhealth = float.Parse(etc[1]);
        health = item.properties.Find(x => x.str.Equals("health")).it;
    }

    public abstract void Menu_Repair();
}
