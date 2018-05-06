using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(ParticleSystem))]
public class ParticleController : MonoBehaviour
{
	void Start ()
	{
		ParticleSystem p = GetComponent<ParticleSystem>();
		Destroy(gameObject, p.duration + p.startLifetime);
	}
}
