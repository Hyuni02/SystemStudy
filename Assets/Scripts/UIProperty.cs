using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIProperty : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {
    [Header("Button Property")]
    public bool b_dragable = false;
    public bool b_dropable = false;


    LobbyInventoryController lobbyInventoryController = null;
    private void Start() {
        lobbyInventoryController = GameObject.FindFirstObjectByType<LobbyInventoryController>();
    }

    public void OnBeginDrag(PointerEventData eventData) {
        if (!b_dragable) return;
        lobbyInventoryController.dragImage.gameObject.SetActive(true);
        lobbyInventoryController.dragImage.GetComponent<Image>().sprite = GetComponent<Item>().sprite;
        lobbyInventoryController.dragCode = this.GetComponent<Item>().code;


    }

    public void OnDrag(PointerEventData eventData) {
        if (!b_dragable) return;

        PointerEventData pointer_data = (PointerEventData)eventData;
        lobbyInventoryController.dragImage.transform.position = pointer_data.position;

    }

    public void OnEndDrag(PointerEventData eventData) {
        if (!b_dragable) return;
        lobbyInventoryController.dragImage.gameObject.SetActive(false);
        lobbyInventoryController.dragImage.GetComponent<Image>().sprite = null;
        lobbyInventoryController.dragCode = 0;

    }
}
