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

        Debug.LogWarning("RaidInfo에 맞는 맵을 프리팹에서 생성하는 기능 미구현");

        GameObject Map = GameObject.FindGameObjectWithTag("Map");


        //debug
        if(RaidInfo.instance == null) {
            Debug.LogWarning("RaidInfo 오브젝트가 없음");
            gameObject.AddComponent<RaidInfo>();
            GetComponent<RaidInfo>().MapName = "연습용 맵";
            GetComponent<RaidInfo>().CharacterName = "AK-12";
            GetComponent<RaidInfo>().MapLevel = 0;
            GetComponent<RaidInfo>().MapStage = 1;
        }

        //플레이어를 생성할 지역 검색
        StageInfo[] Stages = Map.GetComponentsInChildren<StageInfo>();
        StageInfo selectedStage = Stages.Where(x => x.stage == RaidInfo.instance.MapStage).FirstOrDefault();
        if (selectedStage == null) {
            Debug.LogWarning($"스테이지 {RaidInfo.instance.MapStage} 없음");
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
