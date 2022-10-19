using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour
{
    [HideInInspector]
    public float Vertical;
    [HideInInspector]
    public float Horizontal;
    [HideInInspector]
    public float MouseX;
    [HideInInspector]
    public float MouseY;
    [HideInInspector]
    public bool Jump;
    [HideInInspector]
    public bool Fire;


    void Start()
    {
    }

    void Update()
    {
        Horizontal =  Input.GetAxis("Horizontal");
        Vertical = Input.GetAxis("Vertical");
        Jump = Input.GetButtonDown("Jump");
        Fire = Input.GetButton("Fire2");
        MouseX = Input.GetAxis("Mouse X");
        MouseY = Input.GetAxis("Mouse Y");
    }
}
