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
}
