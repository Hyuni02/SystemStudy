using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Item_Throw : Item
{
    public enum Type { grenade, flashbang, molotov, emp}
    [field:SerializeField] public Type type;

    public override void Init(ItemInfo_compact item) {
        base.Init(item);
        type = (Type) Enum.Parse(typeof(Type), LoadItemData.instance.GetItemData(item.itemcode).etc[0]);
    }

    public void Action_Throw() {

    }
}
