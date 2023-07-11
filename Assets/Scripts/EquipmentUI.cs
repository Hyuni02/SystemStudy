using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentUI : MonoBehaviour
{
    //캐릭터 인벤토리 표시 창에 있는 장착장비 칸에 적용 됨
    public Type itemtype = Type.helmet;
    public ItemInfo_compact equiped;
    void Start()
    {
        CheckFull();
    }

    public void CheckFull() {
        if (equiped.itemcode != 0) {
            GetComponent<UIProperty>().b_dropable = false;
            GetComponent<UIProperty>().b_dragable = true;
            Debug.LogWarning("장착 아이템 이미지 표시 미구현");
        }
        else { 
            GetComponent<UIProperty>().b_dropable = true;
            GetComponent<UIProperty>().b_dragable = false;
            GetComponent<Image>().sprite = null;
            Debug.LogWarning("빈 장착칸 배경 이미지 없음");
        }
    }
}
