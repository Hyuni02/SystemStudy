using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item_Weapon : Item_Equipment
{
    public abstract void Fire();
    public abstract void Aim();
    public abstract void Reload();
}
