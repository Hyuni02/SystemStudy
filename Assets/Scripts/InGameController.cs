using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class InGameController : MonoBehaviour
{
    CanvasProperties_InGame canvasProperties_InGame;

    void Start()
    {
        canvasProperties_InGame = FindObjectOfType<CanvasProperties_InGame>();

        Debug.LogWarning("RaidInfo�� �´� ���� �����տ��� �����ϴ� ��� �̱���");

        GameObject Map = GameObject.FindGameObjectWithTag("Map");


        //debug
        if(RaidInfo.instance == null) {
            Debug.LogWarning("RaidInfo ������Ʈ�� ����");
            gameObject.AddComponent<RaidInfo>();
            GetComponent<RaidInfo>().MapName = "������ ��";
            GetComponent<RaidInfo>().CharacterName = "AK-12";
            GetComponent<RaidInfo>().MapLevel = 0;
            GetComponent<RaidInfo>().MapStage = 1;
        }

        //�÷��̾ ������ ���� �˻�
        StageInfo[] Stages = Map.GetComponentsInChildren<StageInfo>();
        StageInfo selectedStage = Stages.Where(x => x.stage == RaidInfo.instance.MapStage).FirstOrDefault();
        if (selectedStage == null) {
            Debug.LogWarning($"�������� {RaidInfo.instance.MapStage} ����");
        }
        selectedStage.SpawnPlayer();

        canvasProperties_InGame.txt_CharacterName.SetText(RaidInfo.instance.CharacterName);
        canvasProperties_InGame.txt_MapName.SetText(RaidInfo.instance.MapName);

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
