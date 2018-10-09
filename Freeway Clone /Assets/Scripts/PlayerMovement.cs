using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
	public AudioClip impact;
	public AudioClip point;
	public AudioClip win;
	public Vector3 playerStart;
	public Vector3 knockbackValue;
	public float speed = 1;
	public float immunityTimerVal = 60;
	private float immunityTimer = 0;
	
	public GameObject p1WinScreen;
	public GameObject p2WinScreen;
	public GameObject winOverlay;
	public GameObject otherPlayer;
	public Sprite opaqueSprite;
	public Sprite transparentSprite;
	
	//public GameObject p1;
	//public GameObject p2;

	public Text scoreText;
	
	//private TextMeshPro p1ScoreText;
	//private TextMeshPro p2ScoreText;
	private AudioSource audioSource;
	private int p1ScoreCount;
	private int p2ScoreCount;
	private bool immTimer = false;
	private bool canMove = true;
	private bool pauseTime = false;
	private CircleCollider2D collider;
	private SpriteRenderer spriteRenderer;
	
	void Start ()
	{
		Time.timeScale = 1;
		Reset();
		audioSource = GetComponent<AudioSource>();
		p1WinScreen.SetActive(false);
		p2WinScreen.SetActive(false);
		winOverlay.SetActive(false);
		collider = GetComponent<CircleCollider2D>();
		spriteRenderer = GetComponent<SpriteRenderer>();
		spriteRenderer.sprite = opaqueSprite;
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
		if (immTimer == true)
		{
			immunityTimer--;
		}
		if (immunityTimer <= 0)
		{
			DisableImmunity();
		}
		//Debug.Log("immunity timer: " + immunityTimer);
		Debug.Log("sprite timer: " + immunityTimer);
		if (p1ScoreCount >= 5 || p2ScoreCount >= 5)
		{
			canMove = false;
		}
	}
	
	void Update ()
	{
		if (Input.GetKey(KeyCode.R))
		{
			RestartLevel();
		}
		if (gameObject.tag == "Player1")
		{
			if (canMove)
			{
				if (Input.GetKey(KeyCode.W))
				{
					transform.position = transform.position + Vector3.up * speed;
				}

				if (Input.GetKey(KeyCode.S))
				{
					transform.position = transform.position + Vector3.down * speed;
				}
			}
		}
		else if (gameObject.tag == "Player2")
		{
			if (canMove)
			{
				if (Input.GetKey(KeyCode.UpArrow))
				{
					transform.position = transform.position + Vector3.up * speed;
				}

				if (Input.GetKey(KeyCode.DownArrow))
				{
					transform.position = transform.position + Vector3.down * speed;
				}
			}
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "Car")
		{
			Debug.Log("hit car");
			Knockback();
			//Reset();
		}
		else if (other.gameObject.tag == "Goal")
		{
			//Debug.Log(gameObject.tag + " reached Goal");
			GivePoint();
			audioSource.PlayOneShot(point, 0.5f);
			Reset();
		}
	}

	void Knockback()
	{
		audioSource.PlayOneShot(impact, 0.3f);
		transform.position = transform.position - knockbackValue;
		EnableImmunity();
	}
	
	void Reset()
	{
		transform.position = playerStart;
	}

	void EnableImmunity()
	{
		collider.enabled = false;
		immTimer = true;
		canMove = false;
		//ToggleSprite();
		spriteRenderer.sprite = transparentSprite;
	}

	void DisableImmunity()
	{
		canMove = true;
		collider.enabled = true;
		immunityTimer = immunityTimerVal;
		immTimer = false;
		//ToggleSprite();
		spriteRenderer.sprite = opaqueSprite;
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
		canMove = false;
		pauseTime = true;
		audioSource.PlayOneShot(win, 0.5f);
		p1WinScreen.SetActive(true);
		Reset();
		otherPlayer.SetActive(false);
	}

	void p2Wins()
	{
		canMove = false;
		pauseTime = true;
		audioSource.PlayOneShot(win, 0.5f);
		p2WinScreen.SetActive(true);
		Reset();
		otherPlayer.SetActive(false);
	}

	void RestartLevel()
	{
		Application.LoadLevel(Application.loadedLevel);
	}
}
