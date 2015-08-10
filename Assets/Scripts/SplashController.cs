using UnityEngine;
using System.Collections;

public class SplashController : MonoBehaviour {

	// Use this for initialization
	void Start () {
        StartCoroutine(AutoSkip());
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.anyKeyDown) {
            Application.LoadLevel(1);
        }
	}

    IEnumerator AutoSkip() {
        
        yield return new WaitForSeconds(5);
        Application.LoadLevel(1);
        
    }
}
