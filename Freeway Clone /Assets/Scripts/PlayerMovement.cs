using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
	public Vector3 player1Start;
	public Vector3 player2Start;
	public float speed = 1;
	public GameObject p1WinScreen;
	public GameObject p2WinScreen;
	public GameObject winOverlay;
	//public GameObject p1;
	//public GameObject p2;

	public Text scoreText;
	
	//private TextMeshPro p1ScoreText;
	//private TextMeshPro p2ScoreText;
	private int p1ScoreCount;
	private int p2ScoreCount;
	
	void Start ()
	{
		Reset();
		p1WinScreen.SetActive(false);
		p2WinScreen.SetActive(false);
		winOverlay.SetActive(false);
		//p1ScoreText = p1.GetComponent<TextMeshPro>();
		//p2ScoreText = p2.GetComponent<TextMeshPro>();
		//p1ScoreText.text = "0";
		//p2ScoreText.text = "0";
	}

	void FixedUpdate()
	{
		if (gameObject.tag == "Player1")
		{
			scoreText.text = p1ScoreCount.ToString();
		}

		if (gameObject.tag == "Player2")
		{
			scoreText.text = p2ScoreCount.ToString();
		}
		Debug.Log("Player 1: " + p1ScoreCount);
		Debug.Log("Player 2: " + p2ScoreCount);
	}
	
	void Update ()
	{
		if (Input.GetKey(KeyCode.R))
		{
			RestartLevel();
		}
		if (gameObject.tag == "Player1")
		{
			if (Input.GetKey(KeyCode.W))
			{
				transform.position = transform.position + Vector3.up*speed;
			}
			if (Input.GetKey(KeyCode.S))
			{
				transform.position = transform.position + Vector3.down*speed;
			}
		}
		else if (gameObject.tag == "Player2")
		{
			if (Input.GetKey(KeyCode.UpArrow))
			{
				transform.position = transform.position + Vector3.up*speed;
			}
			if (Input.GetKey(KeyCode.DownArrow))
			{
				transform.position = transform.position + Vector3.down*speed;
			}
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "Car")
		{
			//Debug.Log("hit car");
			Reset();
		}
		else if (other.gameObject.tag == "Goal")
		{
			//Debug.Log(gameObject.tag + " reached Goal");
			GivePoint();
			Reset();
		}
	}

	void Reset()
	{
		if (gameObject.tag == "Player1")
		{
			transform.position = player1Start;
		}
		if (gameObject.tag == "Player2")
		{
			transform.position = player2Start;
		}
	}

	void GivePoint()
	{
		if (gameObject.tag == "Player1")
		{
			p1ScoreCount = p1ScoreCount + 1;
			if (p1ScoreCount >= 5)
			{
				p1Wins();
			}
		}
		if (gameObject.tag == "Player2")
		{
			p2ScoreCount = p2ScoreCount + 1;
			if (p2ScoreCount >= 5)
			{
				winOverlay.SetActive(true);
				p2Wins();
			}
		}
	}

	void p1Wins()
	{
		p1WinScreen.SetActive(true);
		Reset();
	}

	void p2Wins()
	{
		p2WinScreen.SetActive(true);
		Reset();
	}

	void RestartLevel()
	{
		Application.LoadLevel(Application.loadedLevel);
	}
}
