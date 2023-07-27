using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StoreController : MonoBehaviour
{
    public int itemcount = 1;
    bool isSell = false;
    int totalcost = 0;

    [Header("Buy/Sell Panel")]
    public Transform Panel_BuySell;
    public Transform Image_Item;
    public TMP_Text Text_ItemName;
    public TMP_Text Text_ItemCost;
    public TMP_InputField InputField_Count;
    public Button Button_buySell;

    public void Close_Panel() {
        Panel_BuySell.gameObject.SetActive(false);
    }

    public void Open_Panel(bool _isSell = true) {
        transform.SetAsLastSibling();
        Panel_BuySell.gameObject.SetActive(true);
        Image_Item.GetComponent<Image>().sprite = GetComponent<LobbyInventoryController>().selectedButton.GetComponent<Image>().sprite;
        Text_ItemName.GetComponent<TMP_Text>().SetText(GetComponent<LobbyInventoryController>().selectedButton.GetComponent<Item>().name);
        isSell = _isSell;
        SetCount();
    }

    public void SetCount(int count = 0) {
        if (count == 0) itemcount = 1;
        itemcount += count;
        int max = 16;
        if(isSell) {
            //�κ��丮���� �ش�������� ã�� �� �������� ������ max�� ����
            max = GetComponent<LobbyInventoryController>().SaveData[GetComponent<LobbyInventoryController>().selectedButton.GetComponent<UIProperty>().index].itemcount;
        }
        else {
            //���� ������ ��Ʈ���� 1ȸ ���� �ִ� ������ max�� ����
            max = GetComponent<LoadStoreItemList>().GetItemLimit(GetComponent<LobbyInventoryController>().selectedCode);
        }
        itemcount = (int)Mathf.Repeat(itemcount, max + 1);
        InputField_Count.text = itemcount.ToString();
        //�ǸŰ��� ǥ��
        if (isSell) {
            totalcost = GetComponent<LoadStoreItemList>().GetItemPrice(GetComponent<LobbyInventoryController>().selectedCode, "Sell") * itemcount;
            Text_ItemCost.GetComponent<TMP_Text>().SetText($"�ǸŰ��� : {totalcost}");
        }
        //���԰��� ǥ��
        else {
            totalcost = GetComponent<LoadStoreItemList>().GetItemPrice(GetComponent<LobbyInventoryController>().selectedCode, "Buy") * itemcount;
            Text_ItemCost.GetComponent<TMP_Text>().SetText($"���԰��� : {totalcost}");
        }
        Target_Selected(isSell);
    }

    bool _isSell;
    public void Target_Selected(bool isSell) {
        _isSell = isSell;
        if(itemcount == 0) {
            Button_buySell.interactable = false;
        }
        else {
            Button_buySell.interactable = true;
        }

        //�Ǹ��� ������
        if (isSell) {
            Button_buySell.transform.GetChild(0).GetComponent<TMP_Text>().text = "Sell";
        }
        //������ ������
        else {
            Button_buySell.transform.GetChild(0).GetComponent<TMP_Text>().text = "Buy";
        }
    }

    public void Button_BuySell() {
        //�Ǹ��� ������
        if (_isSell) {
            print($"�Ǹ� : {GetComponent<LobbyInventoryController>().selectedButton.name}\n��ȭ +{totalcost}");
            GetComponent<LobbyInventoryController>().money += totalcost;
            RemoveItemFromInventroy();
        }
        //������ ������
        else {
            if(totalcost > GetComponent<LobbyInventoryController>().money) {
                print("��ȭ ����");
                return;
            }
            print($"���� : {GetComponent<LobbyInventoryController>().selectedButton.name}\n��ȭ -{Text_ItemCost.text}");
            GetComponent<LobbyInventoryController>().money -= totalcost;
            AddItemToInventory();
        }
        Close_Panel();
    }

    public void AddItemToInventory() {
        Debug.LogWarning("������ ȹ�� �̱���");
        //������ �߰�
        LobbyInventoryController inventoryController = GetComponent<LobbyInventoryController>();
        int selectedCode = inventoryController.selectedCode;
        int max = LoadItemData.instance.GetItemData(selectedCode).stack;
        int newitemgroup = itemcount / max; //������ �׷� ��
        int rests = itemcount % max; //������ ������ ��

        if (newitemgroup > 0) {
            for (int i = 0; i < newitemgroup; i++) {
                ItemInfo_compact newitem = new ItemInfo_compact();
                newitem.itemcode = selectedCode;
                newitem.itemcount = max;
                inventoryController.SetItemProps(ref newitem);
                inventoryController.SaveData.Add(newitem);
            }
        }
        if (rests > 0) {
            ItemInfo_compact restitem = new ItemInfo_compact();
            restitem.itemcode = selectedCode;
            restitem.itemcount = rests;
            inventoryController.SetItemProps(ref restitem);
            inventoryController.SaveData.Add(restitem);
        }
        //�κ��丮 ���ΰ�ħ
        inventoryController.ShowItems(ref inventoryController.Content_LobbyInventory, ref inventoryController.SaveData);
        //������ ����
        inventoryController.SaveLobbyInventory();
        inventoryController.LoadLobbyInventory();
    }

    public void RemoveItemFromInventroy() {
        //������ ����
        LobbyInventoryController inventoryController = GetComponent<LobbyInventoryController>();

        //������ �������� �κ��丮�� �ִ��� Ȯ��
        var target = inventoryController.SaveData[inventoryController.selectedButton.GetComponent<UIProperty>().index];

        //�ش� �������� ���� 1����
        target.itemcount -= itemcount;
        //������ �������� ���� 0�̸� ��ư �����
        if (target.itemcount == 0) {
            inventoryController.SaveData.Remove(target);
        }
        //�κ��丮 ���ΰ�ħ
        inventoryController.ShowItems(ref inventoryController.Content_LobbyInventory, ref inventoryController.SaveData);
        //������ ����
        inventoryController.SaveLobbyInventory();
        inventoryController.LoadLobbyInventory();
    }

}
