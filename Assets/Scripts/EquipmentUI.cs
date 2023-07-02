using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentUI : MonoBehaviour
{
    //ĳ���� �κ��丮 ǥ�� â�� �ִ� ������� ĭ�� ���� ��
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
            Debug.LogWarning("�� ����ĭ ��� �̹��� ����");
        }
    }
}
