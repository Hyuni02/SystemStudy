using System.Collections;
using System.Collections.Generic;
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

        //드래그 불가능한 오브젝트를 드래그 (상점 아이템 드래그)
        if(drag.GetComponent<UIProperty>().b_dragable == false) {
            print("드래그 불가능 대상");
            return;
        }

        //드랍할 수 없는 위치에 드랍 (상점 아이템 위)
        if (!b_dropable) {
            if(drop.GetComponent<EquipmentUI>()?.equiped != null) {
                print("해당 슬롯에는 이미 아이템이 존재함");
            }
            else {
                print("드랍 불가능 위치");
            }
            return;
        }

        bool trig = false;
        //장착칸에 드랍
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
        //인벤에 드랍
        else {
            Transform from = drag.transform;
            Transform to = drop.transform;

            //아이템 위에 드랍
            if (to.GetComponent<Item>() != null) {
                //같은 인벤에 존재
                if (from.parent.parent.GetComponent<UIProperty>() == to.parent.parent.GetComponent<UIProperty>() ||
                from.parent.parent.GetComponent<UIProperty>() == to.GetComponent<UIProperty>()) {
                    print("같은 인벤");
                    trig = MoveItem(from, to);
                }
                //다른 인벤에 존재
                else {
                    print("다른 인벤");
                    //용량이 가득차지 않는지 확인
                    Debug.LogWarning("용량 미확인");
                    trig = MoveItem(from, to);
                }
            }
            //빈칸에 드랍
            else {
                print("인벤의 빈칸에 드랍");
                //장착칸에서 인벤으로 이동
                if (from.CompareTag("UI_Slot")) {
                    print($"{from.GetComponent<EquipmentUI>().name} = null, {to.GetComponent<InventoryProperty>().Target_Inventory}의 인벤토리에 아이템추가");
                    return;
                }
                //인벤에서 인벤으로 이동
                else {
                    //다른 인벤의 빈칸
                    if (from.parent.parent.GetComponent<UIProperty>() != to.GetComponent<UIProperty>()) {
                        print($"{from.parent.parent.GetComponent<InventoryProperty>().Target_Inventory}의 인벤토리에서 아이템 제거, {to.GetComponent<InventoryProperty>().Target_Inventory}의 인벤토리에 아이템 추가");
                        return;
                    }
                }
            }

        }
        if(!trig) print("유효하지 않은 이동");
    }

    void aftermove() {
        if (LobbyUIController.instance.index != 0) {
            characterInfoLoader.SaveCharacterInfo(LobbyUIController.selected_dollinfo.name);
            characterInfoLoader.LoadCharacterInfo(LobbyUIController.selected_dollinfo.name);
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

        //같은 아이템
        if (from.GetComponent<Item>().code == to.GetComponent<Item>().code) {
            print("같은 아이템");
            //아이템 겹치기
            if (LoadItemData.instance.GetItemData(to.GetComponent<Item>().code).stack == 1) {
                print("겹칠 수 없는 아이템");
                return true;
            }
            if (from.GetComponent<Item>().count + to.GetComponent<Item>().count <= LoadItemData.instance.GetItemData(to.GetComponent<Item>().code).stack) {
                //print($"{to.name}의 스택 += {from.name}의 스택");
                to.inven[to.index].itemcount += from.GetComponent<Item>().count;
                //print($"{from.name} 삭제");
                from.inven.RemoveAt(from.index);
                aftermove();
                return true;
            }
            else {
                int max = LoadItemData.instance.GetItemData(to.GetComponent<Item>().code).stack;
                //print($"from 차감 {max - to.GetComponent<Item>().count}");
                from.inven[from.index].itemcount -= max - to.GetComponent<Item>().count;
                //print("to는 최대 스택" + max);
                to.inven[to.index].itemcount = max;
                aftermove();
                return true;
            }
        }
        //다른 아이템
        else {
            print("다른 아이템");
            //파츠를 총기에 드랍
            if (from.GetComponent<Item_Parts>() != null && to.GetComponent<Item_Weapon>() != null) {
                Debug.LogWarning("무기-파츠 파일에서 장착가능 여부 확인 하기");
                Debug.LogWarning($"가능 : {from.name}을 {from.transform.parent.parent.GetComponent<InventoryProperty>().Target_Inventory}에서 삭제, {to.name}에 파츠 추가");
                Debug.LogWarning($"불가능 : {from.name}을 {to.transform.parent.parent.GetComponent<InventoryProperty>().Target_Inventory}에 추가");
                return true;
            }
            if (from.transform.parent.parent.GetComponent<UIProperty>() != to.transform.parent.parent.GetComponent<UIProperty>()) {
                print($"{from.name}을 {from.transform.parent.parent.GetComponent<InventoryProperty>().Target_Inventory}의 인벤토리에서 아이템 제거, {to.transform.parent.parent.GetComponent<InventoryProperty>().Target_Inventory}에 추가");
                return true;
            }
        }
        return false;
    }
}
