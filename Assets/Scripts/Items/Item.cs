using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [field : SerializeField]
    public string itemname { get; private set; }
    [field: SerializeField]
    public int code { get; private set; }
    [field: SerializeField]
    public float volume { get; private set; }
    [field: SerializeField]
    public float weight { get; set; }
    [field : SerializeField]
    public int count { get; set; }

    protected ItemInfo_compact info;


    //아이템 정보 초기화
    public virtual void Init(int _code) {
        //print("Init : " + name);
        ItemInfo data = LoadItemData.instance.GetItemData(_code);
        itemname = data.name;
        code = data.code;
        volume = data.volume;
        weight = data.weight;
        //count = 1;
        Debug.LogWarning("Item Count Load 구현 필요");
    }

    public virtual ItemInfo_compact GetSaveInfo() {
        info = new ItemInfo_compact();
        info.itemcode = code;
        info.itemcount = count;
        print("GetSaveInfo from Item");
        return info;
    }

    public virtual void Interact_Pick() {
        print("Pick : " + itemname);
    }

    public virtual void Menu_Detail() {
        print("Detail : " + itemname); 
    }
    public virtual void Menu_Discard() {
        print("Discard : " + itemname);
    }

}
