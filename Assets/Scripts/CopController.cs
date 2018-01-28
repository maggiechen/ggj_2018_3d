using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopController : MonoBehaviour {

    private string stanielPosition;
    private string wkPosition;
    private string emergencyPosition;

    // Use this for initialization
    void Start () {
        stanielPosition = "None";
        wkPosition = "None";
        emergencyPosition = "None";
    }
	
	// Update is called once per frame
	void Update () {
        if(RadioTimer.instance.GetTime() == 1)
        {
            EnableStaniel("Area2");
        } else if(RadioTimer.instance.GetTime() == 120)
        {
            DisableStaniel();
        }
    }

    public bool checkForCop(string area)
    {
        if (stanielPosition != area
            && wkPosition != area
            && emergencyPosition != area)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    void EnableStaniel(string area)
    {
        stanielPosition = area;
    }

    void DisableStaniel()
    {
        stanielPosition = "None";
    }

    void EnableWK(string area)
    {
        wkPosition = area;
    }

    void DisableWK()
    {
        wkPosition = "None";
    }

    void EnableEmergency(string area)
    {
        emergencyPosition = area;
    }

    void DisableEmergency()
    {
        emergencyPosition = "None";
    }
}
