using UnityEngine;
using System.Collections;
using System;
/**
 *  gere les deplacements des squads 
 *  entre les nodes.
 * 
 * a attacher aux squads
 * 
 **/


public class SquadMovement : MonoBehaviour {

    public float speed;

    String[] movementBuffer = new String[5];

    int bufferIndex = 0;

    public enum direction : int { up = 1, down = 2, left = 3, right = 4, stop = 5 };
    public direction dir = direction.stop;

    public MovementManager mm;

    Node target;

	// Use this for initialization
	void Start () {
        movementBuffer[4] = "stop";

        getNewBuffer();
	}
	
	// Update is called once per frame
	void Update () {
        if(target!=null)
        {
           
            transform.position = Vector3.MoveTowards(transform.position, target.pos, speed);
        
        }
       

        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            getNewBuffer();
        }

	}

    public void changeTarget(Node newTarget)
    {
        target = newTarget;
    }

   public int ChangeDirection() 
    {
        string newDir = movementBuffer[bufferIndex];


        if (newDir.ToLower().Equals("up"))
        {
            dir = direction.up;
        }
        else if (newDir.ToLower().Equals("down"))
        {
            dir = direction.down;
        }
        else if (newDir.ToLower().Equals("left"))
        {
            dir = direction.left;
        }
        else if (newDir.ToLower().Equals("right"))
        {
            dir = direction.right;
        }
        else //if (newDir.ToLower().Equals("stop") || newDir == null)
        {
            dir = direction.stop;

        }

        movementBuffer[bufferIndex] = "stop";

        if (bufferIndex < 5) 
        {
            bufferIndex++;
        }

        return (int)dir;
    }

   public void getNewBuffer() 
   {
       try
       {
           var newBuffer = mm.TransferBuffer(0);

           for (int i = 0; i < 4; i++)
           {
               movementBuffer[i] = newBuffer[i];
           }
       }
       catch (NullReferenceException ex)
       {
           Debug.LogError(ex);
       }
   }
}
