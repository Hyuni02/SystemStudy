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
        //���̺� ������ ���� ��
        if (!File.Exists(Application.dataPath + "/Resources/LobbyInven.txt")) {
            string[] line = lobbyInvenData.text.Substring(0, lobbyInvenData.text.Length - 1).Split('\n');
            for (int i = 0; i < line.Length; i++) {
                string[] row = line[i].Split("\t");
                Data_LInven.Add(new InvenInfo(int.Parse(row[0]), int.Parse(row[1])));
            }
            print("Create New Inventory Data File");
            SaveLobbyInventory();
        } 
        //���̺� ������ ���� ��
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
        txt_Name.SetText(code);
    }
    void ShowItems() {
        foreach (var item in Data_LInven) {
            GameObject button = Instantiate(pre_Button);
            button.transform.SetParent(Content_LobbyInventory, false);
            button.name = item.code.ToString();
            button.transform.GetChild(0).GetComponent<TMP_Text>().SetText(item.code.ToString());

            //��ư �̹��� ��������
            Debug.LogWarning("Load Item Image is not realized");

            button.GetComponent<Button>().onClick.AddListener(() => ButtonClicked(button.name));
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
        SaveLobbyInventory();
    }
}