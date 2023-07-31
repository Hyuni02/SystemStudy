using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CommandTableController : MonoBehaviour
{
    public static CommandTableController instance;

    int characterindex = 0;
    [Header("About Character")]
    public Image Image_SelectedCharacter;
    public TMP_Text Text_Name;
    public TMP_Text Text_Level;
    public Image Image_Helmet;
    public Image Image_Armor;
    public Image Image_Bag;
    public Image Image_Primary;
    public Image Image_Secondary;


    private void Awake() {
        if(instance == null) {
            instance = this;
        }
    }

    public void OpenCommandTable() {
        ShowCharacterInfo();
    }

    void ShowCharacterInfo(string _name = "AK-12") {
        //ĳ���� �̸� �ֱ�
        Text_Name.SetText(_name);

        //ĳ���� �̹��� �ֱ�
        Image_SelectedCharacter.sprite = CharacterInfoLoader.instance.Image_Characters[_name];

        //ĳ���� ���� �ֱ�
        Debug.LogWarning("ĳ���� ���� ǥ�� �̱���");

        //ĳ���� ���� ��� �ֱ�
        if (CharacterInfoLoader.instance.Characters[_name].equipInfo.Helmet.itemcode != 0)
            Image_Helmet.sprite = ItemImageLoader.instance.List_ItemImage[CharacterInfoLoader.instance.Characters[_name].equipInfo.Helmet.itemcode];
        else
            Image_Helmet.sprite = ItemImageLoader.instance.List_ItemImage[4000];
        if (CharacterInfoLoader.instance.Characters[_name].equipInfo.Armor.itemcode != 0)
            Image_Armor.sprite = ItemImageLoader.instance.List_ItemImage[CharacterInfoLoader.instance.Characters[_name].equipInfo.Armor.itemcode];
        else
            Image_Armor.sprite = ItemImageLoader.instance.List_ItemImage[5000];
        if (CharacterInfoLoader.instance.Characters[_name].equipInfo.Bag.itemcode != 0)
            Image_Bag.sprite = ItemImageLoader.instance.List_ItemImage[CharacterInfoLoader.instance.Characters[_name].equipInfo.Bag.itemcode];
        else
            Image_Bag.sprite = ItemImageLoader.instance.List_ItemImage[3000];
        if (CharacterInfoLoader.instance.Characters[_name].equipInfo.Primary.itemcode != 0)
            Image_Primary.sprite = ItemImageLoader.instance.List_ItemImage[CharacterInfoLoader.instance.Characters[_name].equipInfo.Primary.itemcode];
        else
            Image_Primary.sprite = ItemImageLoader.instance.List_ItemImage[2000];
        if (CharacterInfoLoader.instance.Characters[_name].equipInfo.Secondary.itemcode != 0)
            Image_Secondary.sprite = ItemImageLoader.instance.List_ItemImage[CharacterInfoLoader.instance.Characters[_name].equipInfo.Secondary.itemcode];
        else
            Image_Secondary.sprite = ItemImageLoader.instance.List_ItemImage[2100];
    }

    public void ChangeIndex(int i) {
        float next = Mathf.Repeat(characterindex + i, CharacterInfoLoader.instance.Characters.Count);
        characterindex += i;
        string name_next = CharacterInfoLoader.instance.Characters.Keys.ElementAt((int)next);
        ShowCharacterInfo(name_next);
    }

}
