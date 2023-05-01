using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item_Parts : Item
{
    public enum Mount { muzzle, upper, side, under, mag, stock}
    [field: SerializeField] public Mount mount { get; protected set; }

    public virtual void Show_Can_Mount() {

    }
    public virtual void MountOn() { }
}
