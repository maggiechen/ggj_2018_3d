using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialRotation : MonoBehaviour {

    public float RotationSpeed = 0.5f;

    private float WrapAngle(float angle)
    {
        angle %= 360;
        if (angle > 180)
            return angle - 360;

        return angle;
    }

	// Update is called once per frame
	void Update () {
        float angle = WrapAngle(gameObject.transform.localRotation.eulerAngles.z);
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
