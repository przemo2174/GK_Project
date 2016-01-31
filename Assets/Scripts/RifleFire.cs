using UnityEngine;
using System.Collections;

public class RifleFire : MonoBehaviour
{
	// Use this for initialization
	void Start ()
    {
        
	}
	
	// Update is called once per frame
	void Update ()
    {
	    if(Input.GetKey(KeyCode.Mouse0))
        {
            AudioSource rifleSound = GetComponent<AudioSource>();
            Animation rifleAnimation = GetComponent<Animation>();
            rifleSound.Play();
            rifleAnimation.Play("RifleShoot");
        }
	}
}
