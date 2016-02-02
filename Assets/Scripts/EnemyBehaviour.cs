using UnityEngine;
using System.Collections;
using System;

public class EnemyBehaviour : MonoBehaviour {

	//Predkosc obrotu przeciwnika.
	public float rotationSpeed = 6.0f;

	//Gładki obrót przeciwnika
	public bool smoothRotation = true;


	//Prędkość poruszania się przeciwnika.
	public float movementSpeed = 2.0f; 
	//Odległość na jaką widzi przeciwnik.
	public float enemySight = 30f;
	//Odstęp w jakim zatrzyma się obiekt wroga od gracza.
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
        //Rigidbody pozwala aby na obiekt oddziaływała fizyka.
        //Wyłaczenie oddziaływanie fizyki na XYZ - 
        // jak obiekt będzie wchodził pod górkę to się przechyli prostopadle do zbocza a fizyka pociągnie go w dół i
        // obiekt się przewróci. POZATYM NIE CHCEMY ABY WRÓG SIĘ TAK DZIWNIE OBRACAŁ ;).
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
        //Pobranie obiektu gracza.
        player = GameObject.Find("FPSController").transform;

		//Pobranie pozycji gracza.
		playerPosition = new Vector3(player.position.x, enemy.position.y, player.position.z);

		//Pobranie dystansu dzielącego wroga od obiektu gracza.
		float dist = Vector3.Distance (enemy.position, player.position);

		lookAtPlayer = false; //Wróg nie patrz na gracza bo jeszcze nie wiadomo czy jest w zasięgu wzroku.

		//Sprawdzenie czy gracz jest w zasięgu wzroku wroga.
		if(dist <= enemySight && dist > spaceFromPlayer)
        {
			lookAtPlayer = true;//Gracz w zasiegu wzroku wiec na neigo patrzymy
			
			//Teraz wykonujemy ruch wroga.
			//Vector3.MoveTowards - pozwala na zdefiniowanie nowej pozycji gracza oraz wykonanie animacji.
			//Pierwszy parametr obecna pozycja drógi parametr pozycja do jakiej dążymy (czyli pozycja gracza).
			//Trzeci parametr określa z jaką prędkością animacja/ruch ma zostać wykonany.
			enemy.position = Vector3.MoveTowards(enemy.position, playerPosition, movementSpeed * Time.deltaTime);
            if(isAlive)
                enemyAnimation.Play("walk");

		}
        else if(dist <= spaceFromPlayer)
        { //Jeżeli wróg jest tuż przy graczu to niech ciągle na niego patrzy mimo że nie musi się już poruszać.
			lookAtPlayer = true;
            if(isAlive)
                enemyAnimation.Play("attack");
            Damage(0.5f);
        }

		LookAtPlayer();
	}

	//Wróg może nie mieć potrzeby sie pruszać bo jest blisko gracza ale niech się obraca w jego stronę.
	void LookAtPlayer()
    {
		if (smoothRotation && lookAtPlayer)
        {

			//Quaternion.LookRotation - zwraca quaternion na podstawie werktora kierunku/pozycji. 
			//Potrzebujemy go aby obrócić wroga w stronę gracza.
			Quaternion rotation = Quaternion.LookRotation(playerPosition- enemy.position);
			//Obracamy wroga w stronę gracza.
			enemy.rotation = Quaternion.Slerp(enemy.rotation, rotation, Time.deltaTime * rotationSpeed);
		}
        else if(!smoothRotation && lookAtPlayer) //Jeżeli nei chcemy gładkiego obracania się wroga tylko błyskawicznego obrot //Błyskawiczny obrót wroga.
			transform.LookAt(playerPosition);
	}

    public void AddDamage(int damage)
    {
        enemyHealth -= damage;
        if (enemyHealth <= 0)
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
        Destroy(gameObject, 5);
    }


}
