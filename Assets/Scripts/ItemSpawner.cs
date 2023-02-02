using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    private List<Transform> itemSpawnLocations;
    public GameObject itemSpawnLocationsPrefab;

    public GameObject itemBoxPrefab;

    // Start is called before the first frame update
    void Start()
    {
        itemSpawnLocations = itemSpawnLocationsPrefab.GetComponentsInChildren<Transform>().ToList();
        InvokeRepeating(nameof(SpawnItem), 0f, 5f);
    }

    private void SpawnItem()
    {
        var randomSpawnLocation = itemSpawnLocations[Random.Range(0, itemSpawnLocations.Count)];
        Instantiate(itemBoxPrefab, randomSpawnLocation.position, Quaternion.identity);
    }
}
