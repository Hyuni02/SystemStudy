using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameController : MonoBehaviour
{
    CanvasProperties_InGame canvasProperties_InGame;

    void Start()
    {
        canvasProperties_InGame = FindObjectOfType<CanvasProperties_InGame>();

        //debug
        if(RaidInfo.instance == null) {
            Debug.LogWarning("RaidInfo ������Ʈ�� ����");
            gameObject.AddComponent<RaidInfo>();
            GetComponent<RaidInfo>().MapName = "������ ��";
            GetComponent<RaidInfo>().CharacterName = "AK-12";
            GetComponent<RaidInfo>().MapLevel = "Debug";
            GetComponent<RaidInfo>().MapStage = "Debug";
        }

        canvasProperties_InGame.txt_CharacterName.SetText(RaidInfo.instance.CharacterName);
        canvasProperties_InGame.txt_MapName.SetText(RaidInfo.instance.MapName);

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
