using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Item_Ammo : Item
{
    public enum Gauge { acp, para, nato5, nato7, shell }
    [field: SerializeField]
    public Gauge gauge { get; private set; }

    public override void Init(ItemInfo_compact item) {
        base.Init(item);
        gauge = (Gauge)Enum.Parse(typeof(Gauge), LoadItemData.instance.GetItemData(item.itemcode).etc[0]);
    }
}
