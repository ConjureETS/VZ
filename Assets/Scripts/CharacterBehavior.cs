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
	// LerpSpeed vector
	private Vector3 _velocity;
	// origin collider height
	// 
	private Animator _animator;							
	private AnimatorStateInfo _currentAnimatorStateInfo;			

	private GameObject _cameraObject;
    private static int _baseLayerIndex = 0;
    static int _idleState = Animator.StringToHash("Idle");
    static readonly int MovingState = Animator.StringToHash("IsMoving");
    static readonly int AttackState = Animator.StringToHash("IsAttacking");
    ////static int restState = Animator.StringToHash("Base Layer.Rest");
    static readonly int FetchState = Animator.StringToHash("IsEating");
    static int _runNCaptureState = Animator.StringToHash("RunNCapture");
    static readonly int IdleCapture = Animator.StringToHash("IsCaptured");
    static readonly int DeadState = Animator.StringToHash("Dead");
    static readonly int FetchedState = Animator.StringToHash("Fetched");
	
    void Start()
	{
		// retreive the character components
		_animator = GetComponent<Animator>();
		_col = GetComponent<CapsuleCollider>();
		_rb = GetComponent<Rigidbody>();
		_cameraObject = GameObject.FindWithTag("MainCamera");
        // return the state of the animation that is currently playing
        _currentAnimatorStateInfo = _animator.GetCurrentAnimatorStateInfo(_baseLayerIndex);
	}

    public float CurrrentAnimationLength()
    {
        //_currentAnimatorStateInfo = _animator.GetCurrentAnimatorStateInfo(_baseLayerIndex);
        
        // TODO Find a way to get the current animation length for each clip that is playing...
        Debug.Log(_currentAnimatorStateInfo.fullPathHash.Equals(DeadState)
            ? "Vamp is dead"
            : _currentAnimatorStateInfo.fullPathHash.ToString());

        return (_currentAnimatorStateInfo.normalizedTime % 1);
    }

    public void PlayRunAnimation(bool decision)
    {
        _animator.SetBool(MovingState, decision);
    }

    public void PlayAttackAnimation(bool decision)
    {
        _animator.SetBool(AttackState, decision);
    }

    public void PlayCaptureAnimation(bool decision)
    {
        _animator.SetBool(IdleCapture, decision);
    }

    public void PlayDeathAnimation()
    {
        _animator.SetTrigger(DeadState);
    }

    public void PlayFetchedAnimation()
    {
        _animator.SetTrigger(FetchedState);
    }

    public void PlayEatingAnimation(bool decision)
    {
        _animator.SetBool(FetchState, decision);
    }

    public void PlayRunWhileCaptured(bool decision)
    {
        // TODO to test running while being captured
        PlayCaptureAnimation(decision);
        _animator.SetBool(MovingState, decision);
    }

    public Animator GeAnimator()
    {
        return _animator;
    }
	void FixedUpdate()
	{
	    //PlayRunAnimation(true);
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
