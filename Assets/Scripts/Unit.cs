using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Unit : MonoBehaviour 
{
	public int HP { get; set; }
	public int Attack { get; set; }
	public bool isAlly { get; set; }
	public bool isHuman { get; set; }
    public bool isDead { get; set; }

	private LinkedList<Command> commandList;
	// Use this for initialization
	void Start () 
	{
	    // initialize default hp
        // initialize default attack
        // initialize default team
        // initialize default specie
	}
	
	// Update is called once per frame
	void Update () 
	{
	    //

	    if (isDead)
	    {
	        
	    }
	}

    /// <summary>
    /// Add a command at the end of the list
    /// </summary>
    /// <param name="c">The command to add</param>
	public void addCommand(Command c)
	{
		this.commandList.AddLast(c);
	}

    public void executeCommand()
    {
        
    }
    

}
