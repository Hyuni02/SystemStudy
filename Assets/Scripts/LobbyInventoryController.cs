using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using System.Text;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Unity.VisualScripting;
using UnityEngine.InputSystem;
using Newtonsoft.Json;
using static UnityEditor.Progress;

[Serializable]
public class InvenInfo { 
    public InvenInfo(int _code, int _count) {
        code= _code;
        count= _count;
    }
    public int code; 
    public int count;
}

public class LobbyInventoryController : MonoBehaviour
{
    public TextAsset lobbyInvenData;

    [Header("Debug Slot")]
    public Button btn_Get;
    public Button btn_Remove;
    public TMP_Text txt_Name;

    [Header("Prefab")]
    public GameObject pre_Button;

    public Transform Panel_LobbyInventory;
    public Transform Content_LobbyInventory;

    [SerializeField]
    public List<InvenInfo> Data_LInven;

    public void Start() {
        LoadLobbyInventory();
    }

    private void Update() {
        if (Keyboard.current.sKey.wasPressedThisFrame) {
            SaveLobbyInventory();
        }
        if(Keyboard.current.lKey.wasPressedThisFrame) {
            LoadLobbyInventory();
        }
    }

    public void LoadLobbyInventory() {
        //세이브 파일이 없을 때
        if (!File.Exists(Application.dataPath + "/Resources/LobbyInven.txt")) {
            string[] line = lobbyInvenData.text.Substring(0, lobbyInvenData.text.Length - 1).Split('\n');
            for (int i = 0; i < line.Length; i++) {
                string[] row = line[i].Split("\t");
                Data_LInven.Add(new InvenInfo(int.Parse(row[0]), int.Parse(row[1])));
            }
            print("Create New Inventory Data File");
            SaveLobbyInventory();
        } 
        //세이브 파일이 있을 때
        else {
            string jdata = File.ReadAllText(Application.dataPath + "/Resources/LobbyInven.txt");
            Data_LInven = JsonConvert.DeserializeObject<List<InvenInfo>>(jdata);
        }
        print("Load Lobby Inventory Data");
        ShowItems();
    }
    void ButtonClicked(string code) {
        print("Click : " + code);

        //debug
        string itemName = GetComponent<LoadItemData>().Data_Item.Find(x => x.code == int.Parse(code)).name;
        txt_Name.SetText(itemName);
    }
    void ShowItems() {
        for(int i=0;i<Content_LobbyInventory.childCount;i++) {
            Destroy(Content_LobbyInventory.GetChild(i).gameObject);    
        }

        foreach (var item in Data_LInven) {
            for (int i = 0; i < item.count; i++) {
                GameObject button = Instantiate(pre_Button);
                button.transform.SetParent(Content_LobbyInventory, false);
                button.name = item.code.ToString();
                string itemName = GetComponent<LoadItemData>().Data_Item.Find(x => x.code == item.code).name;
                button.transform.GetChild(0).GetComponent<TMP_Text>().SetText(itemName);

                //버튼 이미지 가져오기
                Debug.LogWarning("Load Item Image is not realized");

                button.GetComponent<Button>().onClick.AddListener(() => ButtonClicked(button.name));
            }
        }
    }

    public void SaveLobbyInventory() {
        string jdata = JsonConvert.SerializeObject(Data_LInven);
        File.WriteAllText(Application.dataPath + "/Resources/LobbyInven.txt", jdata);

        print("Save Lobby Inventory Data");
    }

    public void AddtoLobbyInventory(int code, int count = 1) {

    }
    public void RemoveFromLobbyInventory(int code, int count = 1) { 

    }

    public void Debug_GetItem() {
        InvenInfo item = Data_LInven.Find(x => x.code == int.Parse(txt_Name.text));
        if (item != null) {
            item.count++;
            print(item.count.ToString());
        }
        ShowItems();
        SaveLobbyInventory();
    }
    public void Debug_RemoveItem() {
        InvenInfo item = Data_LInven.Find(x => x.code == int.Parse(txt_Name.text));
        if (item != null) {
            item.count--;
            if(item.count <= 0) {
                item.count = 0;
            }
            print(item.count.ToString());
        }
        ShowItems();
        SaveLobbyInventory();
    }
}
