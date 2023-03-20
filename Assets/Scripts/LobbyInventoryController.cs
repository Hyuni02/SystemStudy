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
    [Header("Prefab")]
    public GameObject pre_Button;

    public Transform Panel_LobbyInventory;

    [SerializeField]
    public List<InvenInfo> Data_LInven = new List<InvenInfo>();

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

    }
    void ButtonClicked(string code) {
        print("Click : " + code);
    }

    public void SaveLobbyInventory() {
        
    }

    public void AddtoLobbyInventory(int code, int count = 1) {

    }
    public void RemoveFromLobbyInventory(int code, int count = 1) { 
        
    }
}
