using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(Rigidbody))]

public class CharacterBehavior : MonoBehaviour
{

    //public float AnimationSpeed = 1.5f;				
    //public float LookSmoother = 3.0f;			// a smoothing setting for camera motion
    //public float ForwardSpeed = 7.0f;
    //public float BackwardSpeed = 2.0f;
    //public float RightSpeed = 7.0f;
    //public float LeftSpeed = 7.0f;
    //public float RotateSpeed = 2.0f;
	
	private CapsuleCollider _col;
	private Rigidbody _rb;
	// speed vector
	private Vector3 _velocity;
	// origin collider height
	private float _orgColHight;
	// 
	private Vector3 _orgVectColCenter;

	private Animator _animator;							
	private AnimatorStateInfo _currentBaseState;			

	private GameObject cameraObject;	

    //static int idleState = Animator.StringToHash("Base Layer.Idle");
    //static int movingState = Animator.StringToHash("Base Layer.IsMoving");
    //static int attackState = Animator.StringToHash("Base Layer.Smash");
    ////static int restState = Animator.StringToHash("Base Layer.Rest");
    //private static int fetchState = Animator.StringToHash("Base Layer.IsEating");
    //private static int runNCaptureState = Animator.StringToHash("Base Layer.RunNCapture");
    //private static int idleCapture = Animator.StringToHash("Base Layer.IdleCapture");
    //private static int deadStage = Animator.StringToHash("Base Layer.Dead");
    //private static int fetchedState = Animator.StringToHash("Base Layer.Fetched");
	
    void Start()
	{
		// retreive the character components
		_animator = GetComponent<Animator>();
		_col = GetComponent<CapsuleCollider>();
		_rb = GetComponent<Rigidbody>();
		cameraObject = GameObject.FindWithTag("MainCamera");
		_orgColHight = _col.height;
		_orgVectColCenter = _col.center;
	}

    public void PlayRunAnimation(bool decision)
    {
        _animator.SetBool("IsMoving", decision);
    }

    public void PlayAttackAnimation(bool decision)
    {
        _animator.SetBool("IsAttacking", decision);
    }

    public void PlayCaptureAnimation(bool decision)
    {
        _animator.SetBool("IsCaptured", decision);
    }

    public void PlayDeathAnimation()
    {
        _animator.SetTrigger("Dead");
    }

    public void PlayFetchedAnimation()
    {
        _animator.SetTrigger("Fetched");
    }

    public void PlayEatingAnimation(bool decision)
    {
        _animator.SetBool("IsEating", decision);
    }

    public void PlayRunWhileCaptured(bool decision)
    {
        // TODO to test running while being captured
        PlayCaptureAnimation(decision);
        _animator.SetBool("IsMoving", decision);
    }

	void FixedUpdate()
	{
	    PlayRunAnimation(true);
        //PlayRunAnimation(false);
        //PlayAttackAnimation(true);
        //PlayAttackAnimation(false);
        //PlayCaptureAnimation(true);
        //PlayCaptureAnimation(false);
        //PlayDeathAnimation();
        //PlayFetchedAnimation();
        //PlayEatingAnimation(true);
        //PlayEatingAnimation(false);
        //PlayRunWhileCaptured(true);
        //PlayRunWhileCaptured(false);

	}
}
