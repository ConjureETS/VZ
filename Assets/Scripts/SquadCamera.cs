using UnityEngine;
using System.Collections;

public class SquadCamera : MonoBehaviour {

    private Transform targetTransform;
    public float y = 25f; // store the height value since it will never change
    public float offsetZ = -5f;
    
    public float transitionDuration = 2.5f;
    private bool isTransitioning;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {        
        
	}

    void LateUpdate() {
        // Early out if we don't have a target
        if (!targetTransform) return;

        if (!isTransitioning) {
            transform.position = new Vector3(targetTransform.position.x, y, targetTransform.position.z + offsetZ);
        }
    }

  
    public void SetTarget(Transform newTarget) {
        Transform previousTarget = targetTransform;

        if (!previousTarget || !previousTarget.Equals(newTarget)) {

            targetTransform = newTarget;
            StopCoroutine("Transition");
            StartCoroutine("Transition");
        }
    }
    
    IEnumerator Transition() {

        float t = 0.0f;         
        Vector3 startingPos = transform.position;
        Vector3 destination = new Vector3(targetTransform.position.x, y, targetTransform.position.z + offsetZ); // add the offset

        isTransitioning = true;
        while (t < 1.0f) {

            t += Time.deltaTime * (Time.timeScale / transitionDuration);
            
            transform.position = Vector3.Lerp(startingPos, destination, t);
            yield return 0;
        }
        isTransitioning = false;
    }
}
