using UnityEngine;
using System.Collections;

public class SquadCamera : MonoBehaviour {

    private Vector3 target;
    public float y = 25f; // store the height value since it will never change
    public float offsetZ = -5f;
    
    public float transitionDuration = 2.5f;

	// Use this for initialization
	void Start () {
        target = Vector3.zero;
	}
	
	// Update is called once per frame
	void Update () {        
        
	}

    public void SetTarget(Vector3 newTarget) {
        Vector3 previousTarget = target;
        
        if (!previousTarget.Equals(newTarget)) {
            
            target = newTarget;
            StopCoroutine("Transition");
            StartCoroutine("Transition");
        }
    }



    IEnumerator Transition() {

        float t = 0.0f;         
        Vector3 startingPos = transform.position;        
        Vector3 destination = new Vector3( target.x, y, target.z + offsetZ); // add the offset

        while (t < 1.0f) {

            t += Time.deltaTime * (Time.timeScale / transitionDuration);
            
            transform.position = Vector3.Lerp(startingPos, destination, t);
            yield return 0;
        }
    }
}
