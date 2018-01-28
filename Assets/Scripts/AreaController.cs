using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaController : MonoBehaviour {

    Color originalColor;
    MeshRenderer m_renderer;
    public Material selectedMaterial;
    public Material hoverMaterial;
    public Material defaultMaterial;
    AreaTracker areaTracker;
    ChannelController channels;
    public bool selected;

    // Use this for initialization
    void Start () {
        selected = false;
        m_renderer = GetComponent<MeshRenderer>();
        originalColor = m_renderer.material.color;
        channels = FindObjectOfType<ChannelController>();
        areaTracker = this.transform.parent.GetComponent<AreaTracker>();
    }

    void OnMouseOver()
    {
        if (!DialRotation.DialLocked() && channels.TalkingToWeedman)
        {
            SetMaterial(hoverMaterial);
        }
    }
    
    public void SetDefaultMaterial()
    {
        SetMaterial(defaultMaterial);
    }

    void SetMaterial(Material mat)
    {
        Material[] mats = m_renderer.materials;
        mats[0] = mat;
        m_renderer.materials = mats;
    }

    void OnMouseDown()
    {
        Debug.Log(this.name + " clicked");
        if (!DialRotation.DialLocked() && channels.TalkingToWeedman)
        {
            areaTracker.DisableAllAreas();
            selected = true;
            SetMaterial(selectedMaterial);
            areaTracker.SetLeafPosition(this.gameObject.transform.position.x,
                                        this.gameObject.transform.position.y);
            channels.InterruptWeedman();
            //TODO move Van here, if it's not here already
            List <AreaController> areas = areaTracker.areas;
            GameManager.Instance.gameStateMachine.MoveWeedVan(areas.IndexOf(this));
        }
    }

    void OnMouseExit()
    {
        if (selected)
        {
            SetMaterial(selectedMaterial);
        } else
        {
            SetMaterial(defaultMaterial);
        }
    }
}
