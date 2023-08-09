using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaidInfo : MonoBehaviour
{
    public static RaidInfo instance;

    public string MapName;
    public string MapCode;
    public string CharacterName;

    private void Awake() {
        if (instance != null)
            Destroy(instance);
        instance = this;
    }

    private void Start() {
        DontDestroyOnLoad(gameObject);
    }
}
