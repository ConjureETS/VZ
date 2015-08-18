using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

/**
 * gere les inputs
 * 
 * a placer sur les players
*/

public class MovementManager : MonoBehaviour
{

    public Queue<String> moveBuffer = new Queue<String>(4); // Buffer de mouvement


    public const string UP = "Up";
    public const string DOWN = "Down";
    public const string LEFT = "Left";
    public const string RIGHT = "Right";  



    // Use this for initialization
    void Awake()
    {
        //test
        moveBuffer.Enqueue("Up");

        moveBuffer.Enqueue("Right");
        moveBuffer.Enqueue("Up");

        moveBuffer.Enqueue("Down");
        moveBuffer.Enqueue("Down");


    }

    void Update() 
    {
       string[] test = moveBuffer.ToArray();


       //Debug.Log(test[0] + " " + test[1] + " " + test[2]  + " ");
    }
    
    void FixedUpdate()
    {
        //ReadMovement();

        //Debug.Log(moveBuffer.Peek());

        //Debug.Log(Input.GetAxisRaw("Vertical1"));
    }

    

    public void EnqueuMove(string move){

        moveBuffer.Enqueue(move);
    }


    public string[] TransferBuffer(int playerId) 
    {
       /* switch (playerId) 
        {
            case 1:

                fillBuffWithEmpty(1, 4 - p1MovBuffer.Count);
                return p1MovBuffer.ToArray();
                
            case 2:
                fillBuffWithEmpty(2, 4 - p2MovBuffer.Count);
                return p2MovBuffer.ToArray();     
        }*/


        fillBuffWithEmpty(1, 4 - moveBuffer.Count);
        return moveBuffer.ToArray();
    }

    void fillBuffWithEmpty(int id, int num) 
    {
        for (int u = 0; u < num; u++) 
        {
            if(id == 1){
                moveBuffer.Enqueue("Stop");
            }
           /* else if(id ==2){
                p2MovBuffer.Enqueue("Stop");
            }*/
        }
    
    }

    public void ResetBuffer() 
    {
        moveBuffer = new Queue<string>(4);
    }
}
