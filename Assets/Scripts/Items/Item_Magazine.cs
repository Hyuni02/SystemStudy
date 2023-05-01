using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Magazine : Item_Parts
{
    [field : SerializeField] public int capacity { get; private set; }
    [field : SerializeField] public int remains { get; private set; }
    [field : SerializeField] public int reload_reduction { get; private set; }
    [field : SerializeField] public int aim_decrease { get; private set; }

    public override void Init(int _code) {
        base.Init(_code);
        mount = Mount.mag;  
        string[] etc = LoadItemData.instance.GetItemData(_code).etc;
        capacity = int.Parse(etc[0]);
        //TODO
        //remains = 0; //ó�� ����
        //remain = �κ��丮 ������ �ҷ����� // ������ �ִ� źâ
        reload_reduction= int.Parse(etc[1]);
        aim_decrease = int.Parse(etc[2]);
    }

    public void Insert_Ammo() {

    }
    public void Eject_Ammo() {

    }
}
