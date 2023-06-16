using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Bag : Item_Equipment
{
    [field: SerializeField]
    public float additional_volume { get; private set; }

    public override void Init(ItemInfo_compact item) {
        base.Init(item);
        type = Type.bag;
        additional_volume = float.Parse(LoadItemData.instance.GetItemData(item.itemcode).etc[0]);
    }

    public override void Click_Equip() {
        throw new System.NotImplementedException();
    }
    public override void Interact_Equip() {
        throw new System.NotImplementedException();
    }
    public override void Menu_Equip() {
        throw new System.NotImplementedException();
    }
}
