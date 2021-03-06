﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEditor;

public class PlayerStats : MonoBehaviour
{
    private float maxHealth = 100;
    private float currentHealth = 100;
    private float maxStamina = 100;
    private float currentStamina = 100;

    private float barWidth;
    private float barHeight;

    private CharacterController characterController;
    private UnityStandardAssets.Characters.FirstPerson.FirstPersonController fpsController;
    private Vector3 lastPosition;

    public Texture2D healthTexture;
    public Texture2D staminaTexture;

    void Awake()
    {
        barHeight = Screen.height * 0.02f;
        barWidth = barHeight * 10.0f;

        characterController = GetComponent<CharacterController>();
        fpsController = GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>();
    }

    void OnGUI()
    {
        GUI.DrawTexture(new Rect(Screen.width - barWidth - 10, Screen.height - barHeight * 2 - 20, currentStamina * barWidth / maxStamina, barHeight), staminaTexture);
        GUI.DrawTexture(new Rect(Screen.width - barWidth - 10, Screen.height - barHeight - 10, currentHealth * barWidth / maxHealth, barHeight), healthTexture);
    }

	// Use this for initialization
	void Start ()
    {
	}
	
	// Update is called once per frame
	void Update ()
    {
	    if(Input.GetKeyDown(KeyCode.P))
        {
            AddDamage(20);
        }
	}

    void FixedUpdate()
    {
        if(characterController.isGrounded && Input.GetKey(KeyCode.LeftShift) && lastPosition != transform.position)
        {
            lastPosition = transform.position;
            currentStamina -= 1;
        }
        else if(!Input.GetKeyDown(KeyCode.LeftShift))
            currentStamina += 1;

        currentStamina = Mathf.Clamp(currentStamina, 0, maxStamina);

        if (currentStamina > 0)
            fpsController.CanRun = true;
        else
            fpsController.CanRun = false;

    }

    internal void AddDamage(float damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        if (currentHealth == 0)
            Debug.Break();
    }
}
