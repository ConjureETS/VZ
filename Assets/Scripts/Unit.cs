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

    #endregion

}
