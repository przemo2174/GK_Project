using UnityEngine;
using System.Collections;

public class M9Fire : MonoBehaviour
{

	// Use this for initialization
	void Start ()
    {
	    
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            AudioSource gunSound = GetComponent<AudioSource>();
            gunSound.Play();
            GetComponent<Animation>().Play("M9Shoot");
        }
	}
}
