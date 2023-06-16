using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_UnderRail : Item_Parts
{
    [field:SerializeField] public int recoil_reduction { get; private set; }
    [field:SerializeField] public int aim_increase {  get; private set; }

    public override void Init(ItemInfo_compact item) {
        base.Init(item);
        mount = Mount.under;
        string[] etc = LoadItemData.instance.GetItemData(item.itemcode).etc;
        recoil_reduction = int.Parse(etc[0]);
        aim_increase = int.Parse(etc[1]);
    }
}
