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

    public Queue<String> p1MovBuffer = new Queue<String>(4); // Buffer de mouvement pour le player 1
    //public Queue<String> p2MovBuffer = new Queue<String>(4); //Buffer de mouvement pour le player 2

    // Use this for initialization
    void Awake()
    {

        p1MovBuffer.Enqueue("Up");
       // p2MovBuffer.Enqueue("Up");

        p1MovBuffer.Enqueue("Right");
       // p2MovBuffer.Enqueue("Right");

        p1MovBuffer.Enqueue("Down");
       // p2MovBuffer.Enqueue("Down");



    }

    void Update() 
    {
       string[] test = p1MovBuffer.ToArray();


       Debug.Log(test[0] + " " + test[1] + " " + test[2] + " " + test[3] + " ");
    }
    
    void FixedUpdate()
    {
        ReadMovement();

        Debug.Log(p1MovBuffer.Peek());

        Debug.Log(Input.GetAxisRaw("Vertical1"));
    }

    void ReadMovement()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            p1MovBuffer.Enqueue("Up");
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            p1MovBuffer.Enqueue("Left");
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            p1MovBuffer.Enqueue("Down");
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            p1MovBuffer.Enqueue("Right");
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            p1MovBuffer.Enqueue("Up");
        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            p1MovBuffer.Enqueue("Left");
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            p1MovBuffer.Enqueue("Down");
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            p1MovBuffer.Enqueue("Right");
        }
        

      //  Debug.Log(p1MovBuffer.First.ToString() + "  " + p2MovBuffer.First.ToString());
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


        fillBuffWithEmpty(1, 4 - p1MovBuffer.Count);
        return p1MovBuffer.ToArray();
    }

    void fillBuffWithEmpty(int id, int num) 
    {
        for (int u = 0; u < num; u++) 
        {
            if(id == 1){
                p1MovBuffer.Enqueue("Stop");
            }
           /* else if(id ==2){
                p2MovBuffer.Enqueue("Stop");
            }*/
        }
    
    }

    void ResetBuffer() 
    {
        p1MovBuffer = new Queue<string>(4);
    }
}
