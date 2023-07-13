using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIProperty : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler {
    [Header("Button Property")]
    public bool b_dragable = false;
    public bool b_dropable = false;

    GameObject Main_Camera;
    LobbyInventoryController lobbyInventoryController = null;
    private void Start() {
        Main_Camera = GameObject.Find("Main Camera");
        lobbyInventoryController = Main_Camera.GetComponent<LobbyInventoryController>();
    }

    public void OnBeginDrag(PointerEventData eventData) {
        if (!b_dragable) return;
        lobbyInventoryController.dragImage.gameObject.SetActive(true);
        lobbyInventoryController.dragImage.GetComponent<Image>().sprite = GetComponent<Image>().sprite;
        lobbyInventoryController.dragCode = this.GetComponent<Item>() ? this.GetComponent<Item>().code : this.GetComponent<EquipmentUI>().equiped.itemcode;
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

    public void OnDrop(PointerEventData eventData) {
        GameObject drag = eventData.pointerDrag.gameObject;
        GameObject drop = gameObject;
        //print($"�巡���� ������Ʈ : {drag.name}");
        //print($"����� ��ġ�� ������Ʈ : {drop.name}");

        //foreach(var comp in drag.GetComponents<Component>()) {
        //    print(comp.GetType());
        //}

        //����� �� ���� ��ġ�� ��� (���� ������ ��)
        if (!b_dropable) {
            if(drop.GetComponent<EquipmentUI>().equiped != null) {
                print("�ش� ���Կ��� �̹� �������� ������");
            }
            else {
                print("��� �Ұ��� ��ġ");
            }
            return;
        }

        //����ĭ�� ���
        if (drop.CompareTag("UI_Slot")) {
            switch(GetComponent<EquipmentUI>().itemtype) {
                case Type.pri:
                    break;
                case Type.sec:
                    break;
                case Type.helmet:
                    if (drag.GetComponent<Item_Helmet>() != null) {
                        print($"{drag.name} -> {drop.name}");
                        return;
                    }
                    break;
                case Type.body: 
                    break;
                case Type.bag: 
                    break;

            }
        }
        //�κ��� ���
        else {

        }

        print("��ȿ���� ���� �̵�");

    }
}
