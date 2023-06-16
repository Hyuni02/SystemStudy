using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Item_UpperRail : Item_Parts
{
    public enum Type { _fixed, _dynamic}
    [field : SerializeField] public Type type { get; private set; }

    public override void Init(ItemInfo_compact item) {
        base.Init(item);
        mount = Mount.upper;
        type = (Type)Enum.Parse(typeof(Type), LoadItemData.instance.GetItemData(item.itemcode).etc[0]);
    }

    public void ChangeMode() {

    }
}
