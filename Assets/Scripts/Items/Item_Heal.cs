using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Heal : Item_Useable
{
    [field : SerializeField] public int health_recovery { get;private set; }

    public override void Init(int _code) {
        base.Init(_code);
        health_recovery = int.Parse(LoadItemData.instance.GetItemData(_code).etc[0]);
    }
}
