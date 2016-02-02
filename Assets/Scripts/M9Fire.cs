using UnityEngine;
using System.Collections;

public class M9Fire : Weapon
{

    //public int maxAmmo = 100;
    //public int clipSize = 10;
    //private int currentAmmo = 40;
    //private int currentClip;

    // Use this for initialization
    protected override void Start()
    {
        base.Start();
        range = 10.0f;
        zoomFieldOfView = 40.0f;

        gunSound = GetComponent<AudioSource>();
        shootAnimation = GetComponent<Animation>();       
    }
	
   
	// Update is called once per frame
	protected override void Update ()
    {
        base.Update();
        if (Input.GetButton("Fire2"))
        {
            if (fpsController.Camera.fieldOfView > zoomFieldOfView)
                fpsController.Camera.fieldOfView -= 2;
        }
        else
        {
            if (fpsController.Camera.fieldOfView < defaultFieldOfView)
                fpsController.Camera.fieldOfView += 2;
        }
            
            
        if (Input.GetButtonDown("Fire1"))
        {
            gunSound.Play();
            shootAnimation.Play("M9Shoot");
            Shoot(out hit);
        }
	}
}
