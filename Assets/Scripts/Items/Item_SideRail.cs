using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Item_SideRail : Item_Parts
{
    public enum Modes { light, razor, both}
    [field : SerializeField] public Modes mode {  get; private set; }

    public override void Init(int _code) {
        base.Init(_code);
        mount = Mount.side;
        mode = (Modes)Enum.Parse(typeof(Modes), LoadItemData.instance.GetItemData(_code).etc[0]);
    }

    public void ChangeMode() {

    }
}
