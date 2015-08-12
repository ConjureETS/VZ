using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class MovementManager : MonoBehaviour
{

   public Queue<string> p1MovBuffer = new Queue<string>(4); // Buffer de mouvement pour le player 1
   public Queue<string> p2MovBuffer = new Queue<string>(4); //Buffer de mouvement pour le player 2

    // Use this for initialization
    void Awake()
    {
        p1MovBuffer.Enqueue("Stop");
        p2MovBuffer.Enqueue("Stop");
    }

    // Update is called once per frame
    void Update()
    {

        ReadMovement();
        Debug.Log(p1MovBuffer.Peek() + "  " + p2MovBuffer.Peek());

    }

    void ReadMovement()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            p1MovBuffer.Enqueue("Up");
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            p1MovBuffer.Enqueue("Left");
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            p1MovBuffer.Enqueue("Down");
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            p1MovBuffer.Enqueue("Right");
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            p1MovBuffer.Enqueue("Up");
        }
        else if (Input.GetKeyDown(KeyCode.J))
        {
            p1MovBuffer.Enqueue("Left");
        }
        else if (Input.GetKeyDown(KeyCode.K))
        {
            p1MovBuffer.Enqueue("Down");
        }
        else if (Input.GetKeyDown(KeyCode.L))
        {
            p1MovBuffer.Enqueue("Right");
        }
    }

    string getNextDirection(int playerID) 
    {
        try
        {
            if (playerID == 1)
            {
                return p1MovBuffer.Dequeue();
            }
            else if (playerID == 2)
            {
                return p2MovBuffer.Dequeue();
            }
        }catch(InvalidOperationException exception)
        {
            Debug.LogError("Reached the end of the queue we stop by default!: " + exception);

            return "Stop";
        }
        return null;
    }

}
