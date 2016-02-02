using UnityEngine;
using System.Collections;

public class RifleFire : Weapon
{
    
    // Use this for initialization
    protected override void Start ()
    {
        base.Start();
        range = 20.0f;
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

        if (Input.GetKey(KeyCode.Mouse0))
        {
            gunSound.Play();
            shootAnimation.Play("RifleShoot");
            Shoot(out hit);
        }
	}
}
