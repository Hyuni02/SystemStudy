using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item_Weapon : Item_Equipment
{
    public int muzzle;
    public int under_rail;
    public int side_rail;
    public int upper_rail;
    public int magazine;
    public int stock;

    public override void Init(int _code) {
        base.Init(_code);
        string[] etc = LoadItemData.instance.GetItemData(_code).etc;
        muzzle = int.Parse(etc[0]);
        under_rail = int.Parse(etc[1]);
        side_rail = int.Parse(etc[2]);
        upper_rail = int.Parse(etc[3]);
        magazine = int.Parse(etc[4]);
        stock = int.Parse(etc[5]);
    }


    public override ItemInfo_compact GetSaveInfo() {
        base.GetSaveInfo();
        info.itemcount = 1;
        info.properties.Add("muzzle", muzzle);
        info.properties.Add("under_rail", under_rail);
        info.properties.Add("side_rail", side_rail);
        info.properties.Add("upper_rail", upper_rail);
        info.properties.Add("magazine", magazine);
        info.properties.Add("stock", stock);
        print("GetSaveInfo from Weapon");
        return info;
    }

    public abstract void Fire();
    public abstract void Aim();
    public abstract void Reload();
}
