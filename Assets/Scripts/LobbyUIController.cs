using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem.Controls;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class LobbyUIController : MonoBehaviour
{
    LobbyCameraController cameraController;
    CharacterInfoLoader characterInfoLoader;
    int index;

    public GameObject Panel_Inven;
    public GameObject Panel_SelectedCharacter;
    EquipmentUI SelectedCharacter_Helmet;
    EquipmentUI SelectedCharacter_BodyArmor;
    EquipmentUI SelectedCharacter_Bag;
    EquipmentUI SelectedCharacter_Primary;
    EquipmentUI SelectedCharacter_Primary_Muzzle;
    EquipmentUI SelectedCharacter_Primary_Grip;
    EquipmentUI SelectedCharacter_Primary_Mag;
    EquipmentUI SelectedCharacter_Primary_Sight;
    EquipmentUI SelectedCharacter_Primary_Stock;
    EquipmentUI SelectedCharacter_Secondary;
    EquipmentUI SelectedCharacter_Secondary_Muzzle;
    EquipmentUI SelectedCharacter_Secondary_Sight;
    EquipmentUI SelectedCharacter_Secondary_Mag;

    public GameObject[] Panels;

    void Start()
    {
        cameraController = GetComponent<LobbyCameraController>();
        characterInfoLoader = GetComponent<CharacterInfoLoader>();

        SelectedCharacter_Helmet = Panel_SelectedCharacter.transform.GetChild(0).GetComponent<EquipmentUI>();
        SelectedCharacter_BodyArmor = Panel_SelectedCharacter.transform.GetChild(1).GetComponent<EquipmentUI>();
        SelectedCharacter_Bag = Panel_SelectedCharacter.transform.GetChild(2).GetComponent<EquipmentUI>();
        SelectedCharacter_Primary = Panel_SelectedCharacter.transform.GetChild(3).GetChild(0).GetComponent<EquipmentUI>();
        SelectedCharacter_Primary_Muzzle = Panel_SelectedCharacter.transform.GetChild(3).GetChild(1).GetComponent<EquipmentUI>();
        SelectedCharacter_Primary_Grip = Panel_SelectedCharacter.transform.GetChild(3).GetChild(2).GetComponent<EquipmentUI>();
        SelectedCharacter_Primary_Mag = Panel_SelectedCharacter.transform.GetChild(3).GetChild(3).GetComponent<EquipmentUI>();
        SelectedCharacter_Primary_Sight = Panel_SelectedCharacter.transform.GetChild(3).GetChild(4).GetComponent<EquipmentUI>();
        SelectedCharacter_Primary_Stock = Panel_SelectedCharacter.transform.GetChild(3).GetChild(5).GetComponent<EquipmentUI>();
        SelectedCharacter_Secondary = Panel_SelectedCharacter.transform.GetChild(4).GetChild(0).GetComponent<EquipmentUI>();
        SelectedCharacter_Secondary_Muzzle = Panel_SelectedCharacter.transform.GetChild(4).GetChild(1).GetComponent<EquipmentUI>();
        SelectedCharacter_Secondary_Sight = Panel_SelectedCharacter.transform.GetChild(4).GetChild(2).GetComponent<EquipmentUI>();
        SelectedCharacter_Secondary_Mag = Panel_SelectedCharacter.transform.GetChild(4).GetChild(3).GetComponent<EquipmentUI>();
    }

    void Update()
    {
        
    }

    public void UpdateUI() {
        CloseAllPanel();
        if (cameraController.FocusPos != null) {
            switch (cameraController.FocusPos.GetComponentInParent<LobbyFacility>().facilityType) {
                case LobbyFacility.FacilityType.Shop:
                    index = 0;
                    Open_Panel_Inven();
                    break;
                case LobbyFacility.FacilityType.Inventory:
                    index = 1;
                    Open_Panel_Inven();
                    Open_Panel_SelectedCharacter();
                    break;
                case LobbyFacility.FacilityType.CommandTable:
                    index = 2;
                    Debug.LogWarning("지휘부 열기 미구현");
                    break;
                case LobbyFacility.FacilityType.Table:
                    index = 3;
                    Debug.LogWarning("테이블 열기 미구현");
                    break;
                case LobbyFacility.FacilityType.Sever:
                    index = 4;
                    Debug.LogWarning("서버실 열기 미구현");
                    break;
                case LobbyFacility.FacilityType.Counter:
                    index = 5;
                    Debug.LogWarning("카운터 열기 미구현");
                    break;
                case LobbyFacility.FacilityType.RestoreStation:
                    index = 6;
                    Debug.LogWarning("수복시설 열기 미구현");
                    Panel_SelectedCharacter.SetActive(true);
                    break;
                default:
                    Debug.LogError("Wrong Facility Type");
                    break;
            }
            OpenPanel(index);
        }
    }

    void CloseAllPanel() {
        Panel_Inven.SetActive(false);
        Panel_SelectedCharacter.SetActive(false);
        foreach (var panel in Panels) {
            panel.gameObject.SetActive(false);
        }
    }

    void Open_Panel_Inven() {
        GetComponent<LobbyInventoryController>().LoadLobbyInventory();
        Panel_Inven.SetActive(true);
    }
    void Open_Panel_SelectedCharacter(string name = null) {
        DollInfo dollInfo = null;
        if (name == null) {
            dollInfo = characterInfoLoader.Characters.First().Value;
        }
        else {
            dollInfo = characterInfoLoader.Characters[name];
        }
        Update_Panel_SelectedCharacter(dollInfo);

        Panel_SelectedCharacter.SetActive(true);
    }

    void Update_Panel_SelectedCharacter(DollInfo dollInfo) {
        print($"Open {dollInfo.name}");
        #region 장착아이템 보여주기
        //헬멧
        if (dollInfo.equipInfo.Helmet.itemcode != 0) {
            SelectedCharacter_Helmet.equiped = dollInfo.equipInfo.Helmet;
            print($"{dollInfo.name} - Helmet({dollInfo.equipInfo.Helmet.itemcode})");
        }
        else {
            print($"{dollInfo.name} - No Helmet");
        }
        //방탄복
        if (dollInfo.equipInfo.Armor.itemcode != 0) {
            SelectedCharacter_BodyArmor.equiped = dollInfo.equipInfo.Armor;
            print($"{dollInfo.name} - BodyArmor({dollInfo.equipInfo.Armor.itemcode})");
        }
        else {
            print($"{dollInfo.name} - No BodyArmor");
        }
        //가방
        if (dollInfo.equipInfo.Bag.itemcode != 0) {
            SelectedCharacter_Bag.equiped = dollInfo.equipInfo.Bag;
            print($"{dollInfo.name} - Bag({dollInfo.equipInfo.Bag.itemcode})");
        }
        else {
            print($"{dollInfo.name} - No Bag");
        }
        //주무기
        if (dollInfo.equipInfo.Primary.itemcode != 0) {
            SelectedCharacter_Primary.equiped = dollInfo.equipInfo.Primary;
            print($"{dollInfo.name} - Primary({dollInfo.equipInfo.Primary.itemcode})");
            Debug.LogWarning("주무기 부착물 표시 미구현");
        }
        else {
            print($"{dollInfo.name} - No Primary");
        }
        //보조무기
        if (dollInfo.equipInfo.Secondary.itemcode != 0) {
            SelectedCharacter_Secondary.equiped = dollInfo.equipInfo.Secondary;
            print($"{dollInfo.name} - Secondary({dollInfo.equipInfo.Secondary.itemcode})");
            Debug.LogWarning("보조무기 부착물 표시 미구현");
        }
        else {
            print($"{dollInfo.name} - No Secondary");
        }
        #endregion
        #region 보유 아이템 보여주기
        Debug.LogWarning("Show SelectedCharacter's inventory is not realized");
        #endregion
    }

    void OpenPanel(int _index) {
        Panels[_index].gameObject.SetActive(true);
    }
}
