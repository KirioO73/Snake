using UnityEngine;
using System.Collections;

public class SnakeBody : MonoBehaviour {

	private int myOrder;                         //position dans la liste des corps
	public Transform head;                       // tete a la quelle la boule du corps est attachée

	void Start(){
		//head = GameObject.FindGameObjectWithTag("Player").gameObject.transform;                           //recuperation de la tete
		for(int i = 0; i < head.GetComponent<SnakeMovements>().bodyParts.Count; i++){                     //parcour de la liste du corps
			if(gameObject == head.GetComponent<SnakeMovements>().bodyParts[i].gameObject){                //ajoute la boule dans la liste, et met a jour le INT position de la boulle
				myOrder = i;
			}
		}
	}

	private Vector3 movementVelocity;
	[Range(0.0f,1.0f)]
	public float overTime = 0.5f;                //Vitesse a la quelle le corps ratrappe la tete (la distance augmente avec la vitesse de la tete)

	void FixedUpdate(){
		if(myOrder == 0){     //si premier de la liste, je suis la tete
			transform.position = Vector3.SmoothDamp(transform.position, head.position, ref movementVelocity, overTime);
		}
		else{ // sinon je suis la boule devant moi
			transform.position = Vector3.SmoothDamp(transform.position, head.GetComponent<SnakeMovements>().bodyParts[myOrder-1].position, ref movementVelocity, overTime);
		}
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player"                                    //Si je me fait touché par un joueur
            && other.gameObject.transform != head                               //Qui n'est pas ma propre tete,
            && other.GetComponent<SnakeMovements>().partnerAv != head           //Qui n'est pas mon partenaireAv,
            && other.GetComponent<SnakeMovements>().partnerAr != head)          //Ni mon partenaireAr
        {
            decoupeCorps(myOrder, other.gameObject.transform);                  //Je me fait découper
        }
    }

    void decoupeCorps(int order, Transform otherT)
    {   
        //Découper = destruction de mon corps de l'endroit touché à ma queue 
        for (int i = head.GetComponent<SnakeMovements>().bodyParts.Count - 1; i>order ; i--)
        {
            Destroy(head.GetComponent<SnakeMovements>().bodyParts[i].gameObject);
            head.GetComponent<SnakeMovements>().bodyParts.Remove(head.GetComponent<SnakeMovements>().bodyParts[i]);
        }

        if (otherT.GetComponent<SnakeMovements>().partnerAv == null){                                                              //si celui qui me touche n'est pas en coop derriere
            if (head.GetComponent<SnakeMovements>().partnerAv == null && head.GetComponent<SnakeMovements>().partnerAr == null)    //Si je suis solo
            {
                if (otherT.GetComponent<SnakeMovements>().partnerAv == null && otherT.GetComponent<SnakeMovements>().partnerAr == null) //si celui qui me touche est solo
                {
                    head.GetComponent<SnakeMovements>().partnerAr = otherT;                                                 //Celui qui m'a mordu est mon nouveau partnaire arière
                    otherT.GetComponent<SnakeMovements>().partnerAv = head;                                                 //Je suis le nouveau partenaire avant de celui qui m'a mordu
                }
            }
            else if (head.GetComponent<SnakeMovements>().partnerAv == null && otherT.GetComponent<SnakeMovements>().partnerAr != null)       // Si je suis devant 
            {
                    head.GetComponent<SnakeMovements>().partnerAr.GetComponent<SnakeMovements>().partnerAv = null;   //je me fais detacher 
                    head.GetComponent<SnakeMovements>().partnerAr = null;
            }
        }



        /*
        if (head.GetComponent<SnakeMovements>().partnerAv == null 
            && otherT.GetComponent<SnakeMovements>().partnerAv == null             //Si celui qui me découpe a déja un partenaireAv, je me fait juste découper et séparer
            && otherT.GetComponent<SnakeMovements>().partnerAr == null)         //Si celui qui me découpe a déja un partenaireAr, je me fait juste découper et séparé//Si je suis celui de derière je me fait juste découper          
        {
            if (head.GetComponent<SnakeMovements>().partnerAr != null)                                              //Si j'ai un partnaire arrière
            {
                head.GetComponent<SnakeMovements>().partnerAr.GetComponent<SnakeMovements>().partnerAv = null;      //Je me sépare de lui ( il n'a plus de partnaire avant)
            }

            head.GetComponent<SnakeMovements>().partnerAr = otherT;                                                 //Celui qui m'a mordu est mon nouveau partnaire arière
            otherT.GetComponent<SnakeMovements>().partnerAv = head;                                                 //Je suis le nouveau partenaire avant de celui qui m'a mordu
        }*/
    }

    }
