using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Squad : Unit 
{
    /// <summary>
    /// List of non attackable units.
    /// </summary>
    private List<Unit> humans;
    /// <summary>
    /// List of attackable units.
    /// </summary>
    private List<Unit> soldiers;

	// Use this for initialization
	void Start () 
    {
        humans = new List<Unit>();
        soldiers = new List<Unit>();
	}
	
	// Update is called once per frame
	void Update () 
    {
	    // TODO execute movement command
        // TODO Check if all the units are dead
            // if yes destroy 
	    if (soldiers.Count == 0)
	    {
	        // then we destroy the squad
            DestroyUnit();
	    }
	}

    void addHuman(Unit humanUnit) 
    {
        humans.Add(humanUnit);
    }

    void addSoldier(Unit soldierUnit)
    {
        soldiers.Add(soldierUnit);
    }

    /// <summary>
    ///  Dispose of a human unit and heal all the soldiers with a percentage depending 
    ///  of the number of remaining soldiers.
    /// </summary>
    /// <param name="humanUnit">The human to dispose</param>
    void healSquad(Unit humanUnit )
    {
       
        var percentageOfHpToHeal = ( humanUnit.Hp / soldiers.Count );
        
        foreach (var soldier in soldiers)
        {
            soldier.Hp += soldier.Hp * ( 1 + percentageOfHpToHeal );
        }

        // dispose of the human unit
        removeHuman(humanUnit);
    }

    /// <summary>
    /// Abandon a specified amount of units.
    /// </summary>
    /// <param name="nbUnits">The number of units to abandon</param>
    void AbandonUnits(int nbUnits)
    {
        humans.RemoveRange(0,nbUnits);
    }

    /// <summary>
    /// Remove the selected soldier from the unit list.
    /// </summary>
    /// <param name="soldierUnit">the corresponding soldier that we want to remove</param>
    void removeSoldier(Unit soldierUnit)
    {
        soldiers.Remove(soldierUnit);
    }

    /// <summary>
    /// Remove the selected human from the human list.
    /// </summary>
    /// <param name="humanUnit">the corresponding human that we want to remove</param>
    void removeHuman(Unit humanUnit)
    {
        humans.Remove(humanUnit);
    }

    void transformHuman()
    {
        
    }
}
