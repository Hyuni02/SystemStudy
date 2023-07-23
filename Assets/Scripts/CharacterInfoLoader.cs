using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.Experimental.AI;
using Mono.CompilerServices.SymbolWriter;
using System;
using UnityEngine.SceneManagement;
using static UnityEditor.Progress;

[Serializable]
public class EquipInfo {
    public ItemInfo_compact Primary;
    public ItemInfo_compact Secondary;
    public ItemInfo_compact Helmet;
    public ItemInfo_compact Armor;
    public ItemInfo_compact Bag;
}

public class DollInfo {
    public string name;
    public float minCapacity;
    public EquipInfo equipInfo = new EquipInfo();
    public List<ItemInfo_compact> inventory = new List<ItemInfo_compact>();

    public float Update_InvenCapacity() {
        //인벤 최대 용량 계산
        if(equipInfo.Bag == null) {
            return 25;
        }
        else {
            //maxCapacity = 25 + equipInfo.Bag.properties[]
            return 100;
        }
    }
}

public class CharacterInfoLoader : MonoBehaviour
{
    public static CharacterInfoLoader instance;

    //보유 캐릭터 리스트
    public Dictionary<string, DollInfo> Characters = new Dictionary<string, DollInfo>();
    public Dictionary<string, Sprite> Image_Characters = new Dictionary<string, Sprite>();

    private void Awake() {
        if (instance == null) {
            instance = this;
        }
        else {
            Destroy(this);
        }
    }

    void Start()
    {
        //캐릭터 정보 읽어오기
        LoadCharacterInfo();

        //게임 첫 시작
        if(Characters.Count == 0) {
            print("No Characters, Create AK-12, AN-94");

            //기본인형 ak12지급
            DollInfo ak12 = new DollInfo();
            //이름설정
            ak12.name = "AK-12";

            ak12.equipInfo.Helmet = null;
            ak12.equipInfo.Armor = null;
            //가방 부여
            ItemInfo_compact bag = new ItemInfo_compact();
            bag.itemcount = 1;
            bag.itemcode = 3001;
            //TODO 가방 속성값 미부여 상태
            ak12.equipInfo.Bag = bag;
            //주무기 부여
            ItemInfo_compact primary = new ItemInfo_compact();
            primary.itemcount = 1;
            primary.itemcode = 2001;
            primary.properties.Add(new StringInt("muzzle", 0));
            primary.properties.Add(new StringInt("under_rail", 0));
            primary.properties.Add(new StringInt("side_rail", 0));
            primary.properties.Add(new StringInt("upper_rail", 0));
            primary.properties.Add(new StringInt("magazine", 0));
            primary.properties.Add(new StringInt("stock", 0));
            ak12.equipInfo.Primary = primary;
            ak12.equipInfo.Secondary = null;
            //보유 아이템 부여
            ItemInfo_compact itemInfo = new ItemInfo_compact();
            itemInfo.itemcode = 1001;
            itemInfo.itemcount = 16;
            ak12.inventory.Add(itemInfo);
            //저장
            Characters.Add(ak12.name,ak12);
            SaveCharacterInfo(ak12.name);

            //기본인형 an94지급
            DollInfo an94 = new DollInfo();
            an94.name = "AN-94";
            an94.equipInfo.Helmet = null;
            //방어구 지급
            ItemInfo_compact armor2 = new ItemInfo_compact();
            armor2.itemcount = 1;
            armor2.itemcode = 5001;
            armor2.properties.Add(new StringInt("health", 40));
            an94.equipInfo.Armor = armor2;
            an94.equipInfo.Bag = null;
            //주무기 지급
            ItemInfo_compact primary2 = new ItemInfo_compact();
            primary2.itemcount = 1;
            primary2.itemcode = 2002;
            primary2.properties.Add(new StringInt("muzzle", 0));
            primary2.properties.Add(new StringInt("under_rail", 0));
            primary2.properties.Add(new StringInt("side_rail", 0));
            primary2.properties.Add(new StringInt("upper_rail", 0));
            primary2.properties.Add(new StringInt("magazine", 0));
            primary2.properties.Add(new StringInt("stock", 0));
            an94.equipInfo.Primary = primary2;
            an94.equipInfo.Secondary = null;
            //보유 아이템 부여
            ItemInfo_compact itemInfo2 = new ItemInfo_compact();
            itemInfo2.itemcode = 1002;
            itemInfo2.itemcount = 10;
            an94.inventory.Add(itemInfo2);
            //저장
            Characters.Add(an94.name, an94);
            SaveCharacterInfo(an94.name);

            SceneManager.LoadScene("Lobby");
        }
    }

    /// <summary>
    /// 캐릭터 세이브 파일 불러오기
    /// </summary>
    /// <param name="name">미지정 시 전체 불러오기, 지정 시 지정 캐릭터만 불러오기</param>
    public void LoadCharacterInfo(string name = null) {
        //캐릭터 세이브 파일 불러오기
        DirectoryInfo dir = new DirectoryInfo($"{Application.dataPath}/Resources/Characters");

        //지정한 캐릭터 정보만 불러오기
        if (name != null) {
            string jdata = File.ReadAllText($"{Application.dataPath}/Resources/Characters/{name}.txt");
            DollInfo dollinfo = JsonUtility.FromJson<DollInfo>(jdata);
            if (Characters.ContainsKey(name)) {
                Characters.Remove(name);
            }
            Characters.Add(dollinfo.name, dollinfo);
            print($"{name}.txt loaded \n {jdata}");
        }
        //모든 캐릭터 정보 불러오기
        else {
            foreach (var savefile in dir.GetFiles()) {
                //메타 파일 무시
                if (savefile.Name.Contains(".meta")) continue;
                //이미지 파일 무시
                if (savefile.Name.Contains(".png")) continue;
                //캐릭터 세이브 파일
                string jdata = File.ReadAllText($"{Application.dataPath}/Resources/Characters/{savefile.Name}");
                DollInfo dollinfo = JsonUtility.FromJson<DollInfo>(jdata);
                if (Characters.ContainsKey(dollinfo.name)) {
                    Characters.Remove(dollinfo.name);
                }
                Characters.Add(dollinfo.name, dollinfo);
                print($"{savefile.Name} loaded \n {jdata}");
            }
        }

        #region 캐릭터 이미지 파일 불러오기
        Sprite[] sprites = Resources.LoadAll<Sprite>("Characters");
        foreach (var sprite in sprites) {
            if(Image_Characters.ContainsKey(sprite.name)) { 
                Image_Characters.Remove(sprite.name);
            }
            //기본사진 : 이름.png
            //서버사진 : 이름_server.png
            Image_Characters.Add(sprite.name, sprite);
        }
        #endregion
    }

    /// <summary>
    /// 캐릭터 정보 저장
    /// </summary>
    /// <param name="name">미지정 시 전체 저장, 지정 시 지정 캐릭터만 저장</param>
    public void SaveCharacterInfo(string name = null) {
        //캐릭터 지정 시 지정 캐릭터만 저장
        if (name != null) {
            DollInfo dollinfo = new DollInfo();
            dollinfo = Characters[name];
            string jdata2 = JsonUtility.ToJson(dollinfo);
            File.WriteAllText($"{Application.dataPath}/Resources/Characters/{name}.txt", jdata2);
            print($"Save {name} Data");
        }
        //캐릭터 미지정 시 전체 저장
        else {
            foreach(var character in Characters) {
                DollInfo dollinfo = new DollInfo();
                dollinfo = character.Value;
                string jdata2 = JsonUtility.ToJson(dollinfo);
                File.WriteAllText($"{Application.dataPath}/Resources/Characters/{name}.txt", jdata2);
                print($"Save {name} Data");
            }
        }
    }
}
