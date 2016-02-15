using UnityEngine;
using System.Collections;
using System;

public class EnemyBehaviour : MonoBehaviour {

	public float rotationSpeed = 6.0f;
	public bool smoothRotation = true;
	public float movementSpeed = 2.0f; 
	public float enemySight = 30f;
	public float spaceFromPlayer = 2f;

	private Transform enemy; 
	private Transform player;
	private bool lookAtPlayer = false;
	private Vector3 playerPosition;
    internal Animation enemyAnimation; 
    private int enemyHealth;
    private bool isAlive;

    public event Action<float> Damage;

	// Use this for initialization
	void Start ()
    {
        isAlive = true;
        Damage += GameObject.Find("FPSController").GetComponent<PlayerStats>().AddDamage;
		enemy = transform;
        
        enemyAnimation = GetComponent<Animation>();
        enemyHealth = 100;
        Rigidbody rigidbody = GetComponent<Rigidbody>();
        rigidbody.freezeRotation = true;
        Collider collider = GetComponent<Collider>();
        if (collider != null)
            Debug.Log("collider works");
        else
            Debug.Log("is shit");
	}

	// Update is called once per frame
	void Update ()
    {
        player = GameObject.Find("FPSController").transform;

		playerPosition = new Vector3(player.position.x, enemy.position.y, player.position.z);

		float dist = Vector3.Distance (enemy.position, player.position);

		lookAtPlayer = false; 

		if(dist <= enemySight && dist > spaceFromPlayer)
        {
			lookAtPlayer = true;
			enemy.position = Vector3.MoveTowards(enemy.position, playerPosition, movementSpeed * Time.deltaTime);
            if(isAlive)
                enemyAnimation.Play("walk");

		}
        else if(dist <= spaceFromPlayer)
        { 
			lookAtPlayer = true;
            if(isAlive)
                enemyAnimation.Play("attack");
            Damage(0.5f);
        }

		LookAtPlayer();
	}

	void LookAtPlayer()
    {
		if (smoothRotation && lookAtPlayer)
        {

			
			Quaternion rotation = Quaternion.LookRotation(playerPosition- enemy.position);
			enemy.rotation = Quaternion.Slerp(enemy.rotation, rotation, Time.deltaTime * rotationSpeed);
		}
        else if(!smoothRotation && lookAtPlayer) 
			transform.LookAt(playerPosition);
	}

    public void AddDamage(int damage)
    {
        enemyHealth -= damage;
        if (enemyHealth == 0)
            EnemyDead();
    }

    void EnemyDead()
    {
        isAlive = false;
        System.Random random = new System.Random();
        int value = random.Next(3);
        switch(value)
        {
            case 0:
                enemyAnimation.Play("back_fall");
                break;
            case 1:
                enemyAnimation.Play("left_fall");
                break;
            case 2:
                enemyAnimation.Play("right_fall");
                break;
        }
        Destroy(gameObject, 2);
    }


}
