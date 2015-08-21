using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine.UI;

public class Unit : MonoBehaviour 
{
  
    public float MaxHealth = 250;
    public float StartingAttack = 100;
    public Image HealthImage;
    public float CapturedLerpSpeed = 1;
    public int DestinationGap = 5;
    public float DyingTime = 1.0f;
    public AudioClip DeathClip;

    private LinkedList<Command> commandList;
    private float _currentHp; // the unit hp
    private CharacterBehavior _character;
    private bool isDamaged;


    // Use this for initialization
    void Awake ()
    {
        IsCaptured = false;
        CurrentHP = MaxHealth;
       // Debug.Log("Current Human HP: " + CurrentHP);
        Attack = StartingAttack;
        // initialize default specie
        Tag = TagLayerManager.Human;
        Layer = TagLayerManager.HumanLayerIndex;
        IsDead = false;
        _character = GetComponent<CharacterBehavior>();
    }

    void Update()
    {
        HealthImage.fillAmount = (CurrentHP / MaxHealth);

        if (IsCaptured)
        {
           var gapVector = new Vector3(TargetDestination.position.x + DestinationGap, TargetDestination.position.y,TargetDestination.position.z + DestinationGap);
            // TODO improve the translation position so that every unit captured are around the squad leader and not at only one position.
            transform.position = Vector3.Lerp(transform.position, gapVector, CapturedLerpSpeed * 3.0f * Time.deltaTime);
            //See more at: http://unitydojo.blogspot.ca/2014/03/how-to-use-lerp-in-unity-like-boss.html#sthash.ueWlstRk.dpuf*/
            _character.PlayCaptureAnimation(IsCaptured);
        }
        else
        {
            _character.PlayCaptureAnimation(IsCaptured);
        }

        isDamaged = false;
    }

    public void TakeDamage(float amountOfDamage)
    {
        isDamaged = true;
        CurrentHP -= amountOfDamage;

       

        // TODO play hurt animation
        // TODO play hurt sound

        if (CurrentHP <= 0 && !IsDead)
        {
            Death();
        }
    }

    public void Death()
    {
        IsDead = true;
        //_character.PlayDeathAnimation();
        // TODO play death sound
        // TODO disable this gameobject movements.
        Debug.Log("Entered death function!");
        StartCoroutine(DestroyUnit(DyingTime));
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
    protected IEnumerator DestroyUnit(float dyingTime)
    {
        //TODO First play dead animation
        // retrieve the character behavior object, so that we can access it's animation properties...
        // play the death animation 
        _character.PlayDeathAnimation();
        yield return new WaitForSeconds(_character.CurrrentAnimationLength());

        // then destroy the game object only 3 seconds after the animation
        //Destroy(this.transform.gameObject, dyingTime);
    }

    #region Unit properties
    public bool IsCaptured { get; set; }
    public float CurrentHP
    {
        get { return _currentHp; }

        set
        {
            _currentHp = value > MaxHealth ? MaxHealth : value;
        }
    }

    public float Attack { get; set; }
    public bool IsDead { get; set; }

    public Transform TargetDestination { get; set; }

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
