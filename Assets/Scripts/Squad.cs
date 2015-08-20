using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Squad : Unit
{
    #region class members
    public float TimeBetweenAttacks = 0.5f;
    public string TeamTag;
    /// <summary>
    /// List of non attackable units.
    /// </summary>
    public List<Unit> Humans;
    
    /// <summary>
    /// List of attackable units.
    /// </summary>
    public List<Squad> Soldiers;

    private bool _playerInRange;
    private Squad _enemySquad;
    private float _timer;

    /// <summary>
    /// The tag to assign for each soldiers in the squad
    /// </summary>
    //protected String SquadTag;
    private int _numberOfAliveSoldiers;

    #endregion

    #region Unity functions
    // Use this for initialization
    void Start ()
    {
        Humans = new List<Unit>();
        Soldiers = new List<Squad>();
      
        InitializeSquad();
        InitializeDefaultTagAndLayer();
        CurrentHP = ComputeTotalHp();
       // Debug.Log("Current Zomb HP " + CurrentHP);
        Attack = ComputeAttackDamage();
        _numberOfAliveSoldiers = ComputeNumberOfAliveSoldiers();
        _enemySquad = null;
        _timer = 0f;
    }

    //void Awake()
    //{
        
       /* InitializeSquad();
        InitializeDefaultTagAndLayer();
        CurrentHP = 255;
        Debug.Log("Current HP" + CurrentHP) ;
        Attack = ComputeAttackDamage();
        _enemySquad = null;
        _timer = 0f;*/
    //}
    // Update is called once per frame
    void Update ()
    {
        CurrentHP = ComputeTotalHp();
        _numberOfAliveSoldiers = ComputeNumberOfAliveSoldiers();

        _timer += Time.deltaTime;

        if (_timer >= TimeBetweenAttacks && _playerInRange && !IsDead)
        {
            AttackEnemySquad(_enemySquad);
        }
       
        // TODO execute movement command
        // TODO Check if all the units are dead
            // if yes destroy 

        if (_numberOfAliveSoldiers > 0) return;
        // first release all humans ...
        AbandonUnits(Humans.Count);
        // then we destroy the squad
        //DestroyUnit();
        IsDead = true;
    }

    void OnTriggerEnter(Collider collider)
    {
        if (IsDead) return;
        //var objectTag = collider.gameObject;
        // check if the game object have an attached Unit class
        //switch(objectTag.GetType())
        var unitComponent = collider.GetComponent<Unit>();

        if (unitComponent == null)
            return;
        // check if the unit is not a friendly one    
        if (this.Tag == unitComponent.Tag)
            return;

        if (unitComponent.Tag.Equals(TagLayerManager.Human))
        {
            if (unitComponent.IsCaptured)
            {
                return;
            }
            else
            {
                CaptureHuman(unitComponent);
            }
        }
        else // we know that it's an enemy
        {
            if (unitComponent.IsDead)
                return;
            _playerInRange = true;
            _enemySquad = collider.GetComponent<Squad>();

            /* try
                {
                    AttackEnemySquad(unitComponent as Squad);
                }
                catch (InvalidCastException exception)
                {
                    Debug.LogError(exception.ToString());
                    //throw;
                }*/

        }
    }

    void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.tag != Tag && collider.gameObject.tag != TagLayerManager.Human)
        {
            _playerInRange = false;
            this.gameObject.GetComponent<CharacterBehavior>().PlayAttackAnimation(false);
            _enemySquad = null;
           // collider.GetComponent<CharacterBehavior>().PlayAttackAnimation(false);
        }
    }

    #endregion

    #region squad related functions

    public int ComputeNumberOfAliveSoldiers()
    {
        // LINQ + Resharper FTW!!!!!
        return Soldiers.Count(soldier => !soldier.IsDead);
    }

    /// <summary>
    /// Assign the corresponding squad tag to each soldiers.
    /// </summary>
    protected void InitializeSquad()
    {
        if (Soldiers.Count == 0)
        {
            this.Soldiers.Add(this.gameObject.GetComponent<Squad>());
        }
    }

    private void InitializeDefaultTagAndLayer()
    {
        try
        {
            if (this.TeamTag.Length == 0)
            {
                this.Tag = TagLayerManager.VampirePlayer;
                this.Layer = TagLayerManager.VampireLayerIndex;
            }
            else
            {
                if (Tag.Equals(TagLayerManager.VampirePlayer))
                {
                    this.Tag = TagLayerManager.VampirePlayer; // set the tag to player 1      
                    this.Layer = TagLayerManager.VampireLayerIndex;
                }
                else
                {
                    this.Tag = TagLayerManager.ZombiePlayer; // set the tag to player 2      
                    this.Layer = TagLayerManager.ZombieLayerIndex;
                }
            }
        }
        catch (IndexOutOfRangeException ex)
        {
            Debug.LogError("Please set a vampire Tag, check the Tag & layers in the inspector!\n" + ex);
        }

        // set the tag and the layer of the gameObject to vampire
        if (this.gameObject.tag != Tag)
        {
            this.gameObject.tag = Tag;
        }
        if (this.gameObject.layer != Layer)
        {
            this.gameObject.layer = Layer;
        }
    }

    /// <summary>
    /// Add the human unit in the humans unit list.
    /// </summary>
    /// <param name="humanUnit">the human unit to add in the human unit list</param>
    void AddHuman(Unit humanUnit) 
    {
        Humans.Add(humanUnit);
    }

    /// <summary>
    /// Add the human unit in the abandoned unit list.
    /// </summary>
    /// <param name="soldierUnit">the soldier unit to add in the soldier unit list</param>
    void AddSoldier(Squad soldierUnit)
    {
        soldierUnit.IsCaptured = false;
        Soldiers.Add(soldierUnit);
    }

    /// <summary>
    /// Dispose of a human unit and heal all the soldiers with a percentage depending 
    /// of the number of remaining soldiers.
    /// </summary>
    /// <param name="humanUnit">The human to dispose</param>
    public void HealSquad(Unit humanUnit)
    {
       
        var percentageOfHpToHeal = ( humanUnit.CurrentHP / Soldiers.Count );
        
        foreach (var soldier in Soldiers)
        {
            soldier.CurrentHP += soldier.CurrentHP * ( 1 + percentageOfHpToHeal );
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
        if (nbUnits <= Humans.Count)
        {
            for (var i = 0; i < nbUnits; i++)
            {
                // retrieve the human at the specified index
                var humanUnit = Humans.ElementAt(i);
                // reassign the human attributes
                humanUnit.Tag = TagLayerManager.Human;
                humanUnit.Layer = TagLayerManager.HumanLayerIndex;
                humanUnit.gameObject.GetComponent<Rigidbody>().useGravity = true;

                // add the human to the abandoned Unit list
                humanUnit.IsCaptured = false;
                Debug.Log(string.Format("{0} has abandoned the unit {1}", this.gameObject.name, humanUnit.gameObject.name));
                
                // remove the human from the humandUnit that was added to the abandoned unit list
                RemoveHuman(humanUnit);
            }
        }
        else
        {
            Debug.LogError("Exceeded the maximum numbers of units in the humans list!");
        }   
    }

    /// <summary>
    /// Remove the selected soldier from the unit list.
    /// </summary>
    /// <param name="soldierUnit">the corresponding soldier that we want to remove</param>
    void RemoveSoldier(Squad soldierUnit)
    {
        Soldiers.Remove(soldierUnit);
    }

    /// <summary>
    /// Remove the selected human from the human list.
    /// </summary>
    /// <param name="humanUnit">the corresponding human that we want to remove</param>
    void RemoveHuman(Unit humanUnit)
    {
        Humans.Remove(humanUnit);
    }

    /// <summary>
    /// Transform the human to the corresponding type of the squad.
    /// </summary>
    /// <param name="nbHumans">the number of humans to transform</param>
    public void TransformHuman(int nbHumans)
    {
        if (nbHumans <= Humans.Count)
        {
            for (var i = 0; i < nbHumans; i++)
            {
                // retrieve the human at the specified index
                var humanUnit = Humans.ElementAt(i);
               
                // remove the human from the human list
                RemoveHuman(humanUnit);

                // set the human tag to the same as the squad
                humanUnit.Tag = Tag;
              
                AddSoldier(humanUnit as Squad);
                // AddSoldier((VampireSquad) humanUnit) ) (VampireSquad or ZombieSquad)
                /*if (Tag.Equals(TagLayerManager.VampirePlayer))
                {
                    // add the vampire to the soldier list
                    AddSoldier((VampireSquad) humanUnit);
                }
                else
                {
                    // add the zombie to the soldier list
                    AddSoldier((ZombieSquad)humanUnit);
                }*/
            }
        }
        else
        {
            Debug.LogError("Exceeded the maximum number of humans in the humans list!");
        }
       
    }

    public void ReceiveDamage(int amountOfDamage)
    {
        // apply the damage to the first soldier in the list
        foreach (var soldier in Soldiers)
        {
            //soldier.CurrentHP -= amountOfDamage;
            soldier.TakeDamage(amountOfDamage);
            Debug.Log(string.Format("{0} received {1} damage!", soldier.name, amountOfDamage));
            Debug.Log(string.Format("{0} remaining hp : {1}", soldier.name, soldier.CurrentHP));
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
        _timer = 0f;

        if (targettedEnemySquad.CurrentHP > 0)
        {
            foreach (var soldier in Soldiers)
            {
                soldier.GetComponent<CharacterBehavior>().PlayAttackAnimation(true);
            }

            targettedEnemySquad.ReceiveDamage(ComputeAttackDamage());
            Debug.Log(string.Format("{0} Attacked the enemy : {1} ", this.gameObject.name, targettedEnemySquad.gameObject.name));
        }
        else
        {
            foreach (var soldier in Soldiers)
            {
                soldier.GetComponent<CharacterBehavior>().PlayAttackAnimation(false);
            }
        }
        //TODO improve this method add the total of squad damage or to compute the reduce of hp, etc...
        // compute the amount of hp reduced to this unit
        //unit.Hp -= Attack; // we remove some hp of the unit that was 
       // targettedEnemySquad.ReceiveDamage(ComputeAttackDamage());
    }

    /// <summary>
    /// Compute to attack damage depending of the numbers of soldiers in the squad.
    /// </summary>
    /// <returns>the damage to apply to each enemy soldiers units</returns>
    protected int ComputeAttackDamage()
    {
        // LINQ + Resharper FTW!!!!!
        var sumOfAttack = Soldiers.Sum(soldier => soldier.Attack);

        return ( 1 + (sumOfAttack / Soldiers.Count));
    }

    protected int ComputeTotalHp()
    {
        var sumOfHp = Soldiers.Sum(x => x.CurrentHP);

        return sumOfHp;
    }

    #endregion

    #region squad accessors and mutators

    #endregion

}
