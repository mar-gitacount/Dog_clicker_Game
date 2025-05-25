using System.Collections.Generic;
using UnityEngine;

public class SheepManager : MonoBehaviour
{
    public static SheepManager Instance { get; private set; }
    private List<Sheep> sheepList = new List<Sheep>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void RegisterSheep(Sheep sheep)
    {
        if (!sheepList.Contains(sheep))
        {
            sheepList.Add(sheep);
        }
    }

    public void UnregisterSheep(Sheep sheep)
    {
        if (sheepList.Contains(sheep))
        {
            sheepList.Remove(sheep);
        }
    }
    public int GetSheepListLength()
    {
        Debug.Log($"羊の数: {sheepList.Count}");
        return sheepList.Count;
    }
    public List<Sheep> GetAllSheep()
    {
        return sheepList;
    }
        public int GetSheepCountInView()
    {
        int count = 0;
        foreach (var sheep in sheepList)
        {
            if (IsInView(sheep.transform.position))
            {
                count++;
            }
        }
        return count;
    }
    private bool IsInView(Vector3 position)
    {
        Camera camera = Camera.main;
        Vector3 screenPoint = camera.WorldToViewportPoint(position);
        return screenPoint.x >= 0 && screenPoint.x <= 1 && screenPoint.y >= 0 && screenPoint.y <= 1 && screenPoint.z > 0;
    }
}