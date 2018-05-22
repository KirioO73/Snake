using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameControllerSnake : MonoBehaviour {

    public GameObject[] cibles;
    public Vector3 spawnValues;

    

    public Text resultatsTop;
    public Text resultatsBot;

    [Range(0.0f, 200.0f)]
    public float delay;
    [Range(0.0f, 200.0f)]
    public float nouritureDelay;

    private bool PhaseFinal = true;

    private float radius;

    public int[] joueurs;

    List<GameObject> MesSnakes;

    bool spawnEnable;
    // Use this for initialization
    void Start () {
        resultatsTop.enabled = false;
        resultatsBot.enabled = false;
        MesSnakes = new List<GameObject>();
        //Recuperation des joueurs
        for(int i = 0 ; i < joueurs.Length; i++ )
        {
            if (joueurs[i] == 0)
            {
                GameObject.Find("Snake" + (i + 1)).SetActive(false);
            }
            else
            {
                GameObject.Find("Snake" + (i + 1)).SetActive(true);
                MesSnakes.Add(GameObject.Find("Snake" + (i + 1)));
            }
        }
        
        for (int i = 0; i < MesSnakes.Count; i++)
        {
            MesSnakes[i].transform.Find("SnakeHead").GetComponent<SnakeMovements>().enabled = false;
        }
        delay = 0;
        StartCoroutine(AffichageDebut());
        
    }
	
	// Update is called once per frame
	void Update () {

        if (delay > 10)
        {
            delay -= Time.deltaTime;
        }
        else if (delay > 5)
        {
            spawnEnable = false;
            delay -= Time.deltaTime;
        }
        else if (delay > 0)
        {
            delay -= Time.deltaTime;
        }
        else if (delay == 0)
        {

        }
        else
        {
            if (PhaseFinal)
            {
                StartCoroutine(AffichageFin());
                PhaseFinal = false;
            }
            delay = 0;
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

    IEnumerator AffichageDebut()
    {
        resultatsBot.enabled = true;
        resultatsTop.enabled = true;
        resultatsBot.fontSize = 40;
        resultatsTop.fontSize = 40;


        //Décompte de départ
        resultatsBot.text = "3 !";
        resultatsTop.text = "3 !";
        yield return new WaitForSeconds(1);
        resultatsBot.text = "2 !";
        resultatsTop.text = "2 !";
        yield return new WaitForSeconds(1);
        resultatsBot.text = "1 !";
        resultatsTop.text = "1 !";
        yield return new WaitForSeconds(1);
        resultatsBot.text = "Mangez Des Pommes !";
        resultatsTop.text = "Mangez Des Pommes !";
        yield return new WaitForSeconds(1);
        resultatsBot.enabled = false;
        resultatsTop.enabled = false;

        //On active le spawn d'items
        spawnEnable = true;
        radius = cibles[0].GetComponent<Renderer>().bounds.size.x;
        StartCoroutine(SpawnNouriture());


        //on met les tetes actives
        for (int i = 0; i < MesSnakes.Count; i++)
        {
            MesSnakes[i].transform.Find("SnakeHead").GetComponent<SnakeMovements>().enabled = true;
        }

        delay = 120f;
        yield return null;
    }

    IEnumerator AffichageFin()
    {
        //On pause le jeu
        for (int i = 0; i < MesSnakes.Count; i++)
        {
            MesSnakes[i].transform.Find("SnakeHead").GetComponent<SnakeMovements>().enabled = false;
        }        

        //Obtention du meilleur
        GameObject TmpSnake = MesSnakes[0];
        for (int i = 1; i < MesSnakes.Count; i++)
        {
            if (MesSnakes[i].transform.Find("SnakeHead").GetComponent<SnakeMovements>().Score 
                > 
                TmpSnake.transform.Find("SnakeHead").GetComponent<SnakeMovements>().Score)
                TmpSnake = MesSnakes[i];
        }

        //On créer le message de fin, en checkant si le serpent est en coop ou non
        string tmp = "";
        if (TmpSnake.transform.Find("SnakeHead").GetComponent<SnakeMovements>().partnerAr != null)
            tmp = TmpSnake.name  + " et " + TmpSnake.transform.Find("SnakeHead").GetComponent<SnakeMovements>().partnerAr.parent.name + " " + TmpSnake.transform.Find("Score " + TmpSnake.name).GetComponent<Text>().text;
        if (TmpSnake.transform.Find("SnakeHead").GetComponent<SnakeMovements>().partnerAv != null)
            tmp = TmpSnake.transform.Find("SnakeHead").GetComponent<SnakeMovements>().partnerAv.parent.name +" et "+ TmpSnake.name + " " + TmpSnake.transform.Find("Score " + TmpSnake.name).GetComponent<Text>().text;
        else
            tmp = TmpSnake.name + " " + TmpSnake.transform.Find("Score " + TmpSnake.name).GetComponent<Text>().text;

        //Mise a jour des resultats
        resultatsBot.enabled = true;
        resultatsTop.enabled = true;
        resultatsBot.color = TmpSnake.transform.Find("SnakeHead").GetComponent<SpriteRenderer>().color;
        resultatsTop.color = TmpSnake.transform.Find("SnakeHead").GetComponent<SpriteRenderer>().color;
        resultatsBot.text = "Vainqueur : \n " + tmp;
        resultatsTop.text = "Vainqueur : \n " + tmp;
        yield return new WaitForSeconds(3);
        //Sorti Appli to Hub 
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
