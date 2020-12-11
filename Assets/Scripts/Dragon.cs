using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragon : MonoBehaviour {

	Animator anim;
	public float speed;
	int Direction;
	float timer = .7f;
	public int health;
	public GameObject deathParticle;
	bool canAttack;
	float attackTimer = 2f;
	public GameObject projectile;
	public float thrustPower;
	// public AudioSource death;
	// public AudioSource bullet;


    // Start is called before the first frame update
    void Start()
    {
    	anim = GetComponent<Animator>();
    	Direction = Random.Range(0, 3);
        canAttack = false;
        // death = GetComponent<AudioSource>();
        // bullet = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
        	timer = .7f;
        	Direction = Random.Range(0, 3);
        }
        Movement();
        attackTimer -= Time.deltaTime;

        if(attackTimer <= 0)
        {
        	attackTimer = 2f;
        	canAttack = true;
        }    
        Attack();
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
			// bullet.Play();
    	}
    	else if (Direction == 1)
    	{
    		GameObject newProjectile = Instantiate(projectile, transform.position, transform.rotation);
    		newProjectile.GetComponent<Rigidbody2D>().AddForce(Vector2.right * -thrustPower);
			// bullet.Play();
    	}
    	else if (Direction == 2)
    	{
    		GameObject newProjectile = Instantiate(projectile, transform.position, transform.rotation);
    		newProjectile.GetComponent<Rigidbody2D>().AddForce(Vector2.up * -thrustPower);
			// bullet.Play();
    	}
    	else if (Direction == 3)
    	{
    		GameObject newProjectile = Instantiate(projectile, transform.position, transform.rotation);
    		newProjectile.GetComponent<Rigidbody2D>().AddForce(Vector2.right * thrustPower);
			// bullet.Play();
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
    		}
    	}
    }

    void OnCollisionEnter2D(Collision2D col)
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
    		Direction = Random.Range (0, 3);
    	}
    }
}
