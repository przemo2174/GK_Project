﻿using UnityEngine;
using System.Collections;

public abstract class Weapon : MonoBehaviour
{
    public Texture2D crosshairTexture;
    public GameObject bulletHole;

    protected AudioSource gunSound;
    protected Animation shootAnimation;
    protected Vector3 forward;
    protected RaycastHit hit;
    protected float range;
    protected UnityStandardAssets.Characters.FirstPerson.FirstPersonController fpsController;
    protected float zoomFieldOfView;
    protected float defaultFieldOfView;

    private Rect crosshairPosition;
  
    // Use this for initialization
    protected virtual void Start()
    {
        fpsController = GetComponentInParent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>();
        defaultFieldOfView = fpsController.Camera.fieldOfView;
        crosshairPosition = new Rect((Screen.width - crosshairTexture.width) / 2, (Screen.height - crosshairTexture.height) / 2, crosshairTexture.width, crosshairTexture.height);
    }

    protected void OnGUI()
    {
        GUI.DrawTexture(crosshairPosition, crosshairTexture);
    }

    // Update is called once per frame
    protected virtual void Update ()
    {
        //forward = transform.TransformDirection(Vector3.forward);
        forward = fpsController.Camera.ScreenToWorldPoint(new Vector3(crosshairPosition.x, crosshairPosition.y, fpsController.Camera.nearClipPlane));
    }

    protected void Shoot(out RaycastHit hit)
    {
        if (Physics.Raycast(fpsController.Camera.transform.position, fpsController.Camera.transform.forward, out hit))
        {
            if (hit.transform.tag == "Enemy" && hit.distance < range)
                Debug.Log("Enemy hit");
            else if (hit.distance < range)
            {
                Debug.Log("Hit Wall: " + hit.point);
                if (hit.point.y < 8.5f)
                {
                    GameObject go;
                    go = Instantiate(bulletHole, hit.point, Quaternion.FromToRotation(Vector3.up, hit.normal)) as GameObject;
                    Destroy(go, 10);
                }
            }
            else
                Debug.Log("Missed");
        }
    }

}
