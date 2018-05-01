using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Button : MonoBehaviour {

    public GameObject MySnake;  //Serpent controlé par le boutton
    public string MyRole;       //Role du boutton ( utilisation L pour Rotation Left, R -- Right, U -- Up/Accéléré, D -- Down/Décéléré

    //Reception des messages
    private void Update()
    {
        if(MySnake.GetComponent<SnakeMovements>().partnerAv == null)
        {
            switch (MyRole)
            {
                case "L":
                    gameObject.GetComponent<SpriteRenderer>().enabled = true;
                    gameObject.GetComponent<Collider2D>().enabled = true;
                    break;
                case "R":
                    gameObject.GetComponent<SpriteRenderer>().enabled = true;
                    gameObject.GetComponent<Collider2D>().enabled = true;
                    break;
                case "U":
                    gameObject.GetComponent<SpriteRenderer>().enabled = false;
                    gameObject.GetComponent<Collider2D>().enabled = false;
                    break;
                case "D":
                    gameObject.GetComponent<SpriteRenderer>().enabled = false;
                    gameObject.GetComponent<Collider2D>().enabled = false;
                    break;
                default:
                    break;
            }
        }
        else
        {
            switch (MyRole)
            {
                case "L":
                    gameObject.GetComponent<SpriteRenderer>().enabled = false;
                    gameObject.GetComponent<Collider2D>().enabled = false;
                    break;
                case "R":
                    gameObject.GetComponent<SpriteRenderer>().enabled = false;
                    gameObject.GetComponent<Collider2D>().enabled = false;
                    break;
                case "U":
                    gameObject.GetComponent<SpriteRenderer>().enabled = true;
                    gameObject.GetComponent<Collider2D>().enabled = true;
                    break;
                case "D":
                    gameObject.GetComponent<SpriteRenderer>().enabled = true;
                    gameObject.GetComponent<Collider2D>().enabled = true;
                    break;
                default:
                    break;
            }
        }
    }

    void OnTouchDown()
    {
        //On regarde quel Move doit faire le button ( en fonction de son role)
        switch (MyRole)
        {
            case "L":
                MySnake.GetComponent<SnakeMovements>().rotateLeft();
                break;
            case "R":
                MySnake.GetComponent<SnakeMovements>().rotateRight();
                break;
            case "U":
                MySnake.GetComponent<SnakeMovements>().speedUp();
                break;
            case "D":
                MySnake.GetComponent<SnakeMovements>().slowDown();
                break;
            default:
                break;
        }
    }

    void OnTouchUp()
    {
        switch (MyRole)
        {
            case "L":
                break;
            case "R":
                break;
            case "U":
                MySnake.GetComponent<SnakeMovements>().resetVitesse();
                break;
            case "D":
                MySnake.GetComponent<SnakeMovements>().resetVitesse();
                break;
            default:
                break;
        }
    }

    void OnTouchStay()
    {
        switch (MyRole)
        {
            case "L":
                MySnake.GetComponent<SnakeMovements>().rotateLeft();
                break;
            case "R":
                MySnake.GetComponent<SnakeMovements>().rotateRight();
                break;
            case "U":
                MySnake.GetComponent<SnakeMovements>().speedUp();
                break;
            case "D":
                MySnake.GetComponent<SnakeMovements>().slowDown();
                break;
            default:
                break;
        }
    }

    void OnTouchExit()
    {
        switch (MyRole)
        {
            case "L":
                break;
            case "R":
                break;
            case "U":
                MySnake.GetComponent<SnakeMovements>().resetVitesse();
                break;
            case "D":
                MySnake.GetComponent<SnakeMovements>().resetVitesse();
                break;
            default:
                break;
        }
    }

}
