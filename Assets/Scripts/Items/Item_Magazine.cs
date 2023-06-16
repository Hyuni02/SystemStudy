using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using static Item_Ammo;
using UnityEditor.UIElements;

public class Item_Magazine : Item_Parts
{
    [field: SerializeField] public Item_Ammo.Gauge gauge { get; private set; }
    [field : SerializeField] public int capacity { get; private set; }
    [field : SerializeField] public int remains { get; private set; }
    [field : SerializeField] public int reload_reduction { get; private set; }
    [field : SerializeField] public int aim_decrease { get; private set; }

    public override void Init(ItemInfo_compact item) {
        base.Init(item);
        mount = Mount.mag;  
        string[] etc = LoadItemData.instance.GetItemData(item.itemcode).etc;
        capacity = int.Parse(etc[0]);
        remains = item.properties[0].it; //인벤토리 데이터 불러오기
        gauge = (Gauge)Enum.Parse(typeof(Gauge), LoadItemData.instance.GetItemData(item.itemcode).etc[1]);
        reload_reduction= int.Parse(etc[2]);
        aim_decrease = int.Parse(etc[3]);
    }

    public void Insert_Ammo() {

    }
    public void Eject_Ammo() {

    }
}
