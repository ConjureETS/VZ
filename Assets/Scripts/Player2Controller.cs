using UnityEngine;
using System.Collections;

public class Player2Controller : MonoBehaviour {

    public SquadCamera squadCamera;
    public Squad[] squads;

	// Use this for initialization
	void Start () {
        squadCamera.SetTarget(squads[0].gameObject.transform.position);   
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetAxisRaw("Opt1-2") == 1f) {            
            squadCamera.SetTarget(squads[0].gameObject.transform.position);            
        }
        else if (Input.GetAxisRaw("Opt2-2") == 1f) {
            squadCamera.SetTarget(squads[1].gameObject.transform.position);            
        }
        else if (Input.GetAxisRaw("Opt3-2") == 1f) {
            squadCamera.SetTarget(squads[2].gameObject.transform.position); 
        }
	}
}
