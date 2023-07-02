using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentUI : MonoBehaviour
{
    //캐릭터 인벤토리 표시 창에 있는 장착장비 칸에 적용 됨
    GameObject text;
    ItemInfo_compact equiped;
    void Start()
    {
        text = transform.GetChild(0).gameObject;
        CheckFull();
    }

    public void CheckFull() {
        if (equiped != null) {
            text.SetActive(false);
            GetComponent<UIProperty>().b_dropable = false;
        }
        else { 
            text.SetActive(true);
            GetComponent<UIProperty>().b_dropable = true;
            GetComponent<Image>().sprite = null;
            Debug.LogWarning("빈 장착칸 배경 이미지 없음");
        }
    }
}
