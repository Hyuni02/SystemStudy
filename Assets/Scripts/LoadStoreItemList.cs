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

    public Transform Panel_SaleList;

    public void Start() {
        UpdateStoreItemList();
    }

    public void UpdateStoreItemList() {

        List<Dictionary<string, object>> Data_Sale = CSVReader.Read("StoreItemsData");

        foreach (var item in Data_Sale) {
            //print(item["Code"].ToString() + " : " + item["Cost"].ToString());
            GameObject button = Instantiate(pre_Button);
            button.transform.SetParent(Panel_SaleList, false);
            button.name = $"{LoadItemData.instance.GetItemData(int.Parse(item["Code"].ToString())).name}({item["Code"].ToString()})";
            string itemName = GetComponent<LoadItemData>().Data_Item.Find(x => x.code == int.Parse(item["Code"].ToString())).name;
            button.transform.GetChild(0).GetComponent<TMP_Text>().SetText(itemName);
            button.transform.GetChild(1).GetComponent<TMP_Text>().SetText(item["Cost"].ToString());

            //버튼 이미지 가져오기
            button.GetComponent<Image>().sprite = GetComponent<ItemImageLoader>().GetSprite(int.Parse(item["Code"].ToString()));

            button.GetComponent<Button>().onClick.AddListener(() => ButtonClicked(button.name));
        }
        print("Load Shop Item List Successfully");
    }

    void ButtonClicked(string code) {
        print("Click : " + code);
        GetComponent<LobbyInventoryController>().selectedCode = int.Parse(code);

        //debug
        TMP_Text txt_name = GetComponent<LobbyInventoryController>().txt_Name;
        string itemName = GetComponent<LoadItemData>().Data_Item.Find(x => x.code == int.Parse(code)).name;
        txt_name.SetText(itemName);
    }
}
