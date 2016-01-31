using UnityEngine;
using System.Collections;

public class PlayerStats : MonoBehaviour
{
    private float maxHealth = 100;
    private float currentHealth = 100;
    private float maxStamina = 100;
    private float currentStamina = 100;

    private float barWidth;
    private float barHeight;

    public Texture2D healthTexture;
    public Texture2D staminaTexture;

    void Awake()
    {
        barHeight = Screen.height * 0.02f;
        barWidth = barHeight * 10.0f;
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
	
	}
}
