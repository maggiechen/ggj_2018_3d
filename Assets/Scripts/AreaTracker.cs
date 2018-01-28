using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaTracker : MonoBehaviour {

    public List<AreaController> areas;
    public List<TextMesh> policeCounts;
    public GameObject leaf;
    bool leafHidden = true;

    public void SetLeafPosition(float x, float y)
    {
        if (leafHidden)
        {
            leafHidden = false;
            ShowLeaf();
        }
        Vector3 pos = leaf.transform.position;
        pos.x = x;
        pos.y = y;
        leaf.transform.position = pos;
    }

    public void ShowLeaf()
    {
        Vector3 pos = leaf.transform.localPosition;
        pos.z = -0.5f;
        leaf.transform.localPosition = pos;
    }

    private void Update()
    {
        for (int i = 0; i < policeCounts.Count; i++)
        {
            int policeCount = GameManager.Instance.gameStateMachine.locations[i];
            policeCounts[i].text = policeCount.ToString();
        }
    }

    internal void DisableAllAreas()
    {
        for (int i = 0; i < areas.Count; i++)
        {
            areas[i].SetDefaultMaterial();
            areas[i].selected = false;
        }
    }
}
