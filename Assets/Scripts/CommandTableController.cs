using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CommandTableController : MonoBehaviour
{
    public static CommandTableController instance;

    int characterindex = 0;
    int preview_level = 0;
    int selected_level = 0;
    int selected_stage = 0;

    public GameObject Panel_Detail;
    public GameObject Panel_Summary;
    public Transform summary_content;
    public GameObject Map_Preview;
    [Header("About Map")]
    public Image Image_MapThumbnail;
    public TMP_Text Text_MapName;

    [Header("About Character")]
    public Image Image_SelectedCharacter;
    public TMP_Text Text_Name;
    public TMP_Text Text_Level;
    public Image Image_Helmet;
    public Image Image_Armor;
    public Image Image_Bag;
    public Image Image_Primary;
    public Image Image_Secondary;

    public List<Dictionary<string, object>> List_Map =  new List<Dictionary<string, object>>();

    private void Awake() {
        if(instance == null) {
            instance = this;
        }
    }

    private void Start() {
        List_Map = CSVReader.Read("StageList");
    }

    public void OpenCommandTable() {
        Panel_Detail.SetActive(false);
    }

    public void SetPreviewLevel(int level) {
        preview_level = level;
    }
    public void OpenClose_SummaryTab(bool open) {
        if (Panel_Detail.activeInHierarchy) return;
        if(open) {
            ClearContent(summary_content);
            foreach(var map in List_Map) {
                if ((int)map["level"] == preview_level) {
                    GameObject maptile = Instantiate(Map_Preview, summary_content);
                    maptile.transform.GetComponentInChildren<TMP_Text>().SetText(map["name"].ToString());
                    maptile.transform.GetChild(0).GetComponent<Image>().sprite = MapThumbnailLoader.instance.GetSprite($"{preview_level}-{map["stage"]}");
                }
            }
            Panel_Summary.SetActive(true);
        }
        else {
            Panel_Summary.SetActive(false);
        }
    }

    void ClearContent(Transform content) {
        for (int i = 0; i < content.childCount; i++) {
            Destroy(content.GetChild(i).gameObject);
        }
    }

    public void OpenDetailTab(int stage = 0) {
        foreach(var map in List_Map) {
            if ((int)map["level"] == selected_level && (int)map["stage"] == stage) {
                Text_MapName.SetText(map["name"].ToString());
                Image_MapThumbnail.sprite = MapThumbnailLoader.instance.GetSprite($"{selected_level}-{selected_stage}");
            }
        }
        Panel_Summary.SetActive(false);
        Panel_Detail.SetActive(true);
        ShowCharacterInfo();
    }

    public void CloseDetailTab() {
        Panel_Detail.SetActive(false);
    }

    void ShowCharacterInfo(string _name = "AK-12") {
        //ĳ���� �̸� �ֱ�
        Text_Name.SetText(_name);

        //ĳ���� �̹��� �ֱ�
        Image_SelectedCharacter.sprite = CharacterInfoLoader.instance.Image_Characters[_name];

        //ĳ���� ���� �ֱ�
        Debug.LogWarning("ĳ���� ���� ǥ�� �̱���");

        //ĳ���� ���� ��� �ֱ�
        #region
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
        #endregion
    }

    public void RaidStart() {
        //level�� stage�� �̿��ؼ� �� �����Ϳ��� ã��
        Debug.LogWarning("���̵� ���� ������ �Ѱ��ֱ� �̱���");
        SceneManager.LoadScene("InGame");
    }

    public void SetLevel(int level) {
        selected_level = level;
        selected_stage = 0;
        if (Panel_Detail.activeSelf) {
            OpenDetailTab();
        }
    }

    public void ChangeStage(int i) {
        float next = Mathf.Repeat(selected_stage + i, List_Map.Where(x => (int)x["level"] == selected_level).Count());
        selected_stage = (int)next;
        OpenDetailTab((int)next);
    }

    public void ChangeIndex(int i) {
        float next = Mathf.Repeat(characterindex + i, CharacterInfoLoader.instance.Characters.Count);
        characterindex = (int)next;
        string name_next = CharacterInfoLoader.instance.Characters.Keys.ElementAt((int)next);
        ShowCharacterInfo(name_next);
    }

}
