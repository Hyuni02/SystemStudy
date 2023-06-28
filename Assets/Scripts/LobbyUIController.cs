using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LobbyUIController : MonoBehaviour
{
    LobbyCameraController cameraController;
    int index;

    public GameObject Panel_Inven;
    public GameObject Panel_SelectedCharacter;
    public GameObject[] Panels;

    void Start()
    {
        cameraController = GetComponent<LobbyCameraController>();
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
                    break;
                case LobbyFacility.FacilityType.Table:
                    index = 3;
                    break;
                case LobbyFacility.FacilityType.Sever:
                    index = 4;
                    break;
                case LobbyFacility.FacilityType.Counter:
                    index = 5;
                    break;
                case LobbyFacility.FacilityType.RestoreStation:
                    index = 6;
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
    void Open_Panel_SelectedCharacter() {
        Debug.LogWarning("SelectedCharacter is not realized");
        Panel_SelectedCharacter.SetActive(true);
    }

    void OpenPanel(int _index) {
        Panels[_index].gameObject.SetActive(true);
    }
}
