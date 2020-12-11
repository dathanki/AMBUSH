using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

	public GameObject particleEffect;
	float timer = 2f;
    // public AudioSource bullet;



    // Start is called before the first frame update
    void Start()
    {
        // bullet = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update() {
    	timer -= Time.deltaTime;
    	if (timer <= 0)
    	{ 
    		CreateParticle();
    		Destroy(gameObject); 
    	}

    }
    public void CreateParticle()
    {
    	Instantiate(particleEffect, transform.position, transform.rotation);
		//add bullet sound
        // bullet.Play();
    }
}
