using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Circle : MonoBehaviour
{
    public float forceMultiplier;

    private GameObject wonUI, lostUI;
    private Vector2 force;
    private Camera cam;
    private Rigidbody2D rb;
    private Vector3[] linePositions;
    private LineRenderer lineRenderer;
    private bool wasShot = false;

    void Start()
    {
        cam = Camera.main;
        rb = GetComponent<Rigidbody2D>();

        lineRenderer = GetComponent<LineRenderer>();
        linePositions = new Vector3[2];
        linePositions[0] = transform.position;

        wonUI = Game.instance.wonUI;
        lostUI = Game.instance.lostUI;
    }

    void Update()
    {
        // next level / retry
        // restrictions on random generation
        // seed for random gen
        // outer bounds as collision
        // distance between obstacles
        // difficulty scaling
        // first level  (onboarding)
        // block that ends the game
        // if there is end block -> outer bounds collide
        // arrow indicating where to shoot

        var mousePos = Input.mousePosition;
        Vector3 mousePosWorld = cam.ScreenToWorldPoint(mousePos);
        force = transform.position - mousePosWorld;

        linePositions[1] = mousePosWorld;
        lineRenderer.SetPositions(linePositions);

        if (Input.GetMouseButtonUp(0) && !wasShot)
        {
            rb.AddForce(force * forceMultiplier);
            lineRenderer.enabled = false;
            wasShot = true;
        }

        Vector3 viewPos = cam.WorldToViewportPoint(transform.position);
        if (!(viewPos.x >= 0 && viewPos.x <= 1 && viewPos.y >= 0 && viewPos.y <= 1 && viewPos.z > 0))
        {
            GameOver();
        }
    }

    private void GameOver()
    {
        Destroy(gameObject);
        if(Game.instance.CollectedCorners == Game.instance.requiredCorners)
        {
            wonUI.SetActive(true);
            Game.instance.canReset = true;
        }
        else
        {
            lostUI.SetActive(true);
            Game.instance.canReset = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Shape shape = collision.collider.GetComponent<Shape>();
        if(shape != null)
        {
            Game.instance.CollectedCorners += shape.corners;
        }
    }
}
