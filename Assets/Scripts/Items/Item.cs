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
    
    //아이템 정보 초기화
    public virtual void Init(int _code) {
        //print("Init : " + name);
        ItemInfo data = LoadItemData.instance.GetItemData(_code);
        itemname = data.name;
        code = data.code;
        volume = data.volume;
        weight = data.weight;
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
