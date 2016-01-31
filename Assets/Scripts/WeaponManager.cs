using UnityEngine;
using System.Collections;

public class WeaponManager : MonoBehaviour {

    public GameObject m9;
    public GameObject rifle;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            m9.SetActive(true);
            rifle.SetActive(false);
        }

        if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            m9.SetActive(false);
            rifle.SetActive(true);
        }
    }
}
