using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Controls the players movement such as jumping, walking and changing direction. - Linnéa
/// </summary>
[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [Tooltip("The players speed.")]
    [SerializeField] private float _speed = 1;
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

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
        _feetPos = GetComponent<CapsuleCollider>().height / 2;
        _feetPos += _extraFeetReach;
    }
    private void Update()
    {
        if(_shouldMove)_rb.velocity = transform.TransformDirection(_direction) * _speed;
        //if(!_shouldMove && IsGrounded()) _rb.velocity = Vector3.zero;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        _direction = context.ReadValue<Vector3>();
        if(_direction.magnitude > 1) _direction.Normalize();
        _shouldMove = context.performed;
    }

    public void OnTurn(InputAction.CallbackContext context)
    {
        //The player should turn based on where the mouse is positioned
        _turnDirection = context.ReadValue<float>();
        transform.Rotate(0, _turnDirection * _turnSpeed/10, 0);
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if(context.started && IsGrounded())
        {
            _rb.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
        }
    }

    private bool IsGrounded()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, _feetPos))
        {
            return true;
        }

        return false;
    }

    private void OnDrawGizmos()
    {
        Debug.DrawRay(transform.position, Vector3.down * _feetPos, Color.blue);
        
    }
}
