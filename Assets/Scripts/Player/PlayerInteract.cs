using UnityEngine;
using UnityEngine.InputSystem;
/// <summary>
/// Handles interaction on the players side - Linnéa
/// </summary>
public class PlayerInteract : MonoBehaviour
{
    [Tooltip("How far you can reach.")]
    [SerializeField] private float _reachDistance = 2.0f;
    private Camera _mainCamera;

    private void Awake()
    {
        _mainCamera = Camera.main;
    }
    /// <summary>
    /// Input function called by the PlayerInput component. Raycast to see if there is something you should be able to
    /// interact with in front of you. (Stuff with IInteract on.) If yes, it triggers interact on obj.
    /// </summary>
    /// <param name="context">Read from the unity event.</param>
    public void OnInteract(InputAction.CallbackContext context)
    {
        if (!context.started) return;
        
        if (Physics.Raycast(_mainCamera.transform.position, _mainCamera.transform.forward, out RaycastHit HitInfo, _reachDistance))
        {
            HitInfo.transform.GetComponent<IInteract>()?.Interact();
            Debug.DrawRay(_mainCamera.transform.position, _mainCamera.transform.forward * _reachDistance, Color.yellow);
        }
    }
}
