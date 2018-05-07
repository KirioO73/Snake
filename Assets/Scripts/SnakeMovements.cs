using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeMovements : MonoBehaviour {

	public List<Transform> bodyParts = new List<Transform>(); //Liste des boules du corps du snake
    public Transform partnerAv;                               //Partner se trouvant devant moi (que je suis)
    public Transform partnerAr;                               //Partner se travant derière moi (qui me suis)

	void Start () {
        partnerAv = null;
        partnerAr = null;
    }
	
	void Update () {

        //Commandes du controle clavier pour simplifications des tests sur machine
        if (gameObject.transform.parent.name == "Snake1")
        {
            if (Input.GetKey(KeyCode.Q))
            {
                currentRotation += RotationSensitivity * Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.D))
            {
                currentRotation -= RotationSensitivity * Time.deltaTime;
            }
        }else if (gameObject.transform.parent.name == "Snake2")
        {
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                currentRotation += RotationSensitivity * Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                currentRotation -= RotationSensitivity * Time.deltaTime;
            }
        }
        else
        {
            if (Input.GetKey(KeyCode.Keypad4))
            {
                currentRotation += RotationSensitivity * Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.Keypad6))
            {
                currentRotation -= RotationSensitivity * Time.deltaTime;
            }
        }
	}


	public float speed = 3.5f; //Vitesse de la tete du snake

	public float currentRotation;                  // valeur de la rotation actuelle 
	public float RotationSensitivity = 50.0f;	   //Sensibilité de la rotation

    private Vector3 movementVelocity;
    [Range(0.0f, 1.0f)]
    public float overTime = 0.5f;                //Vitesse a la quelle le corps ratrappe ce qu'elle suit (la distance augmente avec la vitesse de la tete)

    void FixedUpdate(){                            //check 3 fois par frame
        if (partnerAv == null)
        {
            GetComponent<Rigidbody2D>().isKinematic = false;    // Activer le rigidbody uniquement sur la tete qui dirige (avant)
            MoveForward();                                      // Fait avancer le snake
            Rotation();                                         // gere la rotation du snake
        }
        else
        {
            //Une tete qui suit, suit la queue (dernière partie de son corps) de son partenaire avant
            transform.position = Vector3.SmoothDamp(transform.position,
                partnerAv.GetComponent<SnakeMovements>().bodyParts[partnerAv.GetComponent<SnakeMovements>().bodyParts.Count-1].position,
                ref movementVelocity, 
                overTime);
            GetComponent<Rigidbody2D>().isKinematic = true;     //Une tete qui suit n'a plus de rigidbody
        }
	}


	void MoveForward (){                          // Mouvement vers l'avant
		transform.position += transform.up * speed * Time.deltaTime;
	}

	void Rotation(){                                          //applique une rotation autour de l'axe Z 
		transform.rotation = Quaternion.Euler (new Vector3 (transform.rotation.x, transform.rotation.y, currentRotation));
	}


	public Transform bodyObject;                     // attachement du prefab "body" pour gerer la generation apres chaque consomation de pomme

	void OnCollisionEnter2D(Collision2D other) {                        //2D TRES IMPORTANT 
		if (other.transform.tag == "Apple") {                             // check le tag de l'objet en collision
			Destroy(other.gameObject);                                    // detruit la pomme touchée
            addBody();
		}
	}

    void addBody()
    {
        if (bodyParts.Count == 0)
        {                                   //si pas de corps
            Vector2 currentPos = transform.position;                  // position de la premiere boule du coups sur la tete
            Transform newBodyPart = Instantiate(bodyObject, currentPos, Quaternion.identity) as Transform;        //On instantie
            bodyParts.Add(newBodyPart);                                                                           // ajout dans la liste des bouts de corps
            newBodyPart.GetComponent<SpriteRenderer>().sortingOrder = 0;                                           //On met a la position 0 dans le layer 
            newBodyPart.transform.parent = gameObject.transform.parent;                                            //Rangement --> limite la polution visuel dans l'éditeur + simplifie certaine gestions
            newBodyPart.GetComponent<SnakeBody>().head = this.transform;                                           //On donne sa tete Au corps (au lieu de passer apr tag)
        }
        else
        {                                                              // si on a deja des boules du corps
            Vector3 currentPos = bodyParts[bodyParts.Count - 1].position;     // on fait apparaitre la prochaine boule aux coordonnées de la dernierre boule du corps
            Transform newBodyPart = Instantiate(bodyObject, currentPos, Quaternion.identity) as Transform; //On instantie
            bodyParts.Add(newBodyPart);                                                                      // ajout dans la liste des bouts de corps
            majLayers();                                                    //Decalage de toutes les autres boules du couprs dans le layer
            newBodyPart.GetComponent<SpriteRenderer>().sortingOrder = 0;   //On place la dernierre boule en dessoud de toutes les autres
            newBodyPart.transform.parent = gameObject.transform.parent;     //Rangement --> limite la polution visuel dans l'éditeur + simplifie certaine gestions
            newBodyPart.GetComponent<SnakeBody>().head = this.transform;    //On donne sa tete Au corps (au lieu de passer apr tag)
        }
        if(partnerAr != null)
        {
            partnerAr.GetComponent<SnakeMovements>().addBody();
        }
    }

	void majLayers(){                    // decalage des boules du corps pour faire apparaitre une nouvelle en dessous (couche 0 du layer)
		
		for (int i = 0; i < bodyParts.Count; i++) {
			bodyParts[i].GetComponent<SpriteRenderer> ().sortingOrder = bodyParts[i].GetComponent<SpriteRenderer> ().sortingOrder+1;
		}
	}
		
    //Mouvement gauche
    public void rotateLeft()
    {
        currentRotation += RotationSensitivity * Time.deltaTime;
    }

    //Mouvement droit
    public void rotateRight()
    {
        currentRotation -= RotationSensitivity * Time.deltaTime;
    }

    //Ralentissement
    public void slowDown()
    {
        partnerAv.GetComponent<SnakeMovements>().speed = 1.5f;
    }

    //Accélération
    public void speedUp()
    {   
        if(partnerAv != null) partnerAv.GetComponent<SnakeMovements>().speed = 3.5f;
    }

    //Remet la vitesse
    public void resetVitesse()
    {
        if (partnerAv != null) partnerAv.GetComponent<SnakeMovements>().speed = 2.5f;
    }
}