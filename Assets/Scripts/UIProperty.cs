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
        print($"�巡���� ������Ʈ : {drag.name}");
        print($"����� ��ġ�� ������Ʈ : {drop.name}");

        //foreach(var comp in drag.GetComponents<Component>()) {
        //    print(comp.GetType());
        //}

        //�巡�� �Ұ����� ������Ʈ�� �巡�� (���� ������ �巡��)
        if(drag.GetComponent<UIProperty>().b_dragable == false) {
            print("�巡�� �Ұ��� ���");
            return;
        }

        //����� �� ���� ��ġ�� ��� (���� ������ ��)
        if (!b_dropable) {
            if(drop.GetComponent<EquipmentUI>()?.equiped != null) {
                print("�ش� ���Կ��� �̹� �������� ������");
            }
            else {
                print("��� �Ұ��� ��ġ");
            }
            return;
        }

        bool trig = false;
        //����ĭ�� ���
        if (drop.CompareTag("UI_Slot")) {
            switch(GetComponent<EquipmentUI>().itemtype) {
                case Type.pri:
                    if (drag.GetComponent<Item_Primary>() != null) {
                        print($"{drag.name} -> {drop.name}");
                        return;
                    }
                    break;
                case Type.sec:
                    if (drag.GetComponent<Item_Secondary>() != null) {
                        print($"{drag.name} -> {drop.name}");
                        return;
                    }
                    break;
                case Type.helmet:
                    if (drag.GetComponent<Item_Helmet>() != null) {
                        print($"{drag.name} -> {drop.name}");
                        return;
                    }
                    break;
                case Type.body: 
                    if(drag.GetComponent<Item_BodyArmor>() != null) {
                        print($"{drag.name} -> {drop.name}");
                        return;
                    }
                    break;
                case Type.bag:
                    if (drag.GetComponent<Item_Bag>() != null) {
                        print($"{drag.name} -> {drop.name}");
                        return;
                    }
                    break;
            }
        }
        //�κ��� ���
        else {
            Transform from = drag.transform;
            Transform to = drop.transform;

            //������ ���� ���
            if(to.GetComponent<Item>() != null) {
                //���� �κ��� ����
                if (from.parent.parent.GetComponent<UIProperty>() == to.parent.parent.GetComponent<UIProperty>() ||
                from.parent.parent.GetComponent<UIProperty>() == to.GetComponent<UIProperty>()) {
                    print("���� �κ�");
                    trig = MoveItem(from, to);
                }
                //�ٸ� �κ��� ����
                else {
                    print("�ٸ� �κ�");
                    //�뷮�� �������� �ʴ��� Ȯ��
                    Debug.LogWarning("�뷮 ��Ȯ��");
                    trig = MoveItem(from, to);
                }
            }
            //��ĭ�� ���
            else {
                print("�κ��� ��ĭ�� ���");
                //�ٸ� �κ��� ��ĭ
                if(from.parent.parent.GetComponent<UIProperty>() != to.GetComponent<UIProperty>()) {
                    print($"{from.parent.parent.GetComponent<InventoryProperty>().Target_Inventory}�� �κ��丮���� ������ ����, {to.GetComponent<InventoryProperty>().Target_Inventory}�� �κ��丮�� ������ �߰�");
                    return;
                }
            }

        }

        if(!trig) print("��ȿ���� ���� �̵�");

    }


    bool MoveItem(Transform from, Transform to) {
        //���� ������
        if (from.GetComponent<Item>().code == to.GetComponent<Item>().code) {
            print("���� ������");
            //������ ��ġ��
            if (LoadItemData.instance.GetItemData(to.GetComponent<Item>().code).stack == 1) {
                print("��ĥ �� ���� ������");
                return true;
            }
            if (from.GetComponent<Item>().count + to.GetComponent<Item>().count <= LoadItemData.instance.GetItemData(to.GetComponent<Item>().code).stack) {
                print($"{to.name}�� ���� += {from.name}�� ����");
                print($"{from.name} ����");
                return true;
            }
            else {
                print("to�� �ִ� ����");
                print("from ����");
                return true;
            }
        }
        //�ٸ� ������
        else {
            print("�ٸ� ������");
            //������ �ѱ⿡ ���
            if (from.GetComponent<Item_Parts>() != null && to.GetComponent<Item_Weapon>() != null) {
                Debug.LogWarning("����-���� ���Ͽ��� �������� ���� Ȯ�� �ϱ�");
            }
        }
        return false;
    }
}
