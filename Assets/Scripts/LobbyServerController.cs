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
            //보유한 캐릭터 수 만큼 이미지 생성
            GameObject button = Instantiate(prefab, content);
            button.name = character.Key;
            //캐릭터 이미지 넣기
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
        Debug.LogWarning("선택한 캐릭터 정보 표시 미구현");
    }

    void ShowEXP() {
        Debug.LogWarning("보유 경험치 표시 미구현");
    }

    public void LevelUP_SelectedCharacter() {
        Debug.LogWarning("선택한 캐릭터 레벨 업 미구현");
    }
}
