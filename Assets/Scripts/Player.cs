using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject preview;
    public GameObject circle;

    private GameObject previewInstance;
    private GameObject circleInstance;
    private Camera cam;

    private bool canShoot;

    void Start()
    {
        previewInstance = Instantiate(preview);
        cam = Camera.main;
        canShoot = true;
    }

    void Update()
    {
        if ((Input.GetMouseButtonUp(0) || Input.GetMouseButtonUp(1)) && Game.instance.canReset)
        {
            previewInstance.SetActive(true);
            canShoot = true;
        }

        if (Input.GetMouseButtonDown(0) && canShoot)
        {
            previewInstance.SetActive(false);
            var mousePos = Input.mousePosition;
            Vector3 circlePos = cam.ScreenToWorldPoint(mousePos);
            circlePos.z = 0f;
            circleInstance = Instantiate(circle, circlePos, Quaternion.identity);
            canShoot = false;
        }
    }
}
