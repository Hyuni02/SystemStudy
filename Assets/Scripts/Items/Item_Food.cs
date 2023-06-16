using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Food : Item_Useable
{
    [field:SerializeField] public int energy_recovery { get; private set; }

    public override void Init(ItemInfo_compact item) {
        base.Init(item);
        energy_recovery = int.Parse(LoadItemData.instance.GetItemData(item.itemcode).etc[0]);
    }
}
