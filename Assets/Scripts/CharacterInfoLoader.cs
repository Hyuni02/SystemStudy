using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.Experimental.AI;
using Mono.CompilerServices.SymbolWriter;

public struct EquipInfo {
    public int Primary;
    public int Secondary;
    public int Helmet;
    public int Armor;
    public int Bag;
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
            print("No Characters, Create AK-12");

            DollInfo ak12 = new DollInfo();
            ak12.name = "AK-12";
            ak12.equipInfo.Primary = 0;
            ItemInfo_compact itemInfo = new ItemInfo_compact();
            itemInfo.itemcode = 1001;
            itemInfo.itemcount = 10;
            ak12.inventory.Add(itemInfo);
            Characters.Add(ak12.name,ak12);
            SaveCharacterInfo(ak12.name);
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
    }
}
