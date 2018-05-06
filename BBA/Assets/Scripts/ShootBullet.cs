using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
[RequireComponent(typeof(Rigidbody2D))]

public class ShootBullet : MonoBehaviour
{
	public bool canTouch;
	public bool touchStart;
	public Vector3 startPoint;
	public float speed = 5f;
	//public ParticleSystem particle;

	//[SerializeField] private int shootChance;

	//public int GetshootChance()
	//{
	//	return shootChance;
	//}

	// Use this for initialization
	void Start()
	{
		canTouch = true;
		touchStart = false;
		//shootChance = maxShootChance;
		GetComponent<LineRenderer>().SetVertexCount(2);
		GetComponent<LineRenderer>().SetWidth(0.5f, 0f);
		GetComponent<LineRenderer>().SetColors(Color.red, Color.red);
		GetComponent<LineRenderer>().enabled = false;
	}

	// Update is called once per frame
	void Update()
	{
		if (Input.touchCount > 0)
		{
			Touch t = Input.GetTouch(0);
			Vector3 pos = Camera.main.ScreenToWorldPoint(t.position);
			ReadyShoot(pos, t.phase);
		}
		if (Input.GetMouseButtonDown(0))
		{
			Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			ReadyShoot(pos, TouchPhase.Began);
		}
		else if (Input.GetMouseButton(0))
		{
			Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			ReadyShoot(pos, TouchPhase.Moved);
		}
		else if (Input.GetMouseButtonUp(0))
		{
			Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			ReadyShoot(pos, TouchPhase.Ended);
		}
	}

	void ReadyShoot(Vector3 pos, TouchPhase state)
	{
		if (state == TouchPhase.Began && !touchStart /*&& shootChance > 0*/)
		{
			//if (shootChance != maxShootChance)
			//	Time.timeScale = 0f;
			//--shootChance;
			Time.timeScale = 0.3f;
			startPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			touchStart = true;
			GetComponent<LineRenderer>().SetPosition(0, transform.position);
			GetComponent<LineRenderer>().SetPosition(1, transform.position);
			GetComponent<LineRenderer>().enabled = true;

		}
		else if (state == TouchPhase.Moved && touchStart)
		{
			Vector2 dir = (startPoint - pos).normalized;
			GetComponent<LineRenderer>().SetPosition(0, transform.position);
			GetComponent<LineRenderer>().SetPosition(1, transform.position + (Vector3)dir);
			Debug.DrawLine(startPoint, pos);
		}
		else if (state == TouchPhase.Ended && touchStart)
		{
			//Debug.Log(pos);
			Vector2 dir = (startPoint - pos).normalized;
			GetComponent<Rigidbody2D>().velocity = dir * speed;
			touchStart = false;
			GetComponent<LineRenderer>().enabled = false;
			Time.timeScale = 1f;

		}
	}
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.gameObject.tag == "Bullet")
		{
			Destroy(gameObject);
		}
	}
}
