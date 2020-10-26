using System;
using UnityEngine;
using UnityEngine.SocialPlatforms;

[Serializable]
public class ObstacalSpawnInfo 
{
    public string name;
    public GameObject prefab;
    [Range(0, 1f)] public float chance;
    public float interval;
    public float frequently;
    public float upY;
    public Vector2 spawnRandomDistanceX;
}
