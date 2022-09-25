using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shape : MonoBehaviour
{
    public int corners;

    private void Start()
    {
        Spawn();
    }

    public Vector3 Spawn()
    {
        Vector3 randomRotation = new Vector3(0f, 0f, Random.Range(0f, 360f));
        transform.Rotate(randomRotation);
        Rect rect = Game.instance.playground;
        float x = Random.Range(rect.x, rect.x + rect.width);
        float y = Random.Range(rect.y, rect.y + rect.height);
        Vector3 randomPosition = new Vector3(x, y, 0f);
        transform.position = randomPosition;
        return randomPosition;
    }
}
