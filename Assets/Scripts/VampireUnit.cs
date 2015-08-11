using System;
using UnityEngine;
using System.Collections;
/// <summary>
/// This class contains the information of the Vampire Units.
/// </summary>
public class VampireUnit : Unit
{
	// Use this for initialization
	void Start ()
	{
		InitializeDefaultTag();
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	private void InitializeDefaultTag()
	{
		try
		{
		    this.Tag = TagManager.VampirePlayer; // set the tag to player 1       
		}
		catch (IndexOutOfRangeException ex)
		{
			Debug.LogError( "Please set a vampire Tag, check the Tag & layers in the inspector!\n" + ex );
		}
	 
		// set the tag of the gameObject to vampire
		if (this.gameObject.tag != Tag)
		{
			this.gameObject.tag = Tag;
		}        
	}
}
