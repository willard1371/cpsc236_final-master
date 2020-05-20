﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_manager : MonoBehaviour
{
    private float timer;
    private float maxTimer;
    public GameObject enemy;

    public float timerMin = 5f;
    public float timerMax = 25f;
    
    void Start()
    {
        timer = 0;
        maxTimer = Random.Range(timerMin, timerMax);
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine("SpawnEnemyTimer");
    }

    void SpawnEnemy()
    {
        float y = 1.25f;
        Vector3 spawnPoint = Camera.main.ViewportToWorldPoint(new Vector3(Random.Range(0, 1f), y, 0));
        spawnPoint.z = 0;

        //Adjust x-axis position
        float dist = (this.transform.position - Camera.main.transform.position).z;
        float leftBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, dist)).y;
        float rightBorder = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, dist)).y;
        Vector3 enemySize = enemy.GetComponent<Renderer>().bounds.size;
        spawnPoint.x = Mathf.Clamp(spawnPoint.x, leftBorder + enemySize.x / 2, rightBorder - enemySize.x / 2);

        GameObject.Instantiate(enemy, spawnPoint, new Quaternion(0, 0, 0, 0));
    }

    IEnumerator SpawnEnemyTimer()
    {
        if (timer >= maxTimer)
        {
            SpawnEnemy();
            timer = 0;
            maxTimer = Random.Range(timerMin, timerMax);
        }

        timer += 0.1f;
        yield return new WaitForSeconds(0.1f);
    }
}
