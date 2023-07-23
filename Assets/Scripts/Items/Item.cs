using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    [field : SerializeField]
    public string itemname { get; set; }
    [field: SerializeField]
    public int code { get; set; }
    [field: SerializeField]
    public float volume { get; private set; }
    [field: SerializeField]
    public float weight { get; set; }
    [field : SerializeField]
    public int count { get; set; }
    [field: SerializeField]
    
    protected ItemInfo_compact info;

    GameObject Main_Camera;
    ItemImageLoader itemImageLoader = null;

    private void Start() {
        Main_Camera = GameObject.Find("Main Camera");
        itemImageLoader = Main_Camera.GetComponent<ItemImageLoader>();
        GetComponent<Image>().sprite = itemImageLoader.GetSprite(GetComponent<Item>().code);
    }

    //아이템 정보 초기화
    public virtual void Init(ItemInfo_compact item) {
        //print("Init : " + name);
        ItemInfo data = LoadItemData.instance.GetItemData(item.itemcode);
        itemname = data.name;
        code = data.code;
        volume = data.volume;
        weight = data.weight;
        count = item.itemcount;
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
