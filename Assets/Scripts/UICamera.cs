using UnityEngine;
using System.Collections;

public class UICamera : MonoBehaviour
{
    public Camera TargetCamera;

    void Awake()
    {
        //TargetCamera = GetComponent<>()
        //TODO find a way to target the player 1 camera and player 2 camera
        // example if my parent is player 1 then I target at camera1 else, target at camera2
    }
	// Update is called once per frame
	void Update ()
    {
	    transform.LookAt(
            transform.position + TargetCamera.transform.rotation * Vector3.back, 
            TargetCamera.transform.rotation * Vector3.up
        );
	}
}
