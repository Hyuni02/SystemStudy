using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Muzzle : Item_Parts
{
    public enum Type { comp, silen}
    [field : SerializeField] public Type type { get; protected set; }
    [SerializeField] protected float recoil_reduction;

    public override void Init(ItemInfo_compact item) {
        base.Init(item);
        mount = Mount.muzzle;
        string[] etc = LoadItemData.instance.GetItemData(item.itemcode).etc;
        type = (Type)Enum.Parse(typeof(Type), etc[0]);
        recoil_reduction = float.Parse(etc[1]);
    }

        
}
