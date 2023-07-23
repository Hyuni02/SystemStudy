using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StoreController : MonoBehaviour
{
    [Header("Buy/Sale Panel")]
    public Transform Panel_BuySale;
    public Transform Image_Item;
    public TMP_Text Text_ItemName;
    public TMP_Text Text_ItemCost;
    public TMP_InputField InputField_Count;
    public Button Button_buysale;

    public void Close_Panel() {
        Panel_BuySale.gameObject.SetActive(false);
    }

    public void Open_Panel(bool isSale = true) {
        transform.SetAsLastSibling();
        Panel_BuySale.gameObject.SetActive(true);
        Image_Item.GetComponent<Image>().sprite = GetComponent<LobbyInventoryController>().selectedButton.GetComponent<Image>().sprite;
        Text_ItemName.GetComponent<TMP_Text>().SetText(GetComponent<LobbyInventoryController>().selectedButton.GetComponent<Item>().name);
        Debug.LogWarning("가격 표시 미구현");
        Text_ItemCost.GetComponent<TMP_Text>().SetText($"가격 : ???");
        Target_Selected(isSale);
    }

    public void SetCount(int count) {

    }

    bool _isSale;
    public void Target_Selected(bool isSale) {
        _isSale = isSale;
        //판매할 아이템
        if (isSale) {
            Button_buysale.transform.GetChild(0).GetComponent<TMP_Text>().text = "Sale";
        }
        //구입할 아이템
        else {
            Button_buysale.transform.GetChild(0).GetComponent<TMP_Text>().text = "Buy";
        }
    }

    public void Button_BuySale() {
        //판매할 아이템
        if (_isSale) {
            Debug.LogWarning("판매 미구현");
        }
        //구입할 아이템
        else {
            Debug.LogWarning("구입 미구현");
        }
    }

}
