using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentUI : MonoBehaviour
{
    //ĳ���� �κ��丮 ǥ�� â�� �ִ� ������� ĭ�� ���� ��
    public Type itemtype = Type.helmet;
    public ItemInfo_compact equiped;
    public bool isParts = false;

    void OnEnable()
    {
        CheckFull();
    }

    public void CheckFull() {
        if (equiped.itemcode != 0) {
            GetComponent<UIProperty>().b_dropable = false;
            GetComponent<UIProperty>().b_dragable = true;
            GetComponent<Image>().sprite = ItemImageLoader.instance.GetSprite(equiped.itemcode);
        }
        else { 
            GetComponent<UIProperty>().b_dropable = true;
            GetComponent<UIProperty>().b_dragable = false;
            GetComponent<Image>().sprite = null;
            int tempcode = 0000;

            if (!isParts) {
                switch (itemtype) {
                    case Type.helmet:
                        tempcode = 4000;
                        break;
                    case Type.body:
                        tempcode = 5000;
                        break;
                    case Type.bag:
                        tempcode = 3000;
                        break;
                    case Type.pri:
                        tempcode = 2000;
                        break;
                    case Type.sec:
                        tempcode = 2100;
                        break;
                }
            }
            else {
                Debug.LogWarning("���� ����ĭ ��� �̹��� ����");
            }

            GetComponent<Image>().sprite = ItemImageLoader.instance.GetSprite(tempcode);
        }
    }
}
