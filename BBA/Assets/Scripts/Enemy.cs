using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	public GameObject deathEffect;

	void Start ()
	{
		
	}
	
	void Update ()
	{
		
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if ( collision.gameObject.tag == "Player" )
		{
			GameObject g = Instantiate(deathEffect, transform.position, transform.rotation);
			ParticleSystem.MainModule m = g.GetComponent<ParticleSystem>().main;
			m.startColor = GetComponent<SpriteRenderer>().color;
			GameManager.GetInstance().ScoreUp(1);
			Destroy(gameObject);
		}
	}
}
