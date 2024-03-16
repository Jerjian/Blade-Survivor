using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    [SerializeField] private List<GameObject> enemyPrefabs;
    [SerializeField] private GameObject player;

    private Vector3 playerPosition;

    private void Start()
    {
        InvokeRepeating("SpawnEnemy", 0f, 5f);
    }
    private void Update()
    {
        playerPosition = player.transform.position;
    }

    private void SpawnEnemy()
    {
        Instantiate(enemyPrefabs[Random.Range(0, enemyPrefabs.Count)], playerPosition + new Vector3(Random.Range(-10, 10), 0, Random.Range(-10, 10)), Quaternion.identity);
    }
}
