using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public GameObject player;
	public GameObject[] Enemy;
	public float spawnCycle = 2f;
	public Text scoreText;

	static private GameManager instance;
	private float timer;
	[SerializeField] private int score;

	static public GameManager GetInstance()
	{
		//if (instance == null)
		//	instance = GameObject.GetComponent<GameManager>();

		return instance;
	}
	private void Awake()
	{
		if (instance == null)
			instance = this;
	}

	void Start ()
	{
		score = 0;
		timer = 0f;
	}

	void Update ()
	{
		timer += Time.deltaTime;
		if (timer > spawnCycle)
		{
			Instantiate(Enemy[Random.RandomRange(0, Enemy.Length)], new Vector3(Random.RandomRange(-7.5f, 7.5f), Random.RandomRange(-4f, 4f)), Quaternion.identity);
			timer = 0;
		}

		if (player == null)
		{
			Invoke("Restart", 2f);
		}
	}
	void Restart()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}
	public void ScoreUp(int pScore)
	{
		score += pScore;
		scoreText.text = score.ToString();
	}
}
