using System.Collections;
using System.Collections.Generic;
using TMPro;
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
            button.name = item["Code"].ToString();
            button.transform.GetChild(0).GetComponent<TMP_Text>().SetText(item["Code"].ToString());
            button.transform.GetChild(1).GetComponent<TMP_Text>().SetText(item["Cost"].ToString());
            
            //버튼 이미지 가져오기
            Debug.LogWarning("Load Item Image is not realized");
            
            button.GetComponent<Button>().onClick.AddListener(() => ButtonClicked(button.name));
        }
        print("Load Shop Item List Successfully");
    }

    void ButtonClicked(string code) {
        print("Click : " + code);
    }
}
