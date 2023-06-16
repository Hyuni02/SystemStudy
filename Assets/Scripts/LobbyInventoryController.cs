using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Unity.VisualScripting;
using UnityEngine.InputSystem;
using Newtonsoft.Json;
using static UnityEditor.Progress;
using System.ComponentModel.Design;
using JetBrains.Annotations;
using System.Linq;

[Serializable]
public class InvenInfo { 
    public InvenInfo(int _code, int _count) {
        code= _code;
        count= _count;
    }
    public int code; 
    public int count;
}
[Serializable]
public class StringInt {
    public string str;
    public int it;
    public StringInt(string str, int it) {
        this.str = str;
        this.it = it;
    }
}
[Serializable]
public class ItemInfo_compact {
    public int itemcode;
    public int itemcount;
    [SerializeField]
    public List<StringInt> properties = new List<StringInt>();
}

[Serializable]
public class Class_SaveData {
    public int money;
    public List<ItemInfo_compact> items;
}

public class LobbyInventoryController : MonoBehaviour {
    public TextAsset InitLobbyInvenData;

    [HideInInspector]
    public int selectedCode;
    [HideInInspector]
    int itemcount = 3;

    [Header("Debug Slot")]
    public Button btn_Get;
    public Button btn_Remove;
    public TMP_Text txt_Name;

    [Header("Prefab")]
    public GameObject pre_Button;

    public Transform Panel_LobbyInventory;
    public Transform Content_LobbyInventory;

    [SerializeField]
    public List<ItemInfo_compact> SaveData;

    public void Start() {
        LoadLobbyInventory();
    }

    private void Update() {
        if (Keyboard.current.sKey.wasPressedThisFrame) {
            SaveLobbyInventory();
        }
        if (Keyboard.current.lKey.wasPressedThisFrame) {
            LoadLobbyInventory();
        }
    }

    void SetItemProps(ref ItemInfo_compact item) {
        int code = item.itemcode;
        string[] props = LoadItemData.instance.Data_Item.Find(x => x.code == code).etc;
        #region 내부 정보 저장
        int type = item.itemcode / 1000;
        switch (type) {
            case 2:
                int subtype = item.itemcode / 100;
                switch (subtype) {
                    case 20:
                    case 21:
                        item.properties.Add(new StringInt("muzzle", int.Parse(props[0])));
                        item.properties.Add(new StringInt("under_rail", int.Parse(props[1])));
                        item.properties.Add(new StringInt("side_rail", int.Parse(props[2])));
                        item.properties.Add(new StringInt("upper_rail", int.Parse(props[3])));
                        item.properties.Add(new StringInt("magazine", int.Parse(props[4])));
                        item.properties.Add(new StringInt("stock", int.Parse(props[5])));
                        break;
                    case 27:
                        item.properties.Add(new StringInt("count", 0));
                        break;
                }
                break;
            case 4:
            case 5:
                item.properties.Add(new StringInt("health", int.Parse(props[1])));
                break;
        }
        #endregion
    }
    public void LoadLobbyInventory() {
        //세이브 파일이 없을 때
        if (!File.Exists(Application.dataPath + "/Resources/SaveData.txt")) {
            //초기화 파일 불러오기
            string[] line = InitLobbyInvenData.text.Substring(0, InitLobbyInvenData.text.Length - 1).Split('\n');
            for (int i = 0; i < line.Length; i++) {
                string[] row = line[i].Split("\t");
                ItemInfo_compact item = new ItemInfo_compact();
                item.itemcode = int.Parse(row[0]);
                item.itemcount = int.Parse(row[1]);

                SetItemProps(ref item);

                SaveData.Add(item);
            }

            print("Create New Inventory Data File");
            SaveLobbyInventory();
            LoadLobbyInventory();
        }
        //세이브 파일이 있을 때
        else {
            string jdata = File.ReadAllText(Application.dataPath + "/Resources/SaveData.txt");
            Class_SaveData loadedData = JsonUtility.FromJson<Class_SaveData>(jdata);
            //money = loadedData.money;
            SaveData = loadedData.items;
            print("Load Lobby Inventory Data");
        }

        ShowItems();
    }
    void ButtonClicked(GameObject button) {
        print("Click : " + button.GetComponent<Item>().itemname);
        selectedCode = button.GetComponent<Item>().code;
        //debug
        string itemName = button.GetComponent<Item>().itemname;
        txt_Name.SetText(itemName);
    }
    void ShowItems() {
        for (int i = 0; i < Content_LobbyInventory.childCount; i++) {
            Destroy(Content_LobbyInventory.GetChild(i).gameObject);
        }
        SaveData = SaveData.OrderBy(x => x.itemcode).ToList();

        foreach (var item in SaveData) {
            GameObject button = Instantiate(pre_Button);
            button.transform.SetParent(Content_LobbyInventory, false);
            button.name = item.itemcode.ToString();
            string itemName = GetComponent<LoadItemData>().Data_Item.Find(x => x.code == item.itemcode).name;
            button.transform.GetChild(0).GetComponent<TMP_Text>().SetText(itemName);
            #region Add Component
            int type = item.itemcode / 1000;
            switch (type) {
                case 1:
                    button.AddComponent<Item>();
                    break;
                case 2:
                    int subtype = item.itemcode / 100;
                    switch (subtype) {
                        case 20:
                            button.AddComponent<Item_Primary>();
                            break;
                        case 21:
                            button.AddComponent<Item_Secondary>();
                            break;
                        case 22:
                            button.AddComponent<Item_UpperRail>();
                            break;
                        case 23:
                            button.AddComponent<Item_UnderRail>();
                            break;
                        case 24:
                            button.AddComponent<Item_Stock>();
                            break;
                        case 25:
                            button.AddComponent<Item_Muzzle>();
                            break;
                        case 26:
                            button.AddComponent<Item_SideRail>();
                            break;
                        case 27:
                            button.AddComponent<Item_Magazine>();
                            break;
                        case 28:
                            button.AddComponent<Item_Ammo>();
                            break;
                        case 29:
                            button.AddComponent<Item_Throw>();
                            break;
                        default:
                            Debug.LogError("Inventory Item Code Error" + item.itemcode);
                            break;
                    }
                    break;
                case 3:
                    button.AddComponent<Item_Bag>();
                    break;
                case 4:
                    button.AddComponent<Item_Helmet>();
                    break;
                case 5:
                    button.AddComponent<Item_BodyArmor>();
                    break;
                case 6:
                    button.AddComponent<Item_Food>();
                    break;
                case 7:
                    button.AddComponent<Item_Heal>();
                    break;
                default:
                    Debug.LogError("Inventory Item Code Error : " + item.itemcode);
                    break;
            }
            button.GetComponent<Item>()?.Init(item);
            #endregion

            //버튼에 갯수 표시
            if (GetComponent<LoadItemData>().Data_Item.Find(x => x.code == item.itemcode).stack != 1) {
                button.transform.GetChild(1).GetComponent<TMP_Text>().SetText(item.itemcount.ToString());
            }
            else {
                button.transform.GetChild(1).GetComponent<TMP_Text>().SetText("");
            }
            //버튼 이미지 가져오기
            Debug.LogWarning("Load Item Image is not realized");
            //코드를 이용해서 이미지 가져오기

            button.GetComponent<Button>().onClick.AddListener(() => ButtonClicked(button));

        }
    }

    public void SaveLobbyInventory() {
        Class_SaveData class_SaveData = new Class_SaveData();
        class_SaveData.money = 100;
        class_SaveData.items = SaveData;
        string jdata2 = JsonUtility.ToJson(class_SaveData);
        File.WriteAllText(Application.dataPath + "/Resources/SaveData.txt", jdata2);
        print("Save Lobby Inventory Data");
    }

    public void AddtoLobbyInventory(int code, int count = 1) {

    }
    public void RemoveFromLobbyInventory(int code, int count = 1) {

    }

    public void Debug_GetItem() {
        print("Add : " + selectedCode + "(" + itemcount + ")");
        //아이템 추가
        int max = LoadItemData.instance.GetItemData(selectedCode).stack;
        int newitemgroup = itemcount / max; //아이템 그룹 수
        int rests = itemcount % max; //나머지 아이템 수

        if (newitemgroup > 0) {
            ItemInfo_compact newitem = new ItemInfo_compact();
            newitem.itemcode = selectedCode;
            newitem.itemcount = max;
            SetItemProps(ref newitem);

            for (int i = 0; i < newitemgroup; i++) {
                SaveData.Add(newitem);
            }
        }
        if (rests > 0) {
            ItemInfo_compact restitem = new ItemInfo_compact();
            restitem.itemcode = selectedCode;
            restitem.itemcount = rests;
            SetItemProps(ref restitem);
            SaveData.Add(restitem);
        }
        //인벤토리 새로고침
        ShowItems();
        //데이터 저장
        SaveLobbyInventory();

        //Data_LobbyInven.Add(new InvenInfo(selectedCode, 1));
        //Data_LobbyInven.Sort(comparel);
    }
    public void Debug_RemoveItem() {
        //InvenInfo item = Data_LobbyInven.Find(x => x.code == selectedCode);
        //if (item != null) {
        //    item.count--;
        //    if (item.count <= 0)
        //        Data_LobbyInven.Remove(item);
        //}
        //else {
        //    print("No Item in Inven");
        //}
        //Data_LobbyInven.Sort(comparel);
        //ShowItems();
        //SaveLobbyInventory();
    }

    int comparel(ItemInfo_compact a, ItemInfo_compact b) {
        return a.itemcode < b.itemcode ? -1 : 1;
    }
}
