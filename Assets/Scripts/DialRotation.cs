using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialRotation : MonoBehaviour {

    public float DeltaPerFrame = 0.5f;

	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.LeftArrow)) {
            gameObject.transform.Rotate(0f, 0f, DeltaPerFrame);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            gameObject.transform.Rotate(0f, 0f, -DeltaPerFrame);
        }

	}
}
