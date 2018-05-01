using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControllerSnake : MonoBehaviour {

    public GameObject cible;
    public Vector3 spawnValues;
    public float nouritureDelay;

    private float radius;

    bool spawnEnable;
    // Use this for initialization
    void Start () {
        spawnEnable = true;
        radius = cible.GetComponent<Renderer>().bounds.size.x;
        StartCoroutine(SpawnNouriture());
    }
	
	// Update is called once per frame
	void Update () {
		
	}



    IEnumerator SpawnNouriture()
    {
        while (spawnEnable)
        {
            Vector3 spawnPosition;
            do
            {
                spawnPosition = new Vector2(Random.Range(-spawnValues.x, spawnValues.x), Random.Range(-spawnValues.y, spawnValues.y));
            } while (Physics2D.OverlapCircle(spawnPosition, radius) != null);
            if (spawnEnable)
            {
                Instantiate(cible, spawnPosition, Quaternion.Euler(0, 0, Random.Range(0, 360)));
            }
            else
            {
                yield return new WaitForSeconds(5 * nouritureDelay);
            }
            yield return new WaitForSeconds(Random.Range(0, nouritureDelay));
        }        
    }
}
