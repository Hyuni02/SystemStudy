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

    public ItemInfo_compact(ItemInfo_compact info = null) {
        if (info != null) {
            itemcode = info.itemcode;
            itemcount = info.itemcount;
            properties = info.properties;
        }
    }
}

[Serializable]
public class Class_SaveData {
    public int money;
    public List<ItemInfo_compact> items;
}

public class LobbyInventoryController : MonoBehaviour {
    public TextAsset InitLobbyInvenData;
    public Image dragImage;
    public int dragCode;
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
    public Slider Slider_Inven;
    public float maxCapacity = 500f;
    public float currentCapacity = 0f;
    public List<ItemInfo_compact> SaveData;


    public void Start() {
        Content_LobbyInventory.transform.parent.GetComponent<InventoryProperty>().Target_Inventory = nameof(SaveData);
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

    public void SetItemProps(ref ItemInfo_compact item) {
        int code = item.itemcode;
        string[] props = LoadItemData.instance.Data_Item.Find(x => x.code == code).etc;
        #region ���� ���� ����
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
            case 3:
                break;
            case 4:
            case 5:
                item.properties.Add(new StringInt("health", int.Parse(props[1])));
                break;
        }
        #endregion
    }
    public void LoadLobbyInventory() {
        //���̺� ������ ���� ��
        if (!File.Exists(Application.dataPath + "/Resources/SaveData.txt")) {
            //�ʱ�ȭ ���� �ҷ�����
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
        //���̺� ������ ���� ��
        else {
            string jdata = File.ReadAllText(Application.dataPath + "/Resources/SaveData.txt");
            Class_SaveData loadedData = JsonUtility.FromJson<Class_SaveData>(jdata);
            //money = loadedData.money;
            SaveData = loadedData.items;
            print("Load Lobby Inventory Data");
        }


        Update_CapacitySlider(Slider_Inven, ref SaveData, maxCapacity);
        ShowItems(ref Content_LobbyInventory, ref SaveData);
    }
    public void Update_CapacitySlider(Slider slider, ref List<ItemInfo_compact> list_item, float max) {
        //�κ��丮 �뷮 ���ΰ�ħ
        currentCapacity = 0;
        foreach (var item in list_item) {
            currentCapacity += LoadItemData.instance.GetItemData(item.itemcode).volume * item.itemcount;
        }
        float percent = currentCapacity / max;
        slider.value = percent;
        if (percent > 0.67f) slider.fillRect.GetComponent<Image>().color = Color.red;
        else slider.fillRect.GetComponent<Image>().color = Color.green;
    }
    void ButtonClicked(GameObject button) {
        print("Click : " + button.GetComponent<Item>().itemname);
        selectedCode = button.GetComponent<Item>().code;
        //debug
        string itemName = button.GetComponent<Item>().itemname;
        txt_Name.SetText(itemName);
    }

    //������(��ư)�� ������ ����
    public void AddComponenttoButton(ItemInfo_compact item, GameObject button) {
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
    }
    /// <summary>
    /// ������ viewport�� list_item�� ��ư���� ����
    /// </summary>
    /// <param name="viewport">��ư�� ������ ��ġ(�θ�)</param>
    /// <param name="list_item">������ ��ư ����Ʈ</param>
    public void ShowItems(ref Transform viewport,ref List<ItemInfo_compact> list_item) {
        //viewport �ʱ�ȭ
        for (int i = 0; i < viewport.childCount; i++) {
            Destroy(viewport.GetChild(i).gameObject);
        }

        //����Ʈ ����
        list_item = list_item.OrderBy(x => x.itemcode).ToList();

        #region ��ư ����
        //��ư�� �Ҽӵ� �κ� ���
        viewport.parent.GetComponent<UIProperty>().inven = list_item;

        foreach (var item in list_item.Select((value, index) => (value, index))) {
            GameObject button = Instantiate(pre_Button);
            //��ư �θ� ����
            button.transform.SetParent(viewport, false);
            //��ư �̸� ����
            button.name = $"{LoadItemData.instance.GetItemData(item.value.itemcode).name}({item.value.itemcode.ToString()})";
            //��ư �Ӽ� ���� (�巡��, ��� ����)
            button.GetComponent<UIProperty>().index = item.index;
            button.GetComponent<UIProperty>().inven = list_item;
            button.GetComponent<UIProperty>().b_dragable = true;
            button.GetComponent<UIProperty>().b_dropable = true;
            string itemName = GetComponent<LoadItemData>().Data_Item.Find(x => x.code == item.value.itemcode).name;
            button.transform.GetChild(0).GetComponent<TMP_Text>()?.SetText(itemName);
            AddComponenttoButton(item.value, button);
           
            //��ư�� ���� ǥ��
            if (GetComponent<LoadItemData>().Data_Item.Find(x => x.code == item.value.itemcode).stack != 1)
                button.transform.GetChild(1).GetComponent<TMP_Text>()?.SetText(item.value.itemcount.ToString());
            else 
                button.transform.GetChild(1).GetComponent<TMP_Text>()?.SetText("");
            

            //Ŭ�� �̺�Ʈ �Ҵ�
            button.GetComponent<Button>().onClick.AddListener(() => ButtonClicked(button));
        }
        #endregion
    }

    /// <summary>
    /// �κ� ������ ����
    /// </summary>
    public void SaveLobbyInventory() {
        Class_SaveData class_SaveData = new Class_SaveData();
        class_SaveData.money = 100;
        Debug.LogWarning("����� �ڿ� ���� ���� �ʿ�");
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
        print($"Add : {selectedCode}({itemcount})");
        //������ �߰�
        int max = LoadItemData.instance.GetItemData(selectedCode).stack;
        int newitemgroup = itemcount / max; //������ �׷� ��
        int rests = itemcount % max; //������ ������ ��

        if (newitemgroup > 0) {
            for (int i = 0; i < newitemgroup; i++) {
                ItemInfo_compact newitem = new ItemInfo_compact();
                newitem.itemcode = selectedCode;
                newitem.itemcount = max;
                SetItemProps(ref newitem);
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
        //�κ��丮 ���ΰ�ħ
        ShowItems(ref Content_LobbyInventory, ref SaveData);
        //������ ����
        SaveLobbyInventory();
        LoadLobbyInventory();
    }
    public void Debug_RemoveItem() {
        //������ ����
        //������ �������� �κ��丮�� �ִ��� Ȯ��
        foreach(var item in SaveData) {
            if(item.itemcode == selectedCode) {
                ItemInfo_compact target = item;
                //�ش� �������� ���� 1����
                print($"Remove : {selectedCode}(1)");
                target.itemcount--;
                //������ �������� ���� 0�̸� ��ư �����
                if (target.itemcount == 0) {
                    SaveData.Remove(target);
                }
                //�κ��丮 ���ΰ�ħ
                ShowItems(ref Content_LobbyInventory, ref SaveData);
                //������ ����
                SaveLobbyInventory();
                LoadLobbyInventory();
                return;
            }
        }
        //�ش� �������� ������ �ƹ��͵� ����
        print($"No Item : {selectedCode}");
    }

    int comparel(ItemInfo_compact a, ItemInfo_compact b) {
        return a.itemcode < b.itemcode ? -1 : 1;
    }
}
