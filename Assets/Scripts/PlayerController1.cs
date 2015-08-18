using UnityEngine;
using System.Collections;

public class PlayerController1 : MonoBehaviour {

    public enum Player { Player1 = 1, Player2 = 2 };
    public Player player;

    public SquadCamera squadCamera;
    public Squad[] squads;
    private int currentSquadIndex;

    private float lastHorizontalAxis = 0f;
    private float lastVerticalAxis = 0f;
    
	// Use this for initialization
	void Start () {
        currentSquadIndex = 0;
        squadCamera.SetTarget(squads[currentSquadIndex].gameObject.transform);

	}
	
	// Update is called once per frame
	void Update () {

        // switching between squads
        if (Input.GetButtonDown("Opt1-" + (int)player)) {
            if (currentSquadIndex != 0) {
                squads[0].overwriteQueue = true;
            }
            currentSquadIndex = 0;
        }
        if (Input.GetButtonDown("Opt2-" + (int)player)) {
            if (currentSquadIndex != 1) {
                squads[1].overwriteQueue = true;
            }
            currentSquadIndex = 1;
        }
        if (Input.GetButtonDown("Opt3-" + (int)player)) {
            if (currentSquadIndex != 2) {
                squads[2].overwriteQueue = true;
            } 
            currentSquadIndex = 2;
        }

        squadCamera.SetTarget(squads[currentSquadIndex].transform);



        // movements

        float horizontalAxis = Input.GetAxisRaw("Horizontal" + (int)player); 
        if (horizontalAxis != 0 && horizontalAxis != lastHorizontalAxis) { // only enqueue a move when it is a new key press
            
            if (horizontalAxis == 1f) {
                squads[currentSquadIndex].EnqueueMove((int)Unit.direction.right);
            }
            else if (horizontalAxis == -1f) {
                squads[currentSquadIndex].EnqueueMove((int)Unit.direction.left);
            }

        }
        lastHorizontalAxis = horizontalAxis;

        float verticalAxis = Input.GetAxisRaw("Vertical" + (int)player);
        if (verticalAxis != 0 && verticalAxis != lastVerticalAxis) { // only enqueue a move when it is a new key press
            
            if (verticalAxis == 1f) {
                squads[currentSquadIndex].EnqueueMove((int)Unit.direction.up);
            }
            else if (verticalAxis == -1f) {
                squads[currentSquadIndex].EnqueueMove((int)Unit.direction.down);
            }

        }
        lastVerticalAxis = verticalAxis;


        //TODO add human interaction button (heal/retreat)


        //TODO add squad mode button (transform/capture)


	}
}
