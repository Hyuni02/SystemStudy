using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Heal : Item_Useable
{
    [field : SerializeField] public int health_recovery { get;private set; }
    
    public override void Init(ItemInfo_compact item) {
        base.Init(item);
        health_recovery = int.Parse(LoadItemData.instance.GetItemData(item.itemcode).etc[0]);
    }

}
