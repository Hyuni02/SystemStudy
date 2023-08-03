using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapThumbnailLoader : MonoBehaviour
{
    public static MapThumbnailLoader instance;

    public Dictionary<string, Sprite> List_MapThumbnail = new Dictionary<string, Sprite>();

    private void Awake() {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
    }

    private void Start() {
        Sprite[] sprites = Resources.LoadAll<Sprite>("MapThumbnail");

        foreach (var sprite in sprites) {
            List_MapThumbnail.Add(sprite.name, sprite);
        }
    }

    public Sprite GetSprite(string code) {
        if (List_MapThumbnail.ContainsKey(code))
            return List_MapThumbnail[code];
        return null;
    }
}
