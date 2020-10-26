using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerate : MonoBehaviour
{
    /// <summary>
    /// distance along the axis z 1 part
    /// </summary>
    private const int DISTANCE_NEXT_PART = 10;

    [SerializeField] private int spawnPrewarmPartCount;
    [SerializeField] private float distancePlayerSpawnPart;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject partPref;
    [SerializeField] private List<ObstacalSpawnInfo> obstacals;


    private GameObject currentPart;
    private bool isSpawnObst;
    
    private void Awake()
    {
        SpawnNextPart(Vector3.zero);
        PrewarmSpawnPart();
    }

    private void Update()
    {
        SpawnDistancePart();
    }
    private void SpawnObstacals()
    {
        if (!isSpawnObst)
        {
            var randomObst = obstacals[Random.Range(0, obstacals.Count)];
            var chanceToSpawn = Random.Range(0, 1f);
            if (randomObst.chance > chanceToSpawn)
            {
                var spawnPos = new Vector3(Random.Range(randomObst.spawnRandomDistanceX.x, randomObst.spawnRandomDistanceX.y), currentPart.transform.position.y + randomObst.upY,
                    currentPart.transform.position.z);
                var obj = Instantiate(randomObst.prefab, spawnPos, Quaternion.identity);

                // complication of the game, the time decreases, the chance increases
                randomObst.chance += randomObst.frequently;
                randomObst.interval -= randomObst.frequently;
                StartCoroutine(WaitToSpawnObst(randomObst.interval));
            }
        }

    }
    private IEnumerator WaitToSpawnObst(float time)
    {
        isSpawnObst = true;
        yield return new WaitForSeconds(time);
        isSpawnObst = false;
    }
    private void SpawnDistancePart()
    {
        if (GameController.instance.gameState != GameState.End)
        {
            if (Vector3.Distance(player.transform.position, currentPart.transform.position) < distancePlayerSpawnPart)
            {
                SpawnNextPart(currentPart.transform.position);
                SpawnObstacals();
            }
        }
    }
    private void PrewarmSpawnPart()
    {
        for (int i = 0; i <= spawnPrewarmPartCount; i++)
        {
            SpawnNextPart(currentPart.transform.position);
        }
    }
    private void SpawnNextPart(Vector3 spawnPosition)
    {
        currentPart = Instantiate(partPref, new Vector3(spawnPosition.x, spawnPosition.y, 
            spawnPosition.z - DISTANCE_NEXT_PART),Quaternion.identity);
    }
}

