using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;

public class Unit : MonoBehaviour 
{
    private LinkedList<Command> commandList;
    public int defaultHp = 250;
    public int defaultAttack = 100;
    private int _hp; // the unit hp
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
