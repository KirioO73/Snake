using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Button : MonoBehaviour {

    public GameObject MySnake;  //Serpent controlé par le boutton
    public string MyRole;       //Role du boutton ( utilisation L pour Rotation Left, R -- Right, U -- Up/Accéléré, D -- Down/Décéléré

    //Reception des messages

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

    }

}
