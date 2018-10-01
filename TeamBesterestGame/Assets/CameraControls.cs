using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraControls : MonoBehaviour {

    private float dragSpeed = 1.5f;
    private Vector3 dragOrigin;
    public bool scrollLock;

    Camera thisCamera;
    float cameraSize = 19f;
    float cameraSizeMax = 25f;
    float cameraSizeMin = 3f;
    float scrollSpeed = 5f;

    private void Start()
    {
        thisCamera = this.GetComponent<Camera>();
        scrollLock = true;
    }

    void Update()
    {
        if (scrollLock)
        {

            cameraSize += Input.GetAxis("Mouse ScrollWheel") * -scrollSpeed;
            cameraSize = Mathf.Clamp(cameraSize, cameraSizeMin, cameraSizeMax);
            thisCamera.orthographicSize = cameraSize;
        }

        if (Input.GetMouseButtonDown(0))
        {
            dragOrigin = Input.mousePosition;
            return;
        }

        if (!Input.GetMouseButton(0)) return;

        if (EventSystem.current.currentSelectedGameObject == null)
        {
            Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - dragOrigin);
            Vector3 move = new Vector3(pos.x * dragSpeed, pos.y * dragSpeed, 0);

            transform.Translate(move - pos, Space.World);
        }
        
    }
}
