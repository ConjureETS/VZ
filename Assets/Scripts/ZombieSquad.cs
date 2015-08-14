using System;
using UnityEngine;
using System.Collections;
/// <summary>
/// This class contains the information of the Vampire Units.
/// </summary>
public class ZombieSquad : Squad
{
    // Use this for initialization

    void Start()
    {
        InitializeSquad();
        InitializeDefaultTagAndLayer();
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

    private void InitializeDefaultTagAndLayer()
    {
        try
        {
            this.Tag = TagLayerManager.ZombiePlayer; // set the tag to player 1      
            this.Layer = TagLayerManager.ZombieLayerIndex;
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

    /*void CaptureHuman(Unit unit)
    {
        // TODO either add the human as a squad member or change it's tag to vampireHuman
        // when the player is transformed we just make VampireSquad vampireUnit2 = (VampireSquad) unit;
        Debug.Log("Entered in collision with: " + unit.Tag);
    }*/
    /*void AttackEnemySquad(Unit unit)
    {
       
        // compute the amount of hp reduced to this unit
        unit.Hp -= Attack; // we remove some hp of the unit that was 

        Debug.Log("Attacked the ennemy : " + unit.Tag);
    }*/

    void OnTriggerEnter(Collider collider)
    {
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
            CaptureHuman(unitComponent);
        }
        else // we know that it's an ennemy
        {
            try
            {
                AttackEnemySquad(unitComponent as Squad);
            }
            catch (InvalidCastException exception)
            {
                Debug.LogError(exception.ToString());
                //throw;
            }

        }
    }
}
