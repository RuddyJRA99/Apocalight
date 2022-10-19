using Cinemachine;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    InputController input;
    
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        input = GetComponentInParent<InputController>();
        
    }

}
