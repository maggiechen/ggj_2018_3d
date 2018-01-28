using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public float smoothr = 1.0F;
    public float smoothp = 5.0F;
    public float tiltAngle = 5.0F;
    public float moveAmount = 1.0F;
    public float timeOut = 5.0F;

    private float lastMovementTimestamp;
    private Vector3 basePosition;
    private Quaternion baseRotation;
    public ScreenFaderController screenFaderController;


    private void Start() {
        baseRotation = gameObject.transform.rotation;
        basePosition = gameObject.transform.position;
        lastMovementTimestamp = -timeOut;
        screenFaderController.StartFadingToClear();
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0) {
            // Mouse moved
            lastMovementTimestamp = Time.time;
        }

        float ratioX = Mathf.Clamp((Input.mousePosition.x / Screen.width), 0F, 1F) - 0.5F;
        float ratioY = Mathf.Clamp((Input.mousePosition.y / Screen.height), 0F, 1F) - 0.5F;

        if (Time.time - lastMovementTimestamp > timeOut) {
            transform.rotation = Quaternion.Slerp(transform.rotation, baseRotation, Time.deltaTime * smoothr/4);
            transform.position = Vector3.Slerp(transform.position, basePosition, Time.deltaTime * smoothp/4);
            return;
        }

        Quaternion targetr = Quaternion.Euler(-ratioY * tiltAngle, ratioX * tiltAngle, 0);
        transform.rotation = Quaternion.Slerp(transform.rotation, baseRotation * targetr, Time.deltaTime * smoothr);

        Vector3 targetp = new Vector3(ratioX * moveAmount, ratioY * moveAmount, 0);
        transform.position = Vector3.Slerp(transform.position, basePosition + targetp, Time.deltaTime * smoothp);
    }
}
