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
    private List<Unit> _humans;
    
    /// <summary>
    /// List of attackable units.
    /// </summary>
    private List<Squad> _soldiers;

    /// <summary>
    /// List of abandonned units. (this list is accessible for both teams)
    /// </summary>
    private List<Unit> _abandonnedUnits;
    
    /// <summary>
    /// Index of the first soldier in the list.
    /// </summary>
    private int squadLeaderPosition;
    
    /// <summary>
    /// The tag to assign for each soldiers in the squad
    /// </summary>
    //protected String SquadTag;
    
    #endregion

    // Use this for initialization
    void Awake ()
    {
        _humans = new List<Unit>();
        _soldiers = new List<Squad>();
        _abandonnedUnits = new List<Unit>();
    }
    
    // Update is called once per frame
    void Update () 
    {
        // update is overriden in VampireSquad and ZombieSquad

        // TODO execute movement command
        // TODO Check if all the units are dead
            // if yes destroy 
        if (_soldiers.Count == 0)
        {
            // first release all humans ...
            AbandonUnits(_humans.Count);
            // then we destroy the squad
            //DestroyUnit();
            IsDead = true;
        }
      
    }

    #region squad related functions
    /// <summary>
    /// Assign the corresponding squad tag to each soldiers.
    /// </summary>
    protected void InitializeSquad()
    {
        if (_soldiers.Count == 0)
        {
            this._soldiers.Add(this.gameObject.GetComponent<Squad>());
        }
    }

    /// <summary>
    /// Add the human unit in the humans unit list.
    /// </summary>
    /// <param name="humanUnit">the human unit to add in the human unit list</param>
    void AddHuman(Unit humanUnit) 
    {
        _humans.Add(humanUnit);
    }

    /// <summary>
    /// Add the human unit in the abandonned unit list.
    /// </summary>
    /// <param name="soldierUnit">the soldier unit to add in the soldier unit list</param>
    void AddSoldier(Squad soldierUnit)
    {
        soldierUnit.IsCaptured = false;
        _soldiers.Add(soldierUnit);
    }

    /// <summary>
    /// Add the human unit in the abandonned unit list.
    /// </summary>
    /// <param name="humanUnit">the human to add in the abandonned unit list</param>
    void AddAbandonnedHuman(Unit humanUnit)
    {
        humanUnit.IsCaptured = false;
        _abandonnedUnits.Add(humanUnit);
    }

    /// <summary>
    /// Remove the human unit from the abandonned unit list.
    /// </summary>
    /// <param name="humanUnit">the human unit to remove</param>
    void RemoveAbandonnedHuman(Unit humanUnit)
    {
        _abandonnedUnits.Remove(humanUnit);
    }

    /// <summary>
    /// Dispose of a human unit and heal all the soldiers with a percentage depending 
    /// of the number of remaining soldiers.
    /// </summary>
    /// <param name="humanUnit">The human to dispose</param>
    public void HealSquad(Unit humanUnit)
    {
       
        var percentageOfHpToHeal = ( humanUnit.Hp / _soldiers.Count );
        
        foreach (var soldier in _soldiers)
        {
            soldier.Hp += soldier.Hp * ( 1 + percentageOfHpToHeal );
        }

        humanUnit.GetComponent<CharacterBehavior>().PlayFetchedAnimation();
        // dispose of the human unit
        RemoveHuman(humanUnit);
    }

    /// <summary>
    /// Abandon a specified amount of units.
    /// </summary>
    /// <param name="nbUnits">The number of units to abandon</param>
    public void AbandonUnits(int nbUnits)
    {
        if (nbUnits <= _humans.Count)
        {
            for (var i = 0; i < nbUnits; i++)
            {
                // retreive the human at the specified index
                var humanUnit = _humans.ElementAt(i);
                // reassign the human attributes
                humanUnit.Tag = TagLayerManager.Human;
                humanUnit.Layer = TagLayerManager.HumanLayerIndex;
                humanUnit.gameObject.GetComponent<Rigidbody>().useGravity = true;

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
        _soldiers.Remove(soldierUnit);
    }

    /// <summary>
    /// Remove the selected human from the human list.
    /// </summary>
    /// <param name="humanUnit">the corresponding human that we want to remove</param>
    void RemoveHuman(Unit humanUnit)
    {
        _humans.Remove(humanUnit);
    }

    /// <summary>
    /// Transform the human to the corresponding type of the squad.
    /// </summary>
    /// <param name="nbHumans">the number of humans to transform</param>
    public void TransformHuman(int nbHumans)
    {
        if (nbHumans <= _humans.Count)
        {
            for (var i = 0; i < nbHumans; i++)
            {
                // retreive the human at the specified index
                var humanUnit = _humans.ElementAt(i);
               
                // remove the human from the human list
                RemoveHuman(humanUnit);

                // set the human tag to the same as the squad
                humanUnit.Tag = Tag;
              
                // AddSoldier((VampireSquad) humanUnit) ) (VampireSquad or ZombieSquad)
                if (Tag.Equals(TagLayerManager.VampirePlayer))
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
        foreach (var soldier in _soldiers)
        {
            soldier.Hp -= amountOfDamage;
            Debug.Log(string.Format("{0} received {1} damage!", soldier.name, amountOfDamage));
        }
    }

    protected void CaptureHuman(Unit unit)
    {
        // set the position of the squad as a target, so that 
        // the unit would always follow the squad leader
        unit.TargetDestination = this.transform;
        // specify that the unit is captured to that the captured animation plays at the next update.
        unit.IsCaptured = true;
        // add the caught human in the caught list.
        AddHuman(unit);

        Debug.Log(string.Format("{0} joined the squad of : {1} ", unit.gameObject.name, transform.gameObject.name));
    }

    protected void AttackEnemySquad(Squad targettedEnemySquad)
    {
        //TODO improve this method add the total of squad damage or to compute the reduce of hp, etc...
        // compute the amount of hp reduced to this unit
        //unit.Hp -= Attack; // we remove some hp of the unit that was 
        targettedEnemySquad.ReceiveDamage(ComputeAttackDamage());

        Debug.Log("Attacked the enemy : " + targettedEnemySquad.Tag);
    }

    /// <summary>
    /// Compute to attack damage depending of the numbers of soldiers in the squad.
    /// </summary>
    /// <returns>the damage to apply to each enemy soldiers units</returns>
    int ComputeAttackDamage()
    {
        // LINQ + Resharper FTW!!!!!
        var sumOfAttack = _soldiers.Sum(soldier => soldier.Attack);

        return ( 1 + (sumOfAttack / _soldiers.Count));
    }

    #endregion


    #region squad accessors and mutators

    /// <summary>
    /// Return the list of the abandonned units to the ennemy team
    /// </summary>
    /// <returns></returns>
    public List<Unit> GetAbandonnedUnits()
    {
        return _abandonnedUnits;
    }

    #endregion
}
