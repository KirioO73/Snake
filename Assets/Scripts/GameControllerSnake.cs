﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControllerSnake : MonoBehaviour {

    public GameObject[] cibles;
    public Vector3 spawnValues;
    public float nouritureDelay;

    [Range(0.0f, 200.0f)]
    public float delay;

    private float radius;

    bool spawnEnable;
    // Use this for initialization
    void Start () {
        spawnEnable = true;
        radius = cibles[0].GetComponent<Renderer>().bounds.size.x;
        StartCoroutine(SpawnNouriture());
    }
	
	// Update is called once per frame
	void Update () {



		if(delay > 5)
        {
            delay -= Time.deltaTime;
        }
        else if(delay > 0)
        {
            spawnEnable = false;
            delay -= Time.deltaTime;
        }
        else
        {
            //Load scene Hub
        }

        //Presentation, help clavier
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Application.Quit();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            Application.LoadLevel(Application.loadedLevel);
        }
    }



    IEnumerator SpawnNouriture()
    {
        while (true)
        {
            Vector3 spawnPosition;
            do
            {
                spawnPosition = new Vector2(Random.Range(-spawnValues.x, spawnValues.x), Random.Range(-spawnValues.y, spawnValues.y));
            } while (Physics2D.OverlapCircle(spawnPosition, radius) != null);
            if (spawnEnable)
            {
                Instantiate(cibles[Random.Range(0, cibles.Length -1)], spawnPosition, Quaternion.Euler(0, 0, Random.Range(0, 360)));
            }
            else
            {
                StopCoroutine(SpawnNouriture());
            }
            yield return new WaitForSeconds(Random.Range(0, nouritureDelay));
        }        
    }
}
