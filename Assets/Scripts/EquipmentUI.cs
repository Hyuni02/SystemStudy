using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentUI : MonoBehaviour
{
    //ĳ���� �κ��丮 ǥ�� â�� �ִ� ������� ĭ�� ���� ��
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
            Debug.LogWarning("���� ������ �̹��� ǥ�� �̱���");
        }
        else { 
            GetComponent<UIProperty>().b_dropable = true;
            GetComponent<UIProperty>().b_dragable = false;
            GetComponent<Image>().sprite = null;
            Debug.LogWarning("�� ����ĭ ��� �̹��� ����");
        }
    }
}
