using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class SpawnerResources : MonoBehaviour
{
    [Header("Настройки спавна")]
    public GameObject[] resourcePrefabs;
    public int itemsToSpawn = 10;
    public float spawnRadius = 2f;
    
    private List<Transform> groundTiles = new List<Transform>();
    
    private void Start()
    {
        Hut hut = FindObjectOfType<Hut>();
        if (hut != null)
        {
            hut.onNewDay.AddListener(HandleNewDay);  // Подписываемся
        }
        FindAllGroundTiles();
    }
    private void HandleNewDay()
    {
        Debug.Log("GameManager: Обработка нового дня!");
        DeleteAllResources();
        SpawnResourcesOnIsland();
    }

    private void DeleteAllResources()
    {
        GameObject[] resources = GameObject.FindGameObjectsWithTag("Resource");
        
        foreach (GameObject resource in resources)
        {
            Destroy(resource);
        }
    }

    
    private void SpawnResourcesOnIsland()
    {
        if (groundTiles.Count == 0)
        {
            FindAllGroundTiles();
            if (groundTiles.Count == 0)
            {
                Debug.LogError("GroundTile не найдены!");
                return;
            }
        }
        
        for (int i = 0; i < itemsToSpawn; i++)
        {
            SpawnResource();
        }
    }
    
    // private void SpawnResource()
    // {
    //     Transform randomTile = groundTiles[Random.Range(0, groundTiles.Count)];
        
    //     // Рандомная позиция на тайле
    //     Vector3 spawnPos = randomTile.position + Random.insideUnitSphere * spawnRadius;
    //     spawnPos.y = randomTile.position.y;  // Фиксируем Y
        
    //     // Рандомный префаб
    //     GameObject prefab = resourcePrefabs[Random.Range(0, resourcePrefabs.Length)];
        
    //     Instantiate(prefab, spawnPos, Quaternion.identity);
    // }

    // private void SpawnResource()
    // {
    //     Transform randomTile = groundTiles[Random.Range(0, groundTiles.Count)];
        
    //     // 2D разброс по X и Z, Y от тайла
    //     Vector2 randomOffset = Random.insideUnitCircle * spawnRadius;
    //     Vector3 spawnPos = randomTile.position + new Vector3(randomOffset.x, 0, randomOffset.y);
        
    //     // Рандомный префаб
    //     GameObject prefab = resourcePrefabs[Random.Range(0, resourcePrefabs.Length)];
        
    //     Instantiate(prefab, spawnPos, Quaternion.identity);
    // }

    private void SpawnResource()
    {
        Transform randomTile = groundTiles[Random.Range(0, groundTiles.Count)];
        
        // 2D разброс по X и Z, Y от тайла
        Vector2 randomOffset = Random.insideUnitCircle * spawnRadius;
        Vector3 spawnPos = randomTile.position + new Vector3(randomOffset.x, randomOffset.y, 0);
        
        // Рандомный префаб
        GameObject prefab = resourcePrefabs[Random.Range(0, resourcePrefabs.Length)];
        
        Instantiate(prefab, spawnPos, Quaternion.identity);
    }

//     private void SpawnResource()
// {
//     Transform randomTile = groundTiles[Random.Range(0, groundTiles.Count)];
    
//     // Полный 3D разброс по тайлу
//     Vector3 randomOffset = Random.insideUnitSphere * spawnRadius;
//     randomOffset.y = Mathf.Abs(randomOffset.y);  // Только вверх от тайла
    
//     Vector3 spawnPos = randomTile.position + randomOffset;
    
//     GameObject prefab = resourcePrefabs[Random.Range(0, resourcePrefabs.Length)];
//     Instantiate(prefab, spawnPos, Quaternion.identity);
// }



    
    private void FindAllGroundTiles()
    {
        groundTiles.Clear();
        GameObject[] allGroundTiles = GameObject.FindGameObjectsWithTag("GroundTile");
        groundTiles.AddRange(allGroundTiles.Select(go => go.transform));
        
        Debug.Log($"Найдено GroundTile: {groundTiles.Count}");
    }
}