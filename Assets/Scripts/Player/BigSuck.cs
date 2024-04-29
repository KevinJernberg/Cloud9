using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BigSuck : MonoBehaviour
{
    ///Det ska finnas en modell som man bär på, det ska gå att switcha mellan smallsuck och bigsuck
    /// Smallsuck och bigsuck ska ha samma knappar för input
    /// Den ska suga upp moln som är inom en viss radie, när ett moln sugs upp så ska creatures spawnas.

    public void OnSuck(InputAction.CallbackContext context)
    {
        if(gameObject.activeSelf) Debug.Log("BigSuck!");
    }

    private void Suck()
    {
        
    }
}
