using UnityEngine;
using System.Collections;

public class SquadCamera : MonoBehaviour {

    public GameObject[] squads;
    private GameObject target;
    public enum Player { Player1, Player2 };
    public Player player;

    private float y; // store the height value since it will never change


    public float transitionDuration = 2.5f;


	// Use this for initialization
	void Start () {
        this.target = squads[0];

        this.transform.position= new Vector3( 
            target.gameObject.transform.position.x,
            this.transform.position.y,
            target.gameObject.transform.position.z);

        this.y = this.transform.position.y;

	}
	
	// Update is called once per frame
	void Update () {
        
        Vector3 previousTarget = target.transform.position;


        if(Input.GetAxisRaw("Opt1-1") == 1f){
            target = squads[0];
        }
        else if (Input.GetAxisRaw("Opt2-1") == 1f) {
            
            target = squads[1];
        }
        else if (Input.GetAxisRaw("Opt3-1") == 1f) {
            target = squads[2];
        }


        if (!previousTarget.Equals(target.transform.position)) {
            

            StartCoroutine(Transition());

        }

        

	
	}



    IEnumerator Transition() {
        float t = 0.0f; 
        
        Vector3 startingPos = transform.position;
        
        Vector3 destination = new Vector3(
                target.transform.position.x,
                y,
                target.transform.position.z
               );

        while (t < 1.0f) {

            t += Time.deltaTime * (Time.timeScale / transitionDuration);
            
            transform.position = Vector3.Lerp(startingPos, destination, t);
            yield return 0;
        }
    }
}
