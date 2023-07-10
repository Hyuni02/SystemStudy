using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class LobbyUIController : MonoBehaviour
{
    LobbyCameraController cameraController;
    CharacterInfoLoader characterInfoLoader;
    int index;

    public GameObject Panel_Inven;
    public GameObject Panel_SelectedCharacter;
    public GameObject[] Panels;

    void Start()
    {
        cameraController = GetComponent<LobbyCameraController>();
        characterInfoLoader = GetComponent<CharacterInfoLoader>();
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
        Debug.LogWarning("Updat_Panel_SelectedCharacter is not realized");
        Debug.LogWarning("Show SelectedCharacter's inventory is not realized");
        Debug.LogWarning("Show SelectedCharacter's equipment is not realized");
    }

    void OpenPanel(int _index) {
        Panels[_index].gameObject.SetActive(true);
    }
}
