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
        //�κ� �ִ� �뷮 ���
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

    //���� ĳ���� ����Ʈ
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
        //ĳ���� ���� �о����
        LoadCharacterInfo();

        //���� ù ����
        if(Characters.Count == 0) {
            print("No Characters, Create AK-12, AN-94");

            //�⺻���� ak12����
            DollInfo ak12 = new DollInfo();
            //�̸�����
            ak12.name = "AK-12";

            ak12.equipInfo.Helmet = null;
            ak12.equipInfo.Armor = null;
            //���� �ο�
            ItemInfo_compact bag = new ItemInfo_compact();
            bag.itemcount = 1;
            bag.itemcode = 3001;
            //TODO ���� �Ӽ��� �̺ο� ����
            ak12.equipInfo.Bag = bag;
            //�ֹ��� �ο�
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
            //���� ������ �ο�
            ItemInfo_compact itemInfo = new ItemInfo_compact();
            itemInfo.itemcode = 1001;
            itemInfo.itemcount = 16;
            ak12.inventory.Add(itemInfo);
            //����
            Characters.Add(ak12.name,ak12);
            SaveCharacterInfo(ak12.name);

            //�⺻���� an94����
            DollInfo an94 = new DollInfo();
            an94.name = "AN-94";
            an94.equipInfo.Helmet = null;
            //�� ����
            ItemInfo_compact armor2 = new ItemInfo_compact();
            armor2.itemcount = 1;
            armor2.itemcode = 5001;
            armor2.properties.Add(new StringInt("health", 40));
            an94.equipInfo.Armor = armor2;
            an94.equipInfo.Bag = null;
            //�ֹ��� ����
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
            //���� ������ �ο�
            ItemInfo_compact itemInfo2 = new ItemInfo_compact();
            itemInfo2.itemcode = 1002;
            itemInfo2.itemcount = 10;
            an94.inventory.Add(itemInfo2);
            //����
            Characters.Add(an94.name, an94);
            SaveCharacterInfo(an94.name);

            SceneManager.LoadScene("Lobby");
        }
    }

    /// <summary>
    /// ĳ���� ���̺� ���� �ҷ�����
    /// </summary>
    /// <param name="name">������ �� ��ü �ҷ�����, ���� �� ���� ĳ���͸� �ҷ�����</param>
    public void LoadCharacterInfo(string name = null) {
        //ĳ���� ���̺� ���� �ҷ�����
        DirectoryInfo dir = new DirectoryInfo($"{Application.dataPath}/Resources/Characters");

        //������ ĳ���� ������ �ҷ�����
        if (name != null) {
            string jdata = File.ReadAllText($"{Application.dataPath}/Resources/Characters/{name}.txt");
            DollInfo dollinfo = JsonUtility.FromJson<DollInfo>(jdata);
            if (Characters.ContainsKey(name)) {
                Characters.Remove(name);
            }
            Characters.Add(dollinfo.name, dollinfo);
            print($"{name}.txt loaded \n {jdata}");
        }
        //��� ĳ���� ���� �ҷ�����
        else {
            foreach (var savefile in dir.GetFiles()) {
                //��Ÿ ���� ����
                if (savefile.Name.Contains(".meta")) continue;
                //�̹��� ���� ����
                if (savefile.Name.Contains(".png")) continue;
                //ĳ���� ���̺� ����
                string jdata = File.ReadAllText($"{Application.dataPath}/Resources/Characters/{savefile.Name}");
                DollInfo dollinfo = JsonUtility.FromJson<DollInfo>(jdata);
                if (Characters.ContainsKey(dollinfo.name)) {
                    Characters.Remove(dollinfo.name);
                }
                Characters.Add(dollinfo.name, dollinfo);
                print($"{savefile.Name} loaded \n {jdata}");
            }
        }

        #region ĳ���� �̹��� ���� �ҷ�����
        Sprite[] sprites = Resources.LoadAll<Sprite>("Characters");
        foreach (var sprite in sprites) {
            if(Image_Characters.ContainsKey(sprite.name)) { 
                Image_Characters.Remove(sprite.name);
            }
            //�⺻���� : �̸�.png
            //�������� : �̸�_server.png
            Image_Characters.Add(sprite.name, sprite);
        }
        #endregion
    }

    /// <summary>
    /// ĳ���� ���� ����
    /// </summary>
    /// <param name="name">������ �� ��ü ����, ���� �� ���� ĳ���͸� ����</param>
    public void SaveCharacterInfo(string name = null) {
        //ĳ���� ���� �� ���� ĳ���͸� ����
        if (name != null) {
            DollInfo dollinfo = new DollInfo();
            dollinfo = Characters[name];
            string jdata2 = JsonUtility.ToJson(dollinfo);
            File.WriteAllText($"{Application.dataPath}/Resources/Characters/{name}.txt", jdata2);
            print($"Save {name} Data");
        }
        //ĳ���� ������ �� ��ü ����
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
