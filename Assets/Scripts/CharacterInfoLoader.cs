using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.Experimental.AI;
using Mono.CompilerServices.SymbolWriter;
using System;

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
    public EquipInfo equipInfo = new EquipInfo();
    public List<ItemInfo_compact> inventory = new List<ItemInfo_compact>();
}

public class CharacterInfoLoader : MonoBehaviour
{
    //보유 캐릭터 리스트
    public Dictionary<string, DollInfo> Characters = new Dictionary<string, DollInfo>();

    void Start()
    {
        //캐릭터 정보 읽어오기
        LoadCharacterInfo();

        if(Characters.Count == 0) {
            print("No Characters, Create AK-12, AN-94");

            //기본인형 ak12지급
            DollInfo ak12 = new DollInfo();
            //이름설정
            ak12.name = "AK-12";
            //주무기 부여
            ItemInfo_compact primary = new ItemInfo_compact();
            primary.itemcount = 1;
            primary.itemcode = 2001;
            ak12.equipInfo.Primary = primary;
            //보유 아이템 부여
            ItemInfo_compact itemInfo = new ItemInfo_compact();
            itemInfo.itemcode = 1001;
            itemInfo.itemcount = 10;
            ak12.inventory.Add(itemInfo);
            //저장
            Characters.Add(ak12.name,ak12);
            SaveCharacterInfo(ak12.name);

            //기본인형 an94지급
            DollInfo an94 = new DollInfo();
            an94.name = "AN-94";
            //주무기 지급
            ItemInfo_compact primary2 = new ItemInfo_compact();
            primary2.itemcount = 1;
            primary2.itemcode = 2002;
            an94.equipInfo.Primary = primary2;
            //보유 아이템 부여
            ItemInfo_compact itemInfo2 = new ItemInfo_compact();
            itemInfo2.itemcode = 1002;
            itemInfo2.itemcount = 10;
            an94.inventory.Add(itemInfo2);
            //저장
            Characters.Add(an94.name, an94);
            SaveCharacterInfo(an94.name);
        }
    }

    void LoadCharacterInfo() {
        DirectoryInfo dir = new DirectoryInfo($"{Application.dataPath}/Resources/Characters");
        foreach(var savefile in dir.GetFiles()) {
            if (savefile.Name.Contains(".meta")) continue;
            string jdata = File.ReadAllText($"{Application.dataPath}/Resources/Characters/{savefile.Name}");
            DollInfo dollinfo = JsonUtility.FromJson<DollInfo>(jdata);
            Characters.Add(dollinfo.name, dollinfo);
            print($"{savefile.Name} loaded \n {jdata}");
        }
    }

    void SaveCharacterInfo(string name = null) {
        if (name != null) {
            DollInfo dollinfo = new DollInfo();
            dollinfo = Characters[name];
            string jdata2 = JsonUtility.ToJson(dollinfo);
            File.WriteAllText($"{Application.dataPath}/Resources/Characters/{name}.txt", jdata2);
            print($"Save {name} Data");
        }
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
