using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class ItemImageLoader : MonoBehaviour {

    public static ItemImageLoader instance;

    public Dictionary<int, Sprite> List_ItemImage = new Dictionary<int, Sprite>();


    private void Awake() {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
    }

    void Start() {
        Sprite[] sprites = Resources.LoadAll<Sprite>("ItemImage");

        foreach(var sprite in sprites) {
            int code = int.Parse(sprite.name.Split('_')[0]);
            List_ItemImage.Add(code, sprite);
        }
    }

    public Sprite GetSprite(int code) {
        if(List_ItemImage.ContainsKey(code)) 
            return List_ItemImage[code];
        return null;
    }

}
