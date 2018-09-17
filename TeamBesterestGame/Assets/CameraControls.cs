using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControls : MonoBehaviour {

    private float dragSpeed = 1.5f;
    private Vector3 dragOrigin;

    Camera thisCamera;
    float cameraSize = 19f;
    float cameraSizeMax = 25f;
    float cameraSizeMin = 10f;
    float scrollSpeed = 3f;

    private void Start()
    {
        thisCamera = this.GetComponent<Camera>();
    }

    void Update()
    {
        cameraSize += Input.GetAxis("Mouse ScrollWheel") * -scrollSpeed;
        cameraSize = Mathf.Clamp(cameraSize, cameraSizeMin, cameraSizeMax);
        thisCamera.orthographicSize = cameraSize;

        if (Input.GetMouseButtonDown(0))
        {
            dragOrigin = Input.mousePosition;
            return;
        }

        if (!Input.GetMouseButton(0)) return;

        Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - dragOrigin);
        Vector3 move = new Vector3(pos.x * dragSpeed, pos.y * dragSpeed, 0);

        transform.Translate(move - pos, Space.World);
    }
}
