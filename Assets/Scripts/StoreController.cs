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
            Debug.LogWarning("������ ȹ�� �̱���");
        }
        Close_Panel();
        GetComponent<LobbyInventoryController>().SaveLobbyInventory();
        GetComponent<LobbyInventoryController>().LoadLobbyInventory();
    }

    public void AddItemToInventory() {

    }

    public void RemoveItemFromInventroy() {
        //������ ����
        //������ �������� �κ��丮�� �ִ��� Ȯ��
        var target = GetComponent<LobbyInventoryController>().SaveData[GetComponent<LobbyInventoryController>().selectedButton.GetComponent<UIProperty>().index];

        //�ش� �������� ���� 1����
        target.itemcount--;
        //������ �������� ���� 0�̸� ��ư �����
        if (target.itemcount == 0) {
            GetComponent<LobbyInventoryController>().SaveData.Remove(target);
        }
        //�κ��丮 ���ΰ�ħ
        GetComponent<LobbyInventoryController>().ShowItems(ref GetComponent<LobbyInventoryController>().Content_LobbyInventory, ref GetComponent<LobbyInventoryController>().SaveData);
        //������ ����
        GetComponent<LobbyInventoryController>().SaveLobbyInventory();
        GetComponent<LobbyInventoryController>().LoadLobbyInventory();
    }

}
