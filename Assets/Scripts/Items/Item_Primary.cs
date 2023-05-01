using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Item_Primary : Item_Weapon
{
    public override void Init(int _code) {
        base.Init(_code);
        type = Type.pri;
    }

    public void Draw_QuickSec() {

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
