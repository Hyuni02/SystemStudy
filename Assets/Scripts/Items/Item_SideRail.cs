using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Item_SideRail : Item_Parts
{
    public enum Modes { light, razor, both}
    [field : SerializeField] public Modes mode {  get; private set; }

    public override void Init(ItemInfo_compact item) {
        base.Init(item);
        mount = Mount.side;
        mode = (Modes)Enum.Parse(typeof(Modes), LoadItemData.instance.GetItemData(item.itemcode).etc[0]);
    }

    public void ChangeMode() {

    }
}
