using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    private List<Transform> itemSpawnLocations;
    public GameObject itemSpawnLocationsPrefab;

    public GameObject itemBoxPrefab;
    public int maxItems = 15;
    public int initialItemCount = 10;
    private int spawnListStartSize = 0;

    // Start is called before the first frame update
    void Start()
    {
        itemSpawnLocations = itemSpawnLocationsPrefab.GetComponentsInChildren<Transform>().ToList();
        spawnListStartSize = itemSpawnLocations.Count;
        
        InvokeRepeating(nameof(SpawnItem), 0f, 5f);
        
        for (var i = 0; i < initialItemCount; i++)
        {
            SpawnItem();
        }
    }

    private void SpawnItem()
    {
        if (itemSpawnLocations.Count <= spawnListStartSize - maxItems) return;
        
        var randomSpawnLocation = itemSpawnLocations[Random.Range(0, itemSpawnLocations.Count)];
        itemSpawnLocations.Remove(randomSpawnLocation);
        var itemBox = Instantiate(itemBoxPrefab, randomSpawnLocation.position, Quaternion.identity)
            .GetComponent<ItemBox>();

        itemBox.itemSpawner = this;
    }

    public void AddSpawnLocation(Transform itemTransform)
    {
        itemSpawnLocations.Add(itemTransform);
    }
}
