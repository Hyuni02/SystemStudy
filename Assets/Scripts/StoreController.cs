using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StoreController : MonoBehaviour
{
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

    public void Open_Panel(bool isSell = true) {
        transform.SetAsLastSibling();
        Panel_BuySell.gameObject.SetActive(true);
        Image_Item.GetComponent<Image>().sprite = GetComponent<LobbyInventoryController>().selectedButton.GetComponent<Image>().sprite;
        Text_ItemName.GetComponent<TMP_Text>().SetText(GetComponent<LobbyInventoryController>().selectedButton.GetComponent<Item>().name);
        //판매가격 표시
        if(isSell) {
            Text_ItemCost.GetComponent<TMP_Text>().SetText($"판매가격 : {GetComponent<LoadStoreItemList>().GetItemPrice(GetComponent<LobbyInventoryController>().selectedCode, "Sell")}");
        }
        //구입가격 표시
        else {
            Text_ItemCost.GetComponent<TMP_Text>().SetText($"구입가격 : {GetComponent<LoadStoreItemList>().GetItemPrice(GetComponent<LobbyInventoryController>().selectedCode, "Buy")}");
        }
        Target_Selected(isSell);
    }

    public void SetCount(int count) {

    }

    bool _isSell;
    public void Target_Selected(bool isSell) {
        _isSell = isSell;
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
        Debug.LogWarning("아이템 수량 선택 미구현");
        //판매할 아이템
        if (_isSell) {
            Debug.LogWarning("판매 미구현");
        }
        //구입할 아이템
        else {
            Debug.LogWarning("구입 미구현");
        }
    }

}
