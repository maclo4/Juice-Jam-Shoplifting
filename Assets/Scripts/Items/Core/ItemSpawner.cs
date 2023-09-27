using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemSpawner : Singleton<ItemSpawner>
{
    private List<Transform> _itemSpawnLocations;

    public GameObject itemBoxPrefab;
    public int maxItems = 15;
    public int initialItemCount = 10;
    private int _spawnListStartSize;

    // Start is called before the first frame update
    private void Start()
    {
        _itemSpawnLocations = gameObject.GetComponentsInChildren<Transform>().ToList();
        _spawnListStartSize = _itemSpawnLocations.Count;
        
        InvokeRepeating(nameof(SpawnItem), 0f, 12f);
        
        for (var i = 0; i < initialItemCount; i++)
        {
            SpawnItem();
        }
    }

    private void SpawnItem()
    {
        if (_itemSpawnLocations.Count <= _spawnListStartSize - maxItems) return;
        
        var randomSpawnLocation = _itemSpawnLocations[Random.Range(0, _itemSpawnLocations.Count)];
        _itemSpawnLocations.Remove(randomSpawnLocation);
        Instantiate(itemBoxPrefab, randomSpawnLocation.position, Quaternion.identity)
            .GetComponent<ItemBox>();
    }

    public void AddSpawnLocation(Transform itemTransform)
    {
        _itemSpawnLocations.Add(itemTransform);
    }
}
