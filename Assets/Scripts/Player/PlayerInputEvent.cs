using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR;

public class PlayerInputEvent : MonoBehaviour
{
    [SerializeField]
    KeyCode jumpKey = KeyCode.Mouse0;
    public UnityEvent OnJumpInput;

    private void Update()
    {
        CheckJumpInput();
    }

    public void CheckJumpInput()
    {
        if (Input.GetKey(jumpKey))
        {
            OnJumpInput?.Invoke();
        }
    }

}
