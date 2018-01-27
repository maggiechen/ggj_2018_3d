using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialRotation : MonoBehaviour {

    public float RotationSpeed = 0.5f;
    public GameObject slider;

    private float WrapAngle(float angle)
    {
        angle %= 360;
        if (angle > 180)
            return angle - 360;

        return angle;
    }

    private void MoveSlider(float angle) {
        if (slider == null) 
        {
            Debug.Log("Slider is unassigned");
        }
        else 
        {
            //slider is clamped to (-0.045 to 0.045)
            slider.transform.localPosition = new Vector3(angle / 2000f, 0f, -0.07f);
        }

    }

	// Update is called once per frame
	void Update () {
        float angle = WrapAngle(gameObject.transform.localRotation.eulerAngles.z);
        MoveSlider(angle);
        if (Input.GetKey(KeyCode.LeftArrow) && angle < 90f) 
        {
            gameObject.transform.Rotate(0f, 0f, RotationSpeed);
        }
        else if (Input.GetKey(KeyCode.RightArrow) && angle > -90f)
        {
            gameObject.transform.Rotate(0f, 0f, -RotationSpeed);
        }

	}
        
}
