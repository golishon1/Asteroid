﻿using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    void Update()
    {
        transform.Rotate(Vector3.up * speed * Time.deltaTime);
    }

}
