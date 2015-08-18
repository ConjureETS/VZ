using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {

    public enum direction : int { up, down, left, right };
    public float speed = 0.1f;
    public int maxQueueSize = 5;

    private Queue queue;

    public bool overwriteQueue = false;


    

    public MovementManager movementManager;

    Node target;

    // Use this for initialization
    void Start() {
        queue = new Queue(maxQueueSize); // capacity maybe not needed
    }

    // Update is called once per frame
    void Update() {
        if (target != null) {
            transform.LookAt(target.transform);
            transform.position = Vector3.MoveTowards(transform.position, target.pos, speed);
            Debug.Log("I tried to move to :" + target.pos);
        }


    }

    public void changeTarget(Node newTarget) {
        target = newTarget;
    }

    public void enqueueMove(int move) {
        
        //TODO: add logic to handle max size

        if (overwriteQueue) {
            queue.Clear();
        }
        overwriteQueue = false;

        queue.Enqueue(move);
    }

}
