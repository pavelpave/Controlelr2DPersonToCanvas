using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Drag = DragonBones;

public class PlayerCs : MonoBehaviour
{
    enum DirectionState
    {
        Right,
        Left
    }

    enum MoveState
    {
        Idle,
        Walk,
        Jump
    }

    [Header("Speeds")]
    public float WalkSpeed = 3;
    public float JumpForce = 10;
    public Drag.UnityArmatureComponent _animatorController;

    private MoveState _moveState = MoveState.Idle;
    private DirectionState _directionState = DirectionState.Right;
    private Transform _transform;
    private Rigidbody2D _rigidbody;
    private float _walkTime = 0, _walkCooldown = 0.2f;
    private bool _isStopped = true;
    private bool _corutineJumpStarted = false;
    public void MoveRight()
    {
        if (_moveState != MoveState.Jump)
        {
            _moveState = MoveState.Walk;
            if (_directionState == DirectionState.Left)
            {
                _transform.localScale = new Vector3(-_transform.localScale.x, _transform.localScale.y, _transform.localScale.z);
                _directionState = DirectionState.Right;
            }
            _isStopped = false;
            _walkTime = 10;
            _animatorController.animation.Play("go");

        }
    }

    public void MoveLeft()
    {
        if (_moveState != MoveState.Jump)
        {
            _moveState = MoveState.Walk;
            if (_directionState == DirectionState.Right)
            {
                _transform.localScale = new Vector3(-_transform.localScale.x, _transform.localScale.y, _transform.localScale.z);
                _directionState = DirectionState.Left;
            }
            _isStopped = false;
            _walkTime = 10;
            _animatorController.animation.Play("go");
        }
    }

    public void Jump()
    {
        if (_moveState != MoveState.Jump)
        {
            if (_rigidbody.velocity == Vector2.zero)
            {
                _moveState = MoveState.Jump;
                StartCoroutine(JumpCoroutine());
            }
        }
    }

    IEnumerator JumpCoroutine()
    {
        if (!_corutineJumpStarted)
        {
            _corutineJumpStarted = true;
            Debug.Log("preparationJump");
            yield return new WaitForSeconds(0.1f);
            _animatorController.animation.Play("preparationJump");
            yield return new WaitForSeconds(0.7f);
            _corutineJumpStarted = false;
            _animatorController.animation.Play("default");
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, (_rigidbody.velocity.y == 0 ? 1 : _rigidbody.velocity.y) * JumpForce);
            Debug.Log("END pRER");
        }
    }

    public void StopWalk()
    {

    }

    public void Idle()
    {
        StopWalk();
        _moveState = MoveState.Idle;
        _animatorController.animation.Play("default");
        _walkTime = 0;
    }

    private void Start()
    {
        _transform = GetComponent<Transform>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _animatorController = GetComponent<Drag.UnityArmatureComponent>();
        _directionState = transform.localScale.x > 0 ? DirectionState.Right : DirectionState.Left;
    }

    private float DirectionsWalk()
    {
        return (_directionState == DirectionState.Right ? Vector2.right.x : -Vector2.right.x) * WalkSpeed;
    }

    private void Update()
    {
        if (_moveState == MoveState.Jump)
        {
            if (_rigidbody.velocity == Vector2.zero)
            {
                Idle();
            }
        }
        else if (_moveState == MoveState.Walk)
        {
            _rigidbody.velocity = new Vector2(DirectionsWalk(), _rigidbody.velocity.y);
            //= new Vector2(WalkSpeed, _rigidbody.velocity.y);
            //new Vector2(WalkSpeed, _rigidbody.velocity.y);
            // ((_directionState == DirectionState.Right ? Vector2.right : -Vector2.right) *
            //     WalkSpeed * Time.deltaTime);
            _walkTime -= Time.deltaTime;
            if (_isStopped)
            {
                Idle();
            }
        }
    }
}