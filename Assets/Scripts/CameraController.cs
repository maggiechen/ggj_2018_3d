using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public float smoothr = 2.0F;
    public float smoothp = 10.0F;
    public float tiltAngle = 5.0F;
    public float moveAmount = 1.0F;

    private Vector3 basePosition;
    private Quaternion baseRotation;


    private void Start() {
        baseRotation = gameObject.transform.rotation;
        basePosition = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update () {
        float ratioX = (Input.mousePosition.x / Screen.width) - 0.5F;
        float ratioY = (Input.mousePosition.y / Screen.height) - 0.5F;

        Quaternion targetr = Quaternion.Euler(-ratioY * tiltAngle, ratioX * tiltAngle, 0);
        transform.rotation = Quaternion.Slerp(transform.rotation, baseRotation * targetr, Time.deltaTime * smoothr);

        Vector3 targetp = new Vector3(ratioX * moveAmount, ratioY * moveAmount, 0);
        transform.position = Vector3.Slerp(transform.position, basePosition + targetp, Time.deltaTime * smoothp);
    }
}
