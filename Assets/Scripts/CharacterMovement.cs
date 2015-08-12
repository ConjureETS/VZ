using UnityEngine;
using System.Collections;

public class CharacterMovement : MonoBehaviour {

    public int player;

    public MovementManager mm;
    // code mis en commentaire par sam, raison : cannot convert from string to int (on ne pouvait pas compiler les scnènes dans unity sans mettre cette ligne en commentaire.)
    //enum direction { up = "Up",down = "Down", left = "Left", right = "Right", stop = "Stop" };
    enum direction { up, down, left, right, stop};
    

	// Use this for initialization
	void Start () {

        

	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void UpdateDirection() 
    {
       
      

    }
}
