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

    private void MoveSlider(int dir) {
        if (slider == null) 
        {
            Debug.Log("Slider is unassigned");
        }
        else 
        {
            slider.transform.Translate(Vector3.right * dir * RotationSpeed / 25);
        }

    }

	// Update is called once per frame
	void Update () {
        float angle = WrapAngle(gameObject.transform.localRotation.eulerAngles.z);
        if (Input.GetKey(KeyCode.LeftArrow) && angle < 90f) 
        {
            MoveSlider (-1);
            gameObject.transform.Rotate(0f, 0f, RotationSpeed);
        }
        else if (Input.GetKey(KeyCode.RightArrow) && angle > -90f)
        {
            MoveSlider (1);
            gameObject.transform.Rotate(0f, 0f, -RotationSpeed);
        }

	}
        
}
