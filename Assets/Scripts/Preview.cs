using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Preview : MonoBehaviour
{
    private Camera cam;

    void Start()
    {
        cam = Camera.main;

    }

    void Update()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 circlePos = cam.ScreenToWorldPoint(mousePos);
        circlePos.z = 0f;
        transform.position = circlePos;
    }
}
