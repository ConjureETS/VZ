﻿using UnityEngine;
using System.Collections;

public class Node : MonoBehaviour {

	public Node n_up;
	public Node n_left;
	public Node n_down;
	public Node n_right;

    SquadMovement validSquad;

    public Vector3 pos;

    void Start()
    {
        pos = gameObject.transform.position;
    }

     
	void OnDrawGizmos() {
        if (n_up != null) {
			Gizmos.color = Color.red;
			Gizmos.DrawLine(pos, n_up.pos);
		}else if (n_left != null) {
			Gizmos.color = Color.red;
            Gizmos.DrawLine(pos, n_left.pos);
		}else if (n_down != null) {
			Gizmos.color = Color.red;
			Gizmos.DrawLine(pos, n_down.pos);
		}else if (n_right != null) {
			Gizmos.color = Color.red;
			Gizmos.DrawLine(pos, n_right.pos);
		}   
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "VampirePlayer" || other.tag == "ZombiePlayer") 
        { 
           validSquad = other.GetComponent<SquadMovement>();
            Node newTarget = sendNextNode(validSquad.ChangeDirection());
           validSquad.changeTarget(newTarget);
           Debug.Log((int)validSquad.dir);
        }
    }

    void OnTriggerExit(Collider other) 
    {
        validSquad = null;
    }

    public Node sendNextNode(int direction) 
    {
        switch (direction)
        {
            case 1: //up

                if (n_up != null)
                {
                    return n_up;
                }

                break;

            case 2: //down

                if (n_down != null)
                {
                    return n_down;
                }
                break;

            case 3: //left

                if (n_left != null)
                {
                    return n_left;
                }
                break;

            case 4: //right

                if (n_right != null)
                {
                    return n_right;
                }
                break;

            case 5: //stop 

                //validSquad.UpdateDirection();
                return null;
        }

        return null;
    }

}
