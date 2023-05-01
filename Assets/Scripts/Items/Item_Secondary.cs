using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Item_Secondary : Item_Weapon
{
    public override void Init(int _code) {
        base.Init(_code);
        type = Type.sec;
    }

    public void Draw_Pri() {

    }
    public void Draw_Sec() {

    }

    public override void Aim() {
        throw new System.NotImplementedException();
    }

    public override void Click_Equip() {
        throw new System.NotImplementedException();
    }

    public override void Fire() {
        throw new System.NotImplementedException();
    }

    public override void Interact_Equip() {
        throw new System.NotImplementedException();
    }

    public override void Menu_Equip() {
        throw new System.NotImplementedException();
    }

    public override void Reload() {
        throw new System.NotImplementedException();
    }
}
