using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Item_Throw : Item
{
    public enum Type { grenade, flashbang, molotov, emp}
    [field:SerializeField] public Type type;

    public override void Init(int _code) {
        base.Init(_code);
        type = (Type) Enum.Parse(typeof(Type), LoadItemData.instance.GetItemData(_code).etc[0]);
    }

    public void Action_Throw() {

    }
}
