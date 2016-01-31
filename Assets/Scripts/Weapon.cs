using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {

    public Texture2D crosshairTexture;
    private Rect crosshairPosition;

    protected AudioSource gunSound;
    protected Animation shootAnimation;
    protected Vector3 forward;
    protected RaycastHit hit;
    // Use this for initialization
    protected virtual void Start ()
    {
        
    }

    protected void OnGUI()
    {
        
    }

    // Update is called once per frame
    protected virtual void Update ()
    {
        
    }
}
