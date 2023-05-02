using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Cinemachine.DocumentationSortingAttribute;

public class Item_BodyArmor : Item_Armor
{
    public override void Init(int _code) {
        base.Init(_code);
        type = Type.body;
        string[] etc = LoadItemData.instance.GetItemData(_code).etc;
        level = int.Parse(etc[0]);
        maxhealth = float.Parse(etc[1]);
        health = maxhealth;
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
