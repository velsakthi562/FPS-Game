using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public UIManager ui;

    public float damage = 10f;
    public float fireRate = 25f;
    public float force = 80f;
    public int magazine = 30;
    public int ammo;
    public int mags = 3;

    public GameObject cameraGameObject;

    public ParticleSystem flash;
    public GameObject bulletEffect;
    public AudioClip fireClip;


    public AudioSource fireSource;

    private float readyToFire;
    private Animator animator;

    private bool isReloading = false;
    public float reloadAnimationTime = 2.5f;
    private float reloadTime = 0;
    private int magazineTmp;

    private InputManager inputs;

    // Start is called before the first frame update
    void Start()
    {
        ui = GameObject.FindGameObjectWithTag("UI System").GetComponent<UIManager>();
        animator = GetComponent<Animator>();
        inputs = gameObject.GetComponent<InputManager>();
        animator.SetInteger("Movement", 0);
        ammo = magazine * mags;
        magazineTmp = magazine;

        ui.setAmmo(magazine + "/" + ammo);
        InstantiateAudio(fireClip);
    }

    private void InstantiateAudio(AudioClip clip)
    {
        fireSource = gameObject.GetComponent<AudioSource>();
        fireSource.clip = clip;
    }
    public void PlaySound()
    {
        if (fireSource.isPlaying)
        {
            fireSource.Stop();
            fireSource.Play();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time >= readyToFire)
        {
            animator.SetInteger("Fire", -1);
            animator.SetInteger("Movement", (inputs.ver == 0 && inputs.hor == 0 )? 0 : 1);
        }

        if (Input.GetButton("Fire1") && Time.time >= readyToFire && !isReloading && magazine > 0)
        {
            readyToFire = Time.time + 1f / fireRate;
            fire();

            animator.SetInteger("Fire", 2);
            animator.SetInteger("Movement", -1);
        }

        if (Input.GetKeyDown(KeyCode.R) && !isReloading&& ammo > 0)
        {
            reloadTime = reloadAnimationTime;
            animator.SetInteger("Reload", 1);
            isReloading = true;
        }
        if (isReloading && reloadTime <= 1)
        {
            reloadTime = 0;
            animator.SetInteger("Reload", -1);
            isReloading = false;
            ammo = ammo - 30 + magazine;
            magazine = magazineTmp;
            if (ammo < 0)
            {
               magazine  += ammo;
               ammo = 0;
                ui.setAmmo(magazine + "/" + ammo);
            }
            ui.setAmmo(magazine + "/" + ammo);
        }
        else
        {
            reloadTime -= Time.deltaTime;
        }
    }

 

    void fire()
    {
        flash.Play();
        magazine--;
        ui.setAmmo(magazine + "/" + ammo);
        RaycastHit hit;

        if (Physics.Raycast(cameraGameObject.transform.position,cameraGameObject.transform.forward, out hit))
        {
            if(hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * force);
            }
            Create create = hit.transform.GetComponent<Create>();

            if(create != null)
            {
                create.TakeDamage(damage);
            }

            Instantiate(bulletEffect, hit.point, Quaternion.LookRotation(hit.normal));
        }
    }
}
