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
            //인벤토리에서 해당아이템을 찾고 그 아이템의 수량을 max로 설정
            max = GetComponent<LobbyInventoryController>().SaveData[GetComponent<LobbyInventoryController>().selectedButton.GetComponent<UIProperty>().index].itemcount;
        }
        else {
            //상점 아이템 시트에서 1회 구매 최대 수량을 max로 설정
            max = GetComponent<LoadStoreItemList>().GetItemLimit(GetComponent<LobbyInventoryController>().selectedCode);
        }
        itemcount = (int)Mathf.Repeat(itemcount, max + 1);
        InputField_Count.text = itemcount.ToString();
        //판매가격 표시
        if (isSell) {
            totalcost = GetComponent<LoadStoreItemList>().GetItemPrice(GetComponent<LobbyInventoryController>().selectedCode, "Sell") * itemcount;
            Text_ItemCost.GetComponent<TMP_Text>().SetText($"판매가격 : {totalcost}");
        }
        //구입가격 표시
        else {
            totalcost = GetComponent<LoadStoreItemList>().GetItemPrice(GetComponent<LobbyInventoryController>().selectedCode, "Buy") * itemcount;
            Text_ItemCost.GetComponent<TMP_Text>().SetText($"구입가격 : {totalcost}");
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

        //판매할 아이템
        if (isSell) {
            Button_buySell.transform.GetChild(0).GetComponent<TMP_Text>().text = "Sell";
        }
        //구입할 아이템
        else {
            Button_buySell.transform.GetChild(0).GetComponent<TMP_Text>().text = "Buy";
        }
    }

    public void Button_BuySell() {
        //판매할 아이템
        if (_isSell) {
            print($"판매 : {GetComponent<LobbyInventoryController>().selectedButton.name}\n재화 +{totalcost}");
            GetComponent<LobbyInventoryController>().money += totalcost;
            RemoveItemFromInventroy();
        }
        //구입할 아이템
        else {
            if(totalcost > GetComponent<LobbyInventoryController>().money) {
                print("재화 부족");
                return;
            }
            print($"구입 : {GetComponent<LobbyInventoryController>().selectedButton.name}\n재화 -{Text_ItemCost.text}");
            GetComponent<LobbyInventoryController>().money -= totalcost;
            Debug.LogWarning("아이템 획득 미구현");
        }
        Close_Panel();
        GetComponent<LobbyInventoryController>().SaveLobbyInventory();
        GetComponent<LobbyInventoryController>().LoadLobbyInventory();
    }

    public void AddItemToInventory() {

    }

    public void RemoveItemFromInventroy() {
        //아이템 제거
        //선택한 아이템이 인벤토리에 있는지 확인
        var target = GetComponent<LobbyInventoryController>().SaveData[GetComponent<LobbyInventoryController>().selectedButton.GetComponent<UIProperty>().index];

        //해당 아이템의 수를 1차감
        target.itemcount--;
        //차감된 아이템의 수가 0이면 버튼 지우기
        if (target.itemcount == 0) {
            GetComponent<LobbyInventoryController>().SaveData.Remove(target);
        }
        //인벤토리 새로고침
        GetComponent<LobbyInventoryController>().ShowItems(ref GetComponent<LobbyInventoryController>().Content_LobbyInventory, ref GetComponent<LobbyInventoryController>().SaveData);
        //데이터 저장
        GetComponent<LobbyInventoryController>().SaveLobbyInventory();
        GetComponent<LobbyInventoryController>().LoadLobbyInventory();
    }

}
