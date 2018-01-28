using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaTracker : MonoBehaviour {

    public List<AreaController> areas;
    public List<TextMesh> policeCounts;
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
