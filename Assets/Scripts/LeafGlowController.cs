using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeafGlowController : MonoBehaviour {
    Material leafMaterial;
    private void Start()
    {
        leafMaterial = GetComponent<MeshRenderer>().materials[0];
    }
    
    void Update () {
        Color col = leafMaterial.GetColor("_TintColor");
        col.a = 0.3f + Mathf.Sin(Time.time) / 8;
        leafMaterial.SetColor("_TintColor", col);
    }
}
