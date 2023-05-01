using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item_Armor : Item_Equipment
{
    [field: SerializeField]
    public int level { get; protected set; }
    [field: SerializeField]
    public float maxhealth { get; protected set; }
    [field: SerializeField]
    public float health { get; set; }

    public abstract void Menu_Repair();
}
