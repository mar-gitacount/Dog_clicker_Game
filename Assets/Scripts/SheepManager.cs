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

    public List<Sheep> GetAllSheep()
    {
        return sheepList;
    }
}