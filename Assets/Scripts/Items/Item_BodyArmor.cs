using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Cinemachine.DocumentationSortingAttribute;

public class Item_BodyArmor : Item_Armor
{
    public override void Init(ItemInfo_compact item) {
        base.Init(item);
        type = Type.body;
    }

    public override void Click_Equip() {
        throw new System.NotImplementedException();
    }
    public override void Menu_Equip() {
        throw new System.NotImplementedException();
    }
    public override void Menu_Repair() {
        throw new System.NotImplementedException();
    }
    public override void Interact_Equip() {
        throw new System.NotImplementedException();
    }
}
