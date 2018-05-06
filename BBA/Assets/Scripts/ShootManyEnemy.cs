using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootManyEnemy : MonoBehaviour
{
	public GameObject deathEffect;
	public GameObject bullet;

	public float bulletSpeed = 2f;
	public float chargeTime = 2f;
	public float shootTime = 2f;
	public float shootSpeed = 0.1f;

	private float timer;
	private int count;
	// Use this for initialization
	void Start ()
	{
		timer = 0;
		count = 0;
	}
	
	// Update is called once per frame
	void Update ()
	{
		timer += Time.deltaTime;

		if (timer < chargeTime)
		{
			GetComponent<SpriteRenderer>().color = new Color(timer / chargeTime, 1f - (timer / chargeTime), timer / chargeTime);
		}
		else if ( timer < chargeTime + shootTime)
		{
			float ratio = (timer - chargeTime) / shootTime;
			GetComponent<SpriteRenderer>().color = new Color(1f - ratio, ratio, 1 - ratio);
			if (timer > chargeTime + count * shootSpeed)
			{
				++count;
				SpawnBullet();
			}
		}
		else
		{
			GetComponent<SpriteRenderer>().color = Color.green;
			timer = 0;
			count = 0;
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "Player")
		{
			GameObject g = Instantiate(deathEffect, transform.position, transform.rotation);
			ParticleSystem.MainModule m = g.GetComponent<ParticleSystem>().main;
			m.startColor = GetComponent<SpriteRenderer>().color;
			GameManager.GetInstance().ScoreUp(10);
			Destroy(gameObject);
		}
	}

	private void SpawnBullet()
	{
		GameObject g = Instantiate(bullet, transform.position, transform.rotation);
		g.GetComponent<SpriteRenderer>().color = Color.green;
		if (GameManager.GetInstance().player == null)
			g.GetComponent<Rigidbody2D>().velocity = Vector2.up * bulletSpeed;
		else
		{
			Vector3 dir = GameManager.GetInstance().player.transform.position - transform.position;
			g.GetComponent<Rigidbody2D>().velocity = dir.normalized * bulletSpeed;
		}
	}
}
