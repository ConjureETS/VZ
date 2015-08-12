using System;
using UnityEngine;
using System.Collections;
/// <summary>
/// This class contains the information of the Vampire Units.
/// </summary>
public class ZombieUnit : Unit
{
    // Use this for initialization

    void Start()
    {
        InitializeDefaultTag();
        // initialize default hp
        //Hp = defaultHp;
        // initialize default attack
        Attack = defaultAttack;
        // initialize default team
        // initialize default specie
        IsDead = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsDead)
        {
            // TODO play dead animation before destroying unit
            DestroyUnit();
        }
    }

    private void InitializeDefaultTag()
    {
        try
        {
            this.Tag = TagManager.ZombiePlayer; // set the tag to player 1       
        }
        catch (IndexOutOfRangeException ex)
        {
            Debug.LogError("Please set a vampire Tag, check the Tag & layers in the inspector!\n" + ex);
        }

        // set the tag of the gameObject to vampire
        if (this.gameObject.tag != Tag)
        {
            this.gameObject.tag = Tag;
        }
    }

    void CaptureHuman(Unit unit)
    {
        // TODO either add the human as a squad member or change it's tag to vampireHuman
        // when the player is transformed we just make VampireUnit vampireUnit2 = (VampireUnit) unit;
        Debug.Log("Entered in collision with: " + unit.Tag);
    }
    void AttackEnemy(Unit unit)
    {
       
        // compute the amount of hp reduced to this unit
        unit.Hp -= Attack; // we remove some hp of the unit that was 

        Debug.Log("Attacked the ennemy : " + unit.Tag);
    }

    void OnTriggerEnter(Collider collider)
    {
        //var objectTag = collider.gameObject;
        // check if the game object have an attached Unit class
        //switch(objectTag.GetType())
        var unitComponent = collider.GetComponent<Unit>();

        if (unitComponent != null)
        {
            // check if the unit is not a friendly one    
            if (this.Tag != unitComponent.Tag)
            {
                if (unitComponent.Tag.Equals(TagManager.Human))
                {
                    CaptureHuman(unitComponent);
                }
                else // we know that it's an ennemy
                {
                    AttackEnemy(unitComponent);
                }
            }
        }
    }
}
