using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.ReorderableList;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIProperty : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler {
    [Header("Item Property")]
    public List<ItemInfo_compact> inven = new List<ItemInfo_compact>();
    public int index = -1;
    
    [Header("Button Property")]
    public bool b_dragable = false;
    public bool b_dropable = false;

    GameObject Main_Camera;
    LobbyInventoryController lobbyInventoryController = null;
    CharacterInfoLoader characterInfoLoader = null;
    private void Start() {
        Main_Camera = GameObject.Find("Main Camera");
        lobbyInventoryController = Main_Camera.GetComponent<LobbyInventoryController>();
        characterInfoLoader = Main_Camera.GetComponent<CharacterInfoLoader>();
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
        ResetDragImage();
    }
    void ResetDragImage() {
        lobbyInventoryController.dragImage.gameObject.SetActive(false);
        lobbyInventoryController.dragImage.GetComponent<Image>().sprite = null;
        lobbyInventoryController.dragCode = 0;
    }

    public void OnDrop(PointerEventData eventData) {
        GameObject drag = eventData.pointerDrag.gameObject;
        GameObject drop = gameObject;
        print($"{drag.name} : {drop.name}");

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
                        //print($"{drag.name} -> {drop.name}");
                        //���Կ� ������ �߰�
                        ItemInfo_compact pri = new ItemInfo_compact();
                        pri.itemcode = drag.GetComponent<Item_Primary>().code;
                        lobbyInventoryController.SetItemProps(ref pri);
                        pri.properties[0].it = (int)drag.GetComponent<Item_Primary>().muzzle;
                        pri.properties[1].it = (int)drag.GetComponent<Item_Primary>().under_rail;
                        pri.properties[2].it = (int)drag.GetComponent<Item_Primary>().side_rail;
                        pri.properties[3].it = (int)drag.GetComponent<Item_Primary>().upper_rail;
                        pri.properties[4].it = (int)drag.GetComponent<Item_Primary>().magazine;
                        pri.properties[5].it = (int)drag.GetComponent<Item_Primary>().stock;
                        characterInfoLoader.Characters[LobbyUIController.selected_dollinfo.name].equipInfo.Primary = pri;
                        //�κ����� ������ ����
                        drag.GetComponent<UIProperty>().inven.RemoveAt(drag.GetComponent<UIProperty>().index);
                        aftermove();
                        return;
                    }
                    break;
                case Type.sec:
                    if (drag.GetComponent<Item_Secondary>() != null) {
                        //print($"{drag.name} -> {drop.name}");
                        //���Կ� ������ �߰�
                        ItemInfo_compact sec = new ItemInfo_compact();
                        sec.itemcode = drag.GetComponent<Item_Secondary>().code;
                        lobbyInventoryController.SetItemProps(ref sec);
                        sec.properties[0].it = (int)drag.GetComponent<Item_Secondary>().muzzle;
                        sec.properties[1].it = (int)drag.GetComponent<Item_Secondary>().under_rail;
                        sec.properties[2].it = (int)drag.GetComponent<Item_Secondary>().side_rail;
                        sec.properties[3].it = (int)drag.GetComponent<Item_Secondary>().upper_rail;
                        sec.properties[4].it = (int)drag.GetComponent<Item_Secondary>().magazine;
                        sec.properties[5].it = (int)drag.GetComponent<Item_Secondary>().stock;
                        characterInfoLoader.Characters[LobbyUIController.selected_dollinfo.name].equipInfo.Secondary = sec;
                        //�κ����� ������ ����
                        drag.GetComponent<UIProperty>().inven.RemoveAt(drag.GetComponent<UIProperty>().index);
                        aftermove();
                        return;
                    }
                    break;
                case Type.helmet:
                    if (drag.GetComponent<Item_Helmet>() != null) {
                        //print($"{drag.name} -> {drop.name}");
                        //���Կ� ������ �߰�
                        ItemInfo_compact helmet = new ItemInfo_compact();
                        helmet.itemcode = drag.GetComponent<Item_Helmet>().code;
                        lobbyInventoryController.SetItemProps(ref helmet);
                        //������ ����
                        helmet.properties[0].it = (int)drag.GetComponent<Item_Helmet>().health;
                        characterInfoLoader.Characters[LobbyUIController.selected_dollinfo.name].equipInfo.Helmet = helmet;
                        //�κ����� ������ ����
                        drag.GetComponent<UIProperty>().inven.RemoveAt(drag.GetComponent<UIProperty>().index);
                        aftermove();
                        return;
                    }
                    break;
                case Type.body: 
                    if(drag.GetComponent<Item_BodyArmor>() != null) {
                        //print($"{drag.name} -> {drop.name}");
                        //���Կ� ������ �߰�
                        ItemInfo_compact bodyarmor = new ItemInfo_compact();
                        bodyarmor.itemcode = drag.GetComponent<Item_BodyArmor>().code;
                        lobbyInventoryController.SetItemProps(ref bodyarmor);
                        //������ ����
                        bodyarmor.properties[0].it = (int)drag.GetComponent<Item_BodyArmor>().health;
                        characterInfoLoader.Characters[LobbyUIController.selected_dollinfo.name].equipInfo.Armor = bodyarmor;
                        //�κ����� ������ ����
                        drag.GetComponent<UIProperty>().inven.RemoveAt(drag.GetComponent<UIProperty>().index);
                        aftermove();
                        return;
                    }
                    break;
                case Type.bag:
                    if (drag.GetComponent<Item_Bag>() != null) {
                        //print($"{drag.name} -> {drop.name}");
                        //���Կ� ������ �߰�
                        ItemInfo_compact bag = new ItemInfo_compact();
                        bag.itemcode = drag.GetComponent<Item_Bag>().code;
                        lobbyInventoryController.SetItemProps(ref bag);
                        Debug.LogWarning("���� �Ӽ����� ��������");
                        characterInfoLoader.Characters[LobbyUIController.selected_dollinfo.name].equipInfo.Bag = bag;
                        //�κ����� ������ ����
                        drag.GetComponent<UIProperty>().inven.RemoveAt(drag.GetComponent<UIProperty>().index);
                        aftermove();
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
            if (to.GetComponent<Item>() != null) {
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
                //����ĭ���� �κ����� �̵�
                if (from.CompareTag("UI_Slot")) {
                    print($"{from.GetComponent<EquipmentUI>().name} = null, {to.GetComponent<InventoryProperty>().Target_Inventory}�� �κ��丮�� �������߰�");
                    //from�� Ÿ������ ĳ������ ������� ����,
                    string name = LobbyUIController.selected_dollinfo.name;
                    //�κ��� ������ �߰�
                    ItemInfo_compact equiped = new ItemInfo_compact(from.GetComponent<EquipmentUI>().equiped);
                    to.GetComponent<UIProperty>().inven.Add(equiped);
                    //������� = null
                    switch (from.GetComponent<EquipmentUI>().itemtype) {
                        case Type.helmet:
                            characterInfoLoader.Characters[name].equipInfo.Helmet.itemcode = 0;
                            break;
                        case Type.body:
                            characterInfoLoader.Characters[name].equipInfo.Armor.itemcode = 0;
                            break;
                        case Type.bag:
                            characterInfoLoader.Characters[name].equipInfo.Bag.itemcode = 0;
                            break;
                        case Type.pri:
                            characterInfoLoader.Characters[name].equipInfo.Primary.itemcode = 0;
                            break;
                        case Type.sec:
                            characterInfoLoader.Characters[name].equipInfo.Secondary.itemcode = 0;
                            break;
                    }
                    aftermove();
                    return;
                }
                //�κ����� �κ����� �̵�
                else {
                    //�ٸ� �κ��� ��ĭ
                    if (from.parent.parent.GetComponent<UIProperty>() != to.GetComponent<UIProperty>()) {
                        //print($"{from.parent.parent.GetComponent<InventoryProperty>().Target_Inventory}�� �κ��丮���� ������ ����, {to.GetComponent<InventoryProperty>().Target_Inventory}�� �κ��丮�� ������ �߰�");
                        ItemInfo_compact moveitem = new ItemInfo_compact(from.GetComponent<UIProperty>().inven[from.GetComponent<UIProperty>().index]);
                        //to �κ��� �߰�
                        to.GetComponent<UIProperty>().inven.Add(moveitem);
                        //from �κ����� ����
                        from.GetComponent<UIProperty>().inven.RemoveAt(from.GetComponent<UIProperty>().index);
                        aftermove();
                        return;
                    }
                }
            }

        }
        if(!trig) print("��ȿ���� ���� �̵�");
    }

    void aftermove() {
        if (LobbyUIController.instance.index != 0) {
            characterInfoLoader.SaveCharacterInfo(LobbyUIController.selected_dollinfo.name);
            characterInfoLoader.LoadCharacterInfo(LobbyUIController.selected_dollinfo.name);
            LobbyUIController.instance.ShowMoney();
            LobbyUIController.instance.Open_Panel_SelectedCharacter(LobbyUIController.selected_dollinfo.name);
        }
        lobbyInventoryController.SaveLobbyInventory();
        lobbyInventoryController.LoadLobbyInventory();
        lobbyInventoryController.dragImage.gameObject.SetActive(false);
        lobbyInventoryController.dragImage.GetComponent<Image>().sprite = null;
        lobbyInventoryController.dragCode = 0;
    }
    bool MoveItem(Transform _from, Transform _to) {
        UIProperty to = _to.GetComponent<UIProperty>();
        UIProperty from = _from.GetComponent<UIProperty>();

        //����ĭ���� �κ�(������ ��)���� �̵�
        if (from.CompareTag("UI_Slot")) {
            print($"{from.GetComponent<EquipmentUI>().name} = null, {to.transform.parent.parent.GetComponent<InventoryProperty>().Target_Inventory}�� �κ��丮�� �������߰�");
            //from�� Ÿ������ ĳ������ ������� ����,
            string name = LobbyUIController.selected_dollinfo.name;
            //�κ��� ������ �߰�
            ItemInfo_compact equiped = new ItemInfo_compact(from.GetComponent<EquipmentUI>().equiped);
            to.inven.Add(equiped);
            //������� = null
            switch (from.GetComponent<EquipmentUI>().itemtype) {
                case Type.helmet:
                    characterInfoLoader.Characters[name].equipInfo.Helmet.itemcode = 0;
                    break;
                case Type.body:
                    characterInfoLoader.Characters[name].equipInfo.Armor.itemcode = 0;
                    break;
                case Type.bag:
                    characterInfoLoader.Characters[name].equipInfo.Bag.itemcode = 0;
                    break;
                case Type.pri:
                    characterInfoLoader.Characters[name].equipInfo.Primary.itemcode = 0;
                    break;
                case Type.sec:
                    characterInfoLoader.Characters[name].equipInfo.Secondary.itemcode = 0;
                    break;
            }
            aftermove();
            return true;
        }

        //���� ������
        if (from.GetComponent<Item>().code == to.GetComponent<Item>().code) {
            //print("���� ������");
            //������ ��ġ��
            if (LoadItemData.instance.GetItemData(to.GetComponent<Item>().code).stack == 1) {
                //print("��ĥ �� ���� ������");
                return false;
            }
            if (from.GetComponent<Item>().count + to.GetComponent<Item>().count <= LoadItemData.instance.GetItemData(to.GetComponent<Item>().code).stack) {
                //print($"{to.name}�� ���� += {from.name}�� ����");
                to.inven[to.index].itemcount += from.GetComponent<Item>().count;
                //print($"{from.name} ����");
                from.inven.RemoveAt(from.index);
                aftermove();
                return true;
            }
            else {
                int max = LoadItemData.instance.GetItemData(to.GetComponent<Item>().code).stack;
                //print($"from ���� {max - to.GetComponent<Item>().count}");
                from.inven[from.index].itemcount -= max - to.GetComponent<Item>().count;
                //print("to�� �ִ� ����" + max);
                to.inven[to.index].itemcount = max;
                aftermove();
                return true;
            }
        }
        //�ٸ� ������
        else {
            //print("�ٸ� ������");
            //������ �ѱ⿡ ���
            if (from.GetComponent<Item_Parts>() != null && to.GetComponent<Item_Weapon>() != null) {
                Debug.LogWarning("����-���� ���Ͽ��� �������� ���� Ȯ�� �ϱ�");
                Debug.LogWarning($"���� : {from.name}�� {from.transform.parent.parent.GetComponent<InventoryProperty>().Target_Inventory}���� ����, {to.name}�� ���� �߰�");
                Debug.LogWarning($"�Ұ��� : {from.name}�� {to.transform.parent.parent.GetComponent<InventoryProperty>().Target_Inventory}�� �߰�");
                return true;
            }
            if (from.transform.parent.parent.GetComponent<UIProperty>() != to.transform.parent.parent.GetComponent<UIProperty>()) {
                //print($"{from.name}�� {from.transform.parent.parent.GetComponent<InventoryProperty>().Target_Inventory}�� �κ��丮���� ������ ����, {to.transform.parent.parent.GetComponent<InventoryProperty>().Target_Inventory}�� �߰�");
                ItemInfo_compact moveitem = new ItemInfo_compact(from.GetComponent<UIProperty>().inven[from.GetComponent<UIProperty>().index]);
                //to �κ��� �߰�
                to.inven.Add(moveitem);
                //from �κ����� ����
                from.inven.RemoveAt(from.GetComponent<UIProperty>().index);
                aftermove();
                return true;
            }
        }
        return false;
    }
}
