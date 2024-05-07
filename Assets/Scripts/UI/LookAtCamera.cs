using UnityEngine;


/// <summary>
/// A script for Ui elements, allowing for different type of ui presentation with respect for the player camera
/// - Kevin
/// </summary>
public class LookAtCamera : MonoBehaviour
{
    private enum UIMode
    {
        LookAt,
        LookAtInverted,
        CameraForward,
        CameraForwardInverted
    }

    [SerializeField]
    private UIMode mode;
    
    private void LateUpdate()
    {
        switch (mode)
        {
            case UIMode.LookAt:
                transform.LookAt(Camera.main.transform);
                break;
            case UIMode.LookAtInverted:
                Vector3 dirFromCamera = transform.position - Camera.main.transform.position;
                transform.LookAt(transform.position + dirFromCamera);
                break;
            case UIMode.CameraForward:
                transform.forward = Camera.main.transform.forward;
                break;
            case UIMode.CameraForwardInverted:
                transform.forward = -Camera.main.transform.forward;
                break;
        }
    }
}