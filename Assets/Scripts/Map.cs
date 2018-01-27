using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour {

    Color originalColor;
    MeshRenderer m_renderer;

    // Use this for initialization
    void Start () {
        m_renderer = GetComponent<MeshRenderer>();
        originalColor = m_renderer.material.color;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnMouseOver()
    {
        m_renderer.material.color = Color.blue;
    }

    void OnMouseDown()
    {
        Debug.Log(this.name + " clicked");
    }

    void OnMouseExit()
    {
        m_renderer.material.color = originalColor;
    }
}
