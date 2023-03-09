using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyCameraController : MonoBehaviour
{
    public Transform[] rooms;
    int i = 3;
    Vector3 velo = Vector3.zero;

    public Transform FocusPos;

    void Start()
    {
    }

    void Update()
    {
        if (FocusPos == null)
            transform.position = Vector3.SmoothDamp(transform.position, rooms[i].Find("CamPos").position, ref velo, 0.1f);
        else
            transform.position = Vector3.SmoothDamp(transform.position, FocusPos.position, ref velo, 0.1f);
    }

    public void MoveLobbyCam(int _index) {
        i = _index;
    }
}
