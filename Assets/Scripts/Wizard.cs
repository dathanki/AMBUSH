using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Wizard : MonoBehaviour {

	Animator anim;
	public float speed;
	public int Direction;
	float directionTimer = .7f;
	public int health;
	public GameObject deathParticle;
	bool canAttack;
	float attackTimer = 2f;
	public GameObject projectile;
	public float thrustPower;
	float changeTimer = .2f;
    float specialTimer = .5f;
	bool shouldChange;
	// public AudioSource death;
 

    // Start is called before the first frame update
    void Start()
    {
    	anim = GetComponent<Animator>();
        canAttack = false;
        shouldChange = false;
        // death = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {  
        specialTimer -= Time.deltaTime;
        if (specialTimer <= 0)
        {
            SpecialAttack();
            SpecialAttack();
            specialTimer = .5f;
        }

        directionTimer -= Time.deltaTime;
        if (directionTimer <= 0)
        {
        	directionTimer = 1.2f;
            switch(Direction) {
                case 1: Direction = 0;
                    break;
                case 2: Direction = 1;
                    break;
                case 3: Direction = 2;
                    break;
                case 0: Direction = 3;
                    break;
                default: Direction = 1;
                    break;
        	}
		}
        Movement();
        attackTimer -= Time.deltaTime;

        if(attackTimer <= 0)
        {
        	attackTimer = 2f;
        	canAttack = true;
        }    
        Attack();
        if (shouldChange)
        {
        	changeTimer -= Time.deltaTime;
        	if (changeTimer <= 0)
        	{
        		shouldChange = false;
        		changeTimer = .2f;
        	}
        }
    }

    void Attack()
    {
    	if(!canAttack)
    		return;
    	canAttack = false;
    	if (Direction == 0)
    	{
    		GameObject newProjectile = Instantiate(projectile, transform.position, transform.rotation);
    		newProjectile.GetComponent<Rigidbody2D>().AddForce(Vector2.up * thrustPower);
    	}
    	else if (Direction == 1)
    	{
    		GameObject newProjectile = Instantiate(projectile, transform.position, transform.rotation);
    		newProjectile.GetComponent<Rigidbody2D>().AddForce(Vector2.right * -thrustPower);
    	}
    	else if (Direction == 2)
    	{
    		GameObject newProjectile = Instantiate(projectile, transform.position, transform.rotation);
    		newProjectile.GetComponent<Rigidbody2D>().AddForce(Vector2.up * -thrustPower);
    	}
    	else if (Direction == 3)
    	{
    		GameObject newProjectile = Instantiate(projectile, transform.position, transform.rotation);
    		newProjectile.GetComponent<Rigidbody2D>().AddForce(Vector2.right * thrustPower);
    	}
    }

    void Movement()
    {
    	if (Direction == 0)
    	{ transform.Translate(0, speed * Time.deltaTime, 0); anim.SetInteger("Direction", Direction); }
    	else if (Direction == 1)
    	{ transform.Translate(-speed * Time.deltaTime, 0, 0); anim.SetInteger("Direction", Direction); }
    	else if (Direction == 2)
    	{ transform.Translate(0, -speed * Time.deltaTime, 0); anim.SetInteger("Direction", Direction); }
    	else if (Direction == 3)
    	{ transform.Translate(speed * Time.deltaTime, 0, 0); anim.SetInteger("Direction", Direction); }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
    	if (col.gameObject.tag == "Sword")
    	{
    		health--;
    		col.gameObject.GetComponent<Sword>().CreateParticle();
    		GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().canAttack = true;
    		GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().canMove = true;
    		Destroy(col.gameObject);
    		if(health<=0)
    		{
    			Instantiate(deathParticle, transform.position, transform.rotation);
    			Destroy(gameObject);
		        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    		}
    	}
    }

    void OnCollisionEnter2D(Collision2D col) //changed from Collision2D
    {
    	if (col.gameObject.tag == "Player")
    	{
    		health--;
    		if (!col.gameObject.GetComponent<Player>().iniFrames)
    		{

    			col.gameObject.GetComponent<Player>().currentHealth--;
    			col.gameObject.GetComponent<Player>().iniFrames = true;
    		}

    		if (health <= 0)
    		{
    			Instantiate(deathParticle, transform.position, transform.rotation);
    			//add death sound
				// death.Play();
				Destroy(gameObject);
    		}
    	} 

    	if (col.gameObject.tag == "Wall")
    	{
    		if (shouldChange)
    			return;

    		if (Direction == 0)
    			Direction = 2;
    		else if (Direction == 1)
    			Direction = 3;
    		else if (Direction == 2)
    			Direction = 0;
    		else if (Direction == 3)
    			Direction = 1;
    		shouldChange = true;
    	}
    }

    void SpecialAttack()
    {
      GameObject newProjectile  = Instantiate(projectile, transform.position, transform.rotation);
      int randomDirection = Random.Range(0, 3);
      switch (randomDirection) {
        case 0: newProjectile.GetComponent<Rigidbody2D>().AddForce(Vector2.right * thrustPower);
            break;
        case 1: newProjectile.GetComponent<Rigidbody2D>().AddForce(Vector2.up * thrustPower);
            break;
        case 2: newProjectile.GetComponent<Rigidbody2D>().AddForce(Vector2.right * -thrustPower);
            break;
        case 3: newProjectile.GetComponent<Rigidbody2D>().AddForce(Vector2.up * -thrustPower);
            break;
      }
    }
  } 