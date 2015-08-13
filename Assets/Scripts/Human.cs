using UnityEngine;
using System.Collections;

public class Human : MonoBehaviour {

	//vitesse de deplacement
	public float humanSpeed;
	//Le temps avant de faire un changement de direction
	public int duree;
	private int timer = 0;
	private Rigidbody rb;
	private float horizontal=10.0f;
	private float vertical=0.0f;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
	}
	
	float changementSens()
	{
		//Faire un changement de sens
		if (Random.value > 0.5) {
			return -1.0f;
		} 
		else {
			return 1.0f;
		}
	}

	void FixedUpdate()
	{

		if (timer == duree) {
			if (Random.value > 0.5) {
				horizontal = 10.0f*changementSens();
				vertical=0.0f;
				print (horizontal);
			} else {
				vertical = 10.0f*changementSens();
				horizontal=0.0f;
				print (vertical);
			}
			timer=0;
		}
		timer += 1;


		Vector3 mouvement = new Vector3 (horizontal,0.0f,vertical);
		rb.AddForce (mouvement);
	}


}
