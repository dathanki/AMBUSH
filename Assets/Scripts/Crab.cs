using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crab : MonoBehaviour {

	public int health; 
	public GameObject particleEffect;
	SpriteRenderer spriteRenderer;
	int Direction; 
	float timer = 1.5f;
	public float speed;
	public Sprite facingUp;
	public Sprite facingDown;
	public Sprite facingRight;
	public Sprite facingLeft;
	// public AudioSource death;


    // Start is called before the first frame update
    void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        Direction = Random.Range(0, 3);
        // death = GetComponent<AudioSource>();
        //spriteRenderer.sprite = facingUp;

    }

    // Update is called once per frame
    void Update() {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
        	timer = 1f;
        	Direction = Random.Range (0, 3);
        }
        Movement();
    }

    void Movement()
  	{
  		if (Direction == 0)
  		{
  			transform.Translate(0, -speed * Time.deltaTime, 0);
  			spriteRenderer.sprite = facingDown;
  		}
  		else if (Direction == 1)
  		{
  			transform.Translate(-speed * Time.deltaTime, 0, 0);
  			spriteRenderer.sprite = facingLeft;
  		}
  		else if (Direction == 2)
  		{
  			transform.Translate(speed * Time.deltaTime, 0, 0);
  			spriteRenderer.sprite = facingRight;
  		} 
  		else if (Direction == 3)
  		{
  			transform.Translate(0, speed * Time.deltaTime, 0);
  			spriteRenderer.sprite = facingUp;
  		}
    }

    void OnTriggerEnter2D(Collider2D col)
    {
    	if (col.gameObject.tag == "Sword")
    	{
    		health--;
    		if (health <= 0)
    		{
    			Instantiate(particleEffect, transform.position, transform.rotation);
    			Destroy(gameObject);
    		}
    		col.GetComponent<Sword>().CreateParticle();
    		GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().canAttack = true;
   			//add death sound
			// death.Play();
			Destroy(col.gameObject);
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
    			Instantiate(particleEffect, transform.position, transform.rotation);
    			Destroy(gameObject);	
    		}
    	} 

    	else if (col.gameObject.tag == "Wall")
    	{
    		Direction--;
    		if (Direction < 0)
    			Direction = 3;
    	}
    }
}
