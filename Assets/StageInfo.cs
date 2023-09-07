using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class StageInfo : MonoBehaviour
{
    public int stage;
    Transform PlayerSpawnPos;
    List<Transform> pos= new List<Transform>();

    public void Start() {
        init();
    }

    void init() {
        PlayerSpawnPos = GameObject.FindGameObjectsWithTag("PlayerSpawnPos").Where(x => x.transform.parent == transform).First().transform;
        foreach (Transform p in PlayerSpawnPos.GetComponentInChildren<Transform>()) {
            if (p == transform) continue;
            pos.Add(p);
        }
    }

    public void SpawnPlayer() {
        init();
        Debug.LogWarning("플레이어 생성 위치 지정 임시 구현");
        GameObject Player = GameObject.FindGameObjectWithTag("Player");
        Player.transform.position = pos[Random.Range(0, pos.Count)].position;
    }
}
