using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootEnemy : MonoBehaviour
{
	public GameObject deathEffect;
	public GameObject bullet;
	public float bulletSpeed = 2f;
	public float chargeTime = 2f;

	private float timer;
	// Use this for initialization
	void Start ()
	{
		timer = 0f;
	}
	
	// Update is called once per frame
	void Update ()
	{
		timer += Time.deltaTime;

		if (timer > chargeTime)
		{
			SpawnBullet();
			GetComponent<SpriteRenderer>().color = Color.blue;
			timer = 0;
		}
		else
		{
			GetComponent<SpriteRenderer>().color = new Color(0, timer/chargeTime, 1f);
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "Player")
		{
			GameObject g = Instantiate(deathEffect, transform.position, transform.rotation);
			ParticleSystem.MainModule m = g.GetComponent<ParticleSystem>().main;
			m.startColor = GetComponent<SpriteRenderer>().color;
			GameManager.GetInstance().ScoreUp(3);
			Destroy(gameObject);
		}
	}

	private void SpawnBullet()
	{
		GameObject g = Instantiate(bullet, transform.position, transform.rotation);
		g.GetComponent<SpriteRenderer>().color = Color.blue;
		if (GameManager.GetInstance().player == null)
			g.GetComponent<Rigidbody2D>().velocity = Vector2.up * bulletSpeed;
		else
		{
			Vector3 dir = GameManager.GetInstance().player.transform.position - transform.position;
			g.GetComponent<Rigidbody2D>().velocity = dir.normalized * bulletSpeed;
		}
	}
}
