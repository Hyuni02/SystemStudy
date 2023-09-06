using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BiomeDetector : MonoBehaviour
{

    Ray ray = new Ray();
    RaycastHit hit;
    int layer = 1;

    public string inBiome;

    private void Start() {
        //InvokeRepeating(nameof(DetectBiome), 0.5f, 0.5f);
        layer = 1 << LayerMask.NameToLayer("Biome");
    }

    public void Update() {
        DetectBiome();
    }

    void DetectBiome() {
        ray = new Ray(transform.position, new Vector3(0,-1,0));
        
        if(Physics.Raycast(ray, out hit, 100f, layer)) {
            //print($"{hit.transform.gameObject.GetComponent<BiomeInfo>().biomeName}");
            inBiome = hit.transform.gameObject.GetComponent<BiomeInfo>().biomeName;
        }
    }
}
