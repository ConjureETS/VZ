﻿using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;

public class Unit : MonoBehaviour 
{
    private LinkedList<Command> commandList;
    public int defaultHp = 250;
    public int defaultAttack = 100;
    public float LerpSpeed = 1;
    public int destinationGap = 5;
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
        if (IsCaptured)
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
    protected void DestroyUnit()
    {
        //TODO First play dead animation
        // then destroy the game object
        Destroy(this.transform.gameObject);
    }

    #region Unit properties
    public bool IsCaptured { get; set; }
    public int Hp
    {
        get { return _hp; }

        set
        {
            if (Hp < 0)
            {
                _hp = 0;
                IsDead = true;
            }
            else if (value > defaultHp)
            {
                _hp = defaultHp;
            }
            else
            {
                _hp = value;
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
