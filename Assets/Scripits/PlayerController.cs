using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public UIManager ui;

    public float speed = 1f;
    public float jumpSpeed = 200f;
    [Range (0.1f,5f)]
    public float mouseSensitivity = 1f;
    public float gravityForce = -9.81f;

    private Vector3 movement, gravity;

    public GameObject cameraObject;

    public int weaponIndicators;
    public GameObject[] weapons = new GameObject[4];

    private CharacterController characterController;
    private InputManager inputManager;
    

    // Start is called before the first frame update
    void Start()
    {
        ui = GameObject.FindGameObjectWithTag("UI System").GetComponent<UIManager>();
        characterController = GetComponent<CharacterController>();
        inputManager = GetComponent<InputManager>();

        switchWeapons(0);

        ui.setHealth("100");
        ui.setWeaponToDisplay(0);
    }

    // Update is called once per frame
    void Update()
    {
        //Move
        movement = transform.right * inputManager.hor + transform.forward * inputManager.ver;
        characterController.Move(movement * speed * Time.deltaTime);

        if(isGrounded() && gravity.y < 0)
        {
            gravity.y = -2;
        }
        gravity.y += gravityForce * Time.deltaTime;
        characterController.Move(gravity * Time.deltaTime);

        transform.Rotate(0, inputManager.rotationX, 0);
        if(!(cameraObject.transform.eulerAngles.x + (-inputManager.rotationY)> 45 && cameraObject.transform.eulerAngles.x + (-inputManager.rotationY) < 320 ))
        {
            cameraObject.transform.RotateAround(transform.position, cameraObject.transform.right, -inputManager.rotationY);
        }

        //transform.localRotation *= Quaternion.Euler(0f, inputManager.yValue * mouseSensitivity, 0f);

        //if (inputManager.xValue > 0 && cameraObject.transform.localRotation.x > -0.7f)
            //cameraObject.transform.localRotation *= Quaternion.Euler(-inputManager.xValue * mouseSensitivity, 0f, 0f);

       // if (inputManager.xValue < 0 && cameraObject.transform.localRotation.x < 0.7f)
            //cameraObject.transform.localRotation *= Quaternion.Euler(-inputManager.xValue * mouseSensitivity, 0f, 0f);
    }


    private bool isGrounded()
    {
        RaycastHit raycastHit;
        if (Physics.SphereCast(transform.position, characterController.radius * (1.0f - 0), Vector3.down, out raycastHit, 
            ((characterController.height / 2) - characterController.radius) + 0.15f, Physics.AllLayers, QueryTriggerInteraction.Ignore))
        {
            return true;
        }
        else return false;
    }
  


    public void Jump()
    {
       if(isGrounded())
        {
            gravity.y = Mathf.Sqrt(jumpSpeed * -2 * gravityForce);
        }
    }
    public void switchWeapons(int index)
    {
        for (int i = 0; i < weapons.Length; i++)
        {
            weapons[i].SetActive(false);
        }

        weapons[index].SetActive(true);
        ui.setWeaponToDisplay(index);
        weaponIndicators = index;
    }
}
