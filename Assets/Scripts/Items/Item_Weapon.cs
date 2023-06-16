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

    public override void Init(ItemInfo_compact item) {
        base.Init(item);
        muzzle = item.properties.Find(x => x.str.Equals("muzzle")).it;
        under_rail = item.properties.Find(x => x.str.Equals("under_rail")).it;
        side_rail = item.properties.Find(x => x.str.Equals("side_rail")).it;
        upper_rail = item.properties.Find(x => x.str.Equals("upper_rail")).it;
        magazine = item.properties.Find(x => x.str.Equals("magazine")).it;
        stock = item.properties.Find(x => x.str.Equals("stock")).it;
    }


    public override ItemInfo_compact GetSaveInfo() {
        base.GetSaveInfo();
        info.itemcount = 1;
        info.properties.Add(new StringInt("muzzle", muzzle));
        info.properties.Add(new StringInt("under_rail", under_rail));
        info.properties.Add(new StringInt("side_rail", side_rail));
        info.properties.Add(new StringInt("upper_rail", upper_rail));
        info.properties.Add(new StringInt("magazine", magazine));
        info.properties.Add(new StringInt("stock", stock));
        print("GetSaveInfo from Weapon");
        return info;
    }

    public abstract void Fire();
    public abstract void Aim();
    public abstract void Reload();
}
