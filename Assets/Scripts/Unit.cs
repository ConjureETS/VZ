using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;

public class Unit : MonoBehaviour 
{
  
    public int defaultHp = 250;
    public int defaultAttack = 100;
    public float LerpSpeed = 1;
    public int destinationGap = 5;
    public float DyingTime = 1.0f;

    private LinkedList<Command> commandList;
    private int _hp; // the unit hp
    private CharacterBehavior _character;


    // movements
    public enum direction : int { up = 1, down = 2, left = 3, right = 4 };
    public float speed = 0.5f;
    public int maxQueueSize = 5;
    public bool overwriteQueue = false; 

    private Queue<int> queue = new Queue<int>();


    // Use this for initialization
    void Start ()
    {
        IsCaptured = false;
        Hp = defaultHp;
        Attack = defaultAttack;
        // initialize default specie
        Tag = TagLayerManager.Human;
        Layer = TagLayerManager.HumanLayerIndex;
        IsDead = false;
        _character = GetComponent<CharacterBehavior>();


        queue.Enqueue(4); //TEST

    }

    void Update()
    {

        if (IsDead)
        {
            DestroyUnit(DyingTime);
        }
        else if (IsCaptured)
        {
           var gapVector = new Vector3(TargetDestination.position.x + destinationGap, TargetDestination.position.y,TargetDestination.position.z + destinationGap);
            // TODO improve the translation position so that every unit captured are around the squad leader and not at only one position.
            transform.position = Vector3.Lerp(transform.position, gapVector, LerpSpeed * 3.0f * Time.deltaTime);
            //See more at: http://unitydojo.blogspot.ca/2014/03/how-to-use-lerp-in-unity-like-boss.html#sthash.ueWlstRk.dpuf*/
            _character.PlayCaptureAnimation(IsCaptured);
        }
        else
        {
            _character.PlayCaptureAnimation(IsCaptured);


            // move towards the target node
            if (target != null) {
                
                _character.PlayRunAnimation(true);
                transform.LookAt(target.transform);
                transform.position = Vector3.MoveTowards(transform.position, target.pos, speed);
            }
            else {
                // if we don't have a target, then we're not moving
                _character.PlayRunAnimation(false);
            }


        }
    }

    /// <summary>
    /// Add a command at the end of the list
    /// </summary>
    /// <param name="c">The command to add</param>
    protected void AddCommand(Command c)
    {
        this.commandList.AddLast(c);
    }

    protected void ExecuteCommand()
    {
        
    }

    /// <summary>
    /// Destroy the current unit
    /// </summary>
    protected IEnumerator DestroyUnit(float dyingTime)
    {
        //TODO First play dead animation
        // retreive the character behavior object, so that we can access it's animation properties...
        var character = GetComponent<CharacterBehavior>();
        // play the death animation 
        character.PlayDeathAnimation();
        yield return new WaitForSeconds(character.CurrrentAnimationLength());
        // then destroy the game object only 3 seconds after the animation
        Destroy(this.transform.gameObject, dyingTime);
    }

    public void EnqueueMove(int move) {

        //TODO: add logic to handle max size

        // overwrite the queue on first new move input if we switched to a new squad
        if (overwriteQueue) {
            queue.Clear();
        }
        overwriteQueue = false;
        printQueue();
        queue.Enqueue(move);
    }

    public int DequeueMove() {
        int move;
        
        // if there is only one move left, we do not remove it
        // so the unit keeps moving in the same direction
        if (queue.Count > 1) {
            move = queue.Dequeue();
        }
        else if( queue.Count == 1){
            move = queue.Peek();
        }
        else {
            move = 0;
        }
        printQueue();
        return move;

    }

    // debug function for queue
    private void printQueue() {
        string queuestr = "queue size: " + queue.Count + " ==> ";
        foreach (int m in queue) {
            queuestr += m + ", ";
        }
        Debug.Log(queuestr);
    }

    #region Unit properties
    public bool IsCaptured { get; set; }
    public int Hp
    {
        get { return _hp; }

        set
        {
            _hp = value > defaultHp ? defaultHp : value;

            if (Hp <= 0)
            {
                //IsDead = true;
                StartCoroutine(DestroyUnit(DyingTime));
            }
        }
    }

    public int Attack { get; set; }
    public bool IsDead { get; set; }

    public Transform TargetDestination { get; set; }

    public string Tag
    {
        get { return this.gameObject.tag; }
        set { gameObject.tag = value; }
    }

    public int Layer
    {
        get { return this.gameObject.layer; }
        set { gameObject.layer = value; }
    }

    public Node target { get; set; }

    #endregion

}
