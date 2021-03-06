﻿using UnityEngine;
using System.Collections;

public class WeaponManager : MonoBehaviour {

    private GameObject[] weapons;

    // Use this for initialization
    void Start ()
    {
        weapons = new GameObject[transform.childCount];
        int i = 0;
        foreach (Transform weaponTransform in transform)
            weapons[i++] = weaponTransform.gameObject;

        weapons[0].gameObject.SetActive(true);
        weapons[1].gameObject.SetActive(false);

        Debug.Log(weapons[0].name);
        Debug.Log(weapons[1].name);

	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            weapons[0].SetActive(true);
            weapons[1].SetActive(false);
        }

        if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            weapons[0].SetActive(false);
            weapons[1].SetActive(true);
        }

        
    }
}
