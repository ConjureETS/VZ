using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Squad : Unit
{
    #region class members

    /// <summary>
    /// List of non attackable units.
    /// </summary>
    private List<Unit> humans;
    
    /// <summary>
    /// List of attackable units.
    /// </summary>
    private List<Squad> soldiers;

    /// <summary>
    /// List of abandonned units. (this list is accessible for both teams)
    /// </summary>
    private List<Unit> abandonnedUnits;
    
    /// <summary>
    /// Index of the first soldier in the list.
    /// </summary>
    private int targetSoldierIndex;
    
    /// <summary>
    /// The tag to assign for each soldiers in the squad
    /// </summary>
    protected String squadTag;
    
    #endregion

    // Use this for initialization
    void Awake ()
    {
        humans = new List<Unit>();
        soldiers = new List<Squad>();
    }
    
    // Update is called once per frame
    void Update () 
    {
        // TODO execute movement command
        // TODO Check if all the units are dead
            // if yes destroy 
        if (soldiers.Count != 0) return;
        // first release all humans ...
        AbandonUnits(humans.Count);
        // then we destroy the squad
        DestroyUnit();
    }

    #region squad related functions
    /// <summary>
    /// Assign the corresponding squad tag to each soldiers.
    /// </summary>
    protected void InitializeSquad()
    {
        if (soldiers.Count == 0)
        {
            this.soldiers.Add(this.gameObject.GetComponent<Squad>());
        }
    }

    /// <summary>
    /// Add the human unit in the humans unit list.
    /// </summary>
    /// <param name="humanUnit">the human unit to add in the human unit list</param>
    void AddHuman(Unit humanUnit) 
    {
        humans.Add(humanUnit);
    }

    /// <summary>
    /// Add the human unit in the abandonned unit list.
    /// </summary>
    /// <param name="soldierUnit">the soldier unit to add in the soldier unit list</param>
    void AddSoldier(Squad soldierUnit)
    {
        soldiers.Add(soldierUnit);
    }

    /// <summary>
    /// Add the human unit in the abandonned unit list.
    /// </summary>
    /// <param name="humanUnit">the human to add in the abandonned unit list</param>
    void AddAbandonnedHuman(Unit humanUnit)
    {
        abandonnedUnits.Add(humanUnit);
    }

    /// <summary>
    /// Remove the human unit from the abandonned unit list.
    /// </summary>
    /// <param name="humanUnit">the human unit to remove</param>
    void RemoveAbandonnedHuman(Unit humanUnit)
    {
        abandonnedUnits.Remove(humanUnit);
    }

    /// <summary>
    /// Dispose of a human unit and heal all the soldiers with a percentage depending 
    /// of the number of remaining soldiers.
    /// </summary>
    /// <param name="humanUnit">The human to dispose</param>
    public void HealSquad(Unit humanUnit)
    {
       
        var percentageOfHpToHeal = ( humanUnit.Hp / soldiers.Count );
        
        foreach (var soldier in soldiers)
        {
            soldier.Hp += soldier.Hp * ( 1 + percentageOfHpToHeal );
        }

        // dispose of the human unit
        RemoveHuman(humanUnit);
    }

    /// <summary>
    /// Abandon a specified amount of units.
    /// </summary>
    /// <param name="nbUnits">The number of units to abandon</param>
    public void AbandonUnits(int nbUnits)
    {
        if (nbUnits <= humans.Count)
        {
            for (var i = 0; i < nbUnits; i++)
            {
                // retreive the human at the specified index
                var humanUnit = humans.ElementAt(i);
                // add the human to the abandonned Unit list
                AddAbandonnedHuman(humanUnit);
                // remove the human from the humandUnit that was added to the abandonned unit list
                RemoveHuman(humanUnit);
            }
        }
        else
        {
            Debug.LogError("Exceded the maximum numbers of units in the humans list!");
        }   
    }

    /// <summary>
    /// Remove the selected soldier from the unit list.
    /// </summary>
    /// <param name="soldierUnit">the corresponding soldier that we want to remove</param>
    void RemoveSoldier(Squad soldierUnit)
    {
        soldiers.Remove(soldierUnit);
    }

    /// <summary>
    /// Remove the selected human from the human list.
    /// </summary>
    /// <param name="humanUnit">the corresponding human that we want to remove</param>
    void RemoveHuman(Unit humanUnit)
    {
        humans.Remove(humanUnit);
    }

    /// <summary>
    /// Transform the human to the corresponding type of the squad.
    /// </summary>
    /// <param name="nbHumans">the number of humans to transform</param>
    public void TransformHuman(int nbHumans)
    {
        if (nbHumans <= humans.Count)
        {
            for (var i = 0; i < nbHumans; i++)
            {
                // retreive the human at the specified index
                var humanUnit = humans.ElementAt(i);
               
                // remove the human from the human list
                RemoveHuman(humanUnit);

                // set the human tag to the same as the squad
                humanUnit.Tag = Tag;

                // AddSoldier((VampireSquad) humanUnit) ) (VampireSquad or ZombieSquad)
                if (squadTag.Equals(TagManager.VampirePlayer))
                {
                    // add the vampire to the soldier list
                    AddSoldier((VampireSquad) humanUnit);
                }
                else
                {
                    // add the zombie to the soldier list
                    AddSoldier((ZombieSquad)humanUnit);
                }
            }
        }
        else
        {
            Debug.LogError("Exceded the maximum nb of humans in the humans list!");
        }
       
    }

    public void ReceiveDamage(int amountOfDamage)
    {
        // apply the damage to the first soldier in the list
        // update the soldier hp
            // if the soldier hp reach zero
                // the soldier is destroyed
                // and then dead soldier is removed in the soldier list
        var topSoldier = soldiers.ElementAt(0);
        topSoldier.Hp -= amountOfDamage;

        Debug.Log(string.Format("{0} received {1} damage!",topSoldier.name, amountOfDamage));
    }

    void CaptureHuman(Unit unit)
    {
        AddHuman(unit);
        Debug.Log("Entered in collision with: " + unit.Tag);
    }

    protected void AttackEnemySquad(Squad targettedEnemySquad)
    {
        //TODO improve this method add the total of squad damage or to compute the reduce of hp, etc...
        // compute the amount of hp reduced to this unit
        //unit.Hp -= Attack; // we remove some hp of the unit that was 
        var amountOfDamageToApply = ComputeAttackDamage();
        targettedEnemySquad.ReceiveDamage(amountOfDamageToApply);

        Debug.Log("Attacked the ennemy : " + targettedEnemySquad.Tag);
    }

    int ComputeAttackDamage()
    {
        // TODO improve the damage algorithm depending of the number of units in the soldiers list.
        return Attack;
    }

    #endregion


    #region squad accessors and mutators

    /// <summary>
    /// Return the list of the abandonned units to the ennemy team
    /// </summary>
    /// <returns></returns>
    public List<Unit> GetAbandonnedUnits()
    {
        return abandonnedUnits;
    }

    public int NumberOfSoldiers()
    {
        return soldiers.Count();
    }
    #endregion
}
