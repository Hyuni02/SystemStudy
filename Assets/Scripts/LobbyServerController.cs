using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class LobbyServerController : MonoBehaviour
{
    public static LobbyServerController instance;

    public string selected = "";

    public Transform content;
    public GameObject prefab;

    private void Awake() {
        if(instance == null) {
            instance = this;
        }
        else {
            Destroy(this);
        }
    }
    void Start()
    {
        
    }

    public void OpenServer(string name = null) {
        for (int i = 0; i < content.childCount; i++) {
            Destroy(content.GetChild(i).gameObject);
        }

        ShowEXP();

        foreach (var character in CharacterInfoLoader.instance.Characters) {
            //������ ĳ���� �� ��ŭ �̹��� ����
            GameObject button = Instantiate(prefab, content);
            button.name = character.Key;
            //ĳ���� �̹��� �ֱ�
            button.GetComponent<Image>().sprite = CharacterInfoLoader.instance.Image_Characters[character.Key + "_server"];
            button.GetComponent<Button>().onClick.AddListener(() => SelectCharacter(character.Key));
        }
    }
    public void SelectCharacter(string name = null) {
        if (name == null)
            name = CharacterInfoLoader.instance.Characters.First().Key;
        selected = name;

        for(int i=0;i<content.childCount;i++) {
            ColorBlock cb = content.GetChild(i).GetComponent<Button>().colors;
            if(content.GetChild(i).name == selected) {
                cb.normalColor = Color.gray;
            }
            else {
                cb.normalColor = Color.white;
            }
        }

        print($"select {name}");
        Debug.LogWarning("������ ĳ���� ���� ǥ�� �̱���");
    }

    void ShowEXP() {
        Debug.LogWarning("���� ����ġ ǥ�� �̱���");
    }

    public void LevelUP_SelectedCharacter() {
        Debug.LogWarning("������ ĳ���� ���� �� �̱���");
    }
}
