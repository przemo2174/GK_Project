using UnityEngine;
using System.Collections;

public class M9Fire : MonoBehaviour
{
    private float range = 10.0f;
    public Texture2D crosshairTexture;
    private Rect crosshairPosition;

    private AudioSource gunSound;
    private Animation shootAnimation;
    private Vector3 forward;
    private RaycastHit hit;
    // Use this for initialization
    void Start ()
    {
        crosshairPosition = new Rect((Screen.width - crosshairTexture.width) / 2, (Screen.height - crosshairTexture.height) / 2, crosshairTexture.width, crosshairTexture.height);
        gunSound = GetComponent<AudioSource>();
        shootAnimation = GetComponent<Animation>();
    }
	
    void OnGUI()
    {
        GUI.DrawTexture(crosshairPosition, crosshairTexture);
    }

	// Update is called once per frame
	void Update ()
    {
        forward = transform.TransformDirection(Vector3.forward);
        if (Input.GetButtonDown("Fire1"))
        {
            gunSound.Play();
            shootAnimation.Play("M9Shoot");

            if(Physics.Raycast(transform.position, forward, out hit))
            {
                if (hit.transform.tag == "Enemy" && hit.distance < range)
                    Debug.Log("Enemy hit");
                else if (hit.distance < range)
                    Debug.Log("No hit");
            }
        }
	}
}
