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
    
    // Use this for initialization
    void Start () 
    {
        // initialize default hp
        //Hp = defaultHp;
        // initialize default attack
        Attack = defaultAttack;
        // initialize default team
        // initialize default specie
        isDead = false;
    }

    /// <summary>
    /// Add a command at the end of the list
    /// </summary>
    /// <param name="c">The command to add</param>
    protected void addCommand(Command c)
    {
        this.commandList.AddLast(c);
    }

    protected void executeCommand()
    {
        
    }

    /// <summary>
    /// Destroy the current unit
    /// </summary>
    protected void DestroyUnit()
    {
        Destroy(this.transform.gameObject);
    }

    #region Unit properties

    public int Hp { get; set; }

    public int Attack { get; set; }
    //public bool isAlly { get; set; }
    //public bool isHuman { get; set; }
    public bool isDead { get; set; }

    public String Tag
    {
        get { return this.gameObject.tag; }
        set { gameObject.tag = value; }
    }

    #endregion

}
