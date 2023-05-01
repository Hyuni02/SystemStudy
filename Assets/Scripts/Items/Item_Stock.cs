using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Stock : Item_Parts
{
    [field: SerializeField] public int recoil_reduction { get; private set; }
    [field: SerializeField] public int aim_increase { get; private set; }

    public override void Init(int _code) {
        base.Init(_code);
        mount = Mount.stock;
        string[] etc = LoadItemData.instance.GetItemData(_code).etc;
        recoil_reduction = int.Parse(etc[0]);   
        aim_increase = int.Parse(etc[1]);
    }
}
