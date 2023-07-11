using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Type { helmet, body, bag, pri, sec }
public abstract class Item_Equipment : Item
{
    protected Type type;
    public abstract void Interact_Equip();
    public abstract void Menu_Equip();
    public abstract void Click_Equip();
}
