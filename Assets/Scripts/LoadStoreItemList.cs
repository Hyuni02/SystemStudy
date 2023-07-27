using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class LoadStoreItemList : MonoBehaviour
{
    [Header("Prefab")]
    public GameObject pre_Button;

    public Transform Panel_SellList;
    public List<Dictionary<string, object>> Data_Sell;

    public void Start() {
        Data_Sell = CSVReader.Read("StoreItemsData");
        UpdateStoreItemList();
    }

    public int GetItemPrice(int code, string type) {
        foreach(var item in Data_Sell) {
            if ((int)item["Code"] == code) {
                return (int)item[type];
            }
        }
        Debug.LogError("아이템 가격 검색 실패");
        return -1;
    }

    public int GetItemLimit(int code) {
        foreach (var item in Data_Sell) {
            if ((int)item["Code"] == code) {
                return (int)item["limit"];
            }
        }
        Debug.LogError("아이템 수량 검색 실패");
        return -1;
    }

    public void UpdateStoreItemList() {
        foreach (var item in Data_Sell) {
            //print(item["Code"].ToString() + " : " + item["Buy"].ToString());
            GameObject button = Instantiate(pre_Button);
            button.transform.SetParent(Panel_SellList, false);
            button.AddComponent<Item>();
            button.GetComponent<Item>().itemname = LoadItemData.instance.GetItemData(int.Parse(item["Code"].ToString())).name;
            button.GetComponent<Item>().code = int.Parse(item["Code"].ToString());
            button.name = button.GetComponent<Item>().itemname;
            button.transform.GetChild(0).GetComponent<TMP_Text>().SetText(button.GetComponent<Item>().name);
            int price = GetItemPrice(button.GetComponent<Item>().code, "Buy");
            button.transform.GetChild(1).GetComponent<TMP_Text>().SetText(price.ToString());

            //버튼 이미지 가져오기
            button.GetComponent<Image>().sprite = GetComponent<ItemImageLoader>().GetSprite(int.Parse(item["Code"].ToString()));

            button.GetComponent<Button>().onClick.AddListener(() => ButtonClicked(button));
        }
        print("Load Shop Item List Successfully");
    }

    void ButtonClicked(GameObject button) {
        print("Click : " + button.GetComponent<Item>().name);
        GetComponent<LobbyInventoryController>().selectedCode = button.GetComponent<Item>().code;
        GetComponent<LobbyInventoryController>().selectedButton = button;
        if (LobbyUIController.instance.index == 0)
            GetComponent<StoreController>().Open_Panel(false);

        //debug
        //TMP_Text txt_name = GetComponent<LobbyInventoryController>().txt_Name;
        //string itemName = GetComponent<LoadItemData>().Data_Item.Find(x => x.code == int.Parse(code)).name;
        //txt_name.SetText(itemName);
    }
}
