using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StageInfo : MonoBehaviour
{
    public int stage;
    public Transform PlayerSpawnPos;

    public void Start() {
        PlayerSpawnPos = GameObject.FindGameObjectsWithTag("PlayerSpawnPos").Where(x => x.transform.parent == transform).First().transform;
    }
}
