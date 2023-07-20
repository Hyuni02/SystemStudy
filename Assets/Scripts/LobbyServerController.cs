using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyServerController : MonoBehaviour
{
    public string selected = "";
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void SelectCharacter(string name = null) {
        if(name == null)
            //name = getcom
        selected = name;
    }

    public void LevelUP_SelectedCharacter(string name) {

    }
}
