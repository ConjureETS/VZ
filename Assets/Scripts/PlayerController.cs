using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public enum Player { Player1 = 1, Player2 = 2 };
    public Player player;

    public SquadCamera squadCamera;
    public Squad[] squads;
    private int currentSquadIndex;

    private float lastHorizontalAxis = 0f;
    private float lastVerticalAxis = 0f;

    MovementManager movementManager;

	// Use this for initialization
	void Start () {
        currentSquadIndex = 0;
        //squadCamera.SetTarget(squads[currentSquadIndex].gameObject.transform.position);
        squadCamera.SetTarget(squads[currentSquadIndex].gameObject.transform);
        movementManager = GetComponent<MovementManager>();

	}
	
	// Update is called once per frame
	void Update () {

        // switching between squads
        if (Input.GetButtonDown("Opt1-" + (int)player)) {
            currentSquadIndex = 0;
            squadCamera.SetTarget(squads[0].transform);            
        }
        if (Input.GetButtonDown("Opt2-" + (int)player)) {
            currentSquadIndex = 1;
            squadCamera.SetTarget(squads[1].transform);                        
        }
        if (Input.GetButtonDown("Opt3-" + (int)player)) {
            currentSquadIndex = 2;
            squadCamera.SetTarget(squads[2].transform);             
        }

        // movements

        float horizontalAxis = Input.GetAxisRaw("Horizontal" + (int)player); 
        if (horizontalAxis != 0 && horizontalAxis != lastHorizontalAxis) { // only enqueue a move when it is a new key press
            
            if (horizontalAxis == 1f) {
                movementManager.EnqueuMove(MovementManager.RIGHT);
            }
            else if (horizontalAxis == -1f) {
                movementManager.EnqueuMove(MovementManager.LEFT);
            }

        }
        lastHorizontalAxis = horizontalAxis;

        float verticalAxis = Input.GetAxisRaw("Vertical" + (int)player);
        if (verticalAxis != 0 && verticalAxis != lastVerticalAxis) { // only enqueue a move when it is a new key press
            
            if (verticalAxis == 1f) {
                movementManager.EnqueuMove(MovementManager.UP);
            }
            else if (verticalAxis == -1f) {
                movementManager.EnqueuMove(MovementManager.DOWN);
            }

        }
        lastVerticalAxis = verticalAxis;


        //TODO add human interaction button (heal/retreat)


        //TODO add squad mode button (transform/capture)


	}
}
