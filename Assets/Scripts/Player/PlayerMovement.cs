using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Controls the players movement such as jumping, walking and changing direction. The input is triggered from unity
/// events in the playerInput component, should prbly be the player obj.- Linnéa
/// </summary>
[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [Tooltip("The players speed.")]
    [SerializeField] private float _walkSpeed = 1;
    [Tooltip("The players sprint speed.")] 
    [SerializeField]private float _runSpeed = 2;
    [Tooltip("How fast the player should turn.")]
    [SerializeField] private float _turnSpeed = 1;
    [Tooltip("How high you jump.")]
    [SerializeField] private float _jumpForce = 2;
    [Tooltip("If the feet should reach further down than the collider.")]
    [SerializeField] private float _extraFeetReach = 0.01f;
    private float _feetPos;
    private bool _shouldMove;
    private Vector3 _direction;
    private float _turnDirection;
    private bool _jumping;
    private Rigidbody _rb;
    private float _speed;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
        _feetPos = GetComponent<CapsuleCollider>().height / 2;
        _feetPos += _extraFeetReach;
        _speed = _walkSpeed;
    }
    private void Update()
    {
        if(_shouldMove) Move();
    }

    /// <summary>
    /// Set the RigidBodys velocity to the direction given from OnMove()
    /// </summary>
    private void Move()
    {
        _direction.y = _rb.velocity.y;
        _rb.velocity =
            transform.TransformDirection(new Vector3(_direction.x * _speed, _direction.y, _direction.z * _speed));
    }

    
    /// <summary>
    /// Checks if the player's standing on the ground or not. 
    /// </summary>
    /// <returns></returns>
    private bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, _feetPos);
    }
    #region InputRelated
    /// <summary>
    /// Input function called by the PlayerInput component. Sets the players movement direction when a button is pushed. _shouldMove is triggered for as long as the button
    /// is pushed.
    /// </summary>
    /// <param name="context">Read from the unity event.</param>
    public void OnMove(InputAction.CallbackContext context)
    {
        _direction = context.ReadValue<Vector3>();
        if(_direction.magnitude > 1) _direction.Normalize();
        _shouldMove = context.performed;
    }

    /// <summary>
    /// Input function called by the PlayerInput component. The player turns based on where the mouse is positioned. Rotates the whole player object to make raycasts and so
    /// on still accurate.
    /// </summary>
    /// <param name="context">Read from the unity event.</param>
    public void OnTurn(InputAction.CallbackContext context)
    {
        _turnDirection = context.ReadValue<float>();
        transform.Rotate(0, _turnDirection * _turnSpeed/10, 0);
    }
    /// <summary>
    /// Input function called by the PlayerInput component. Adds a force upwards to make the player jump.
    /// </summary>
    /// <param name="context">Read from the unity event.</param>
    public void OnJump(InputAction.CallbackContext context)
    {
        if(context.started && IsGrounded())
        {
            _rb.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
        }
    }
    /// <summary>
    /// Changes speed based on if sprinting or not
    /// </summary>
    /// <param name="context"></param>
    public void OnSprint(InputAction.CallbackContext context)
    {
        _speed = context.performed ? _runSpeed : _walkSpeed;
    }
    #endregion

    private void OnDrawGizmos()
    {
        Debug.DrawRay(transform.position, Vector3.down * _feetPos, Color.blue);
        
    }
}
