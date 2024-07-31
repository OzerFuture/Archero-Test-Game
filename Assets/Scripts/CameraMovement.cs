using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private Vector3 _mainCameraPos;
    private Camera _mainCamera;
    void Start()
    {
        _mainCamera = Camera.main;
        _mainCameraPos = _mainCamera.transform.position;
    }

    void LateUpdate()
    {
        _mainCamera.transform.position = new Vector3(_mainCameraPos.x, 23, transform.position.z);
    }
}
