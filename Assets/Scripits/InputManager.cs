using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;


public class InputManager : MonoBehaviour
{
    

    private PlayerController playerController;

    [HideInInspector] public float hor;
    [HideInInspector] public float ver;
    [HideInInspector] public float rotationX, rotationY;

    private bool pause;
    void Start()
    {
       
        playerController = GetComponent<PlayerController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (pause)
        {
            hor =0;
            ver = 0;
            rotationX = 0;
            rotationY = 0;
        }
        else
        {
            hor = Input.GetAxis("Horizontal");
            ver = Input.GetAxis("Vertical");
           rotationX = CrossPlatformInputManager.GetAxis("Mouse X");
           rotationY = CrossPlatformInputManager.GetAxis("Mouse Y");

            if (Input.GetButtonUp("Jump"))
                playerController.Jump();
            if (Input.GetKeyDown(KeyCode.E))
            {
                playerController.switchWeapons((playerController.weaponIndicators < 3) ? playerController.weaponIndicators + 1 : 0) ;
            }
        }
    }
    private void isPaused()
    {
        if (pause)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            pause = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            pause = true;
        }
    }
}
