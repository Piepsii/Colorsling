using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Game : MonoBehaviour
{
    public static Game instance;

    public int shapeAmount;
    public List<GameObject> shapeInstances = new List<GameObject>();
    public List<GameObject> shapes = new List<GameObject>();
    public float minDistance;

    public int requiredCorners;
    private int collectedCorners;

    public int minCorners;
    public int maxCorners;

    public Rect playground;

    public TextMeshProUGUI requiredCornersUI;
    public TextMeshProUGUI collectedCornersUI;
    public GameObject wonUI, lostUI;

    public bool canReset;

    public int CollectedCorners { 
        get => collectedCorners; 
        set
        {
            collectedCorners = value;
            UpdateUI();
        }
    }

    private void Awake()
    {
        instance = this;
        canReset = false;
    }

    void Start()
    {
        NewLevel();
    }

    void Update()
    {
        if (Input.GetMouseButtonUp(0) && canReset)
        {
            RestartLevel();
        }
        else if(Input.GetMouseButtonUp(1) && canReset)
        {
            NewLevel();
        }
    }

    private void RestartLevel()
    {
        collectedCorners = 0;
        UpdateUI();
        wonUI.SetActive(false);
        lostUI.SetActive(false);
        canReset = false;
    }

    private void NewLevel()
    {
        SpawnShapes();
        requiredCorners = Random.Range(minCorners, maxCorners);
        collectedCorners = 0;
        UpdateUI();
        wonUI.SetActive(false);
        lostUI.SetActive(false);
        canReset = false;
    }

    private void SpawnShapes()
    {
        if (shapeInstances.Count > 0)
        {
            for (int i = 0; i < shapeAmount; i++)
                Destroy(shapeInstances[i].gameObject);
        }
        shapeInstances.Clear();
        for (int i = 0; i < shapeAmount; i++)
        {
            var instance = Instantiate(shapes[Random.Range(0, shapes.Count)]);
            Shape shape = instance.GetComponent<Shape>();
            Vector3 spawnPos = shape.Spawn();
            for (int j = 0; j < i; j++)
            {
                float dist = Vector3.Distance(spawnPos, shapeInstances[j].transform.position);
                int k = 0;
                while (dist < minDistance && k < 10)
                {
                    shape.Spawn();
                    k++;
                }
            }
            shapeInstances.Add(instance);
        }
    }

    public void UpdateUI()
    {
        requiredCornersUI.text = requiredCorners.ToString();
        collectedCornersUI.text = collectedCorners.ToString();
    }
}
