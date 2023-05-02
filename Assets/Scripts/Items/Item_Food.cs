using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Food : Item_Useable
{
    [field:SerializeField] public int energy_recovery { get; private set; }

    public override void Init(int _code) {
        base.Init(_code);
        energy_recovery = int.Parse(LoadItemData.instance.GetItemData(_code).etc[0]);
    }
}
