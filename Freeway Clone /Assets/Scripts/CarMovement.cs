using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMovement : MonoBehaviour
{
	public bool moveRight;
	public float speed;
	public Vector3 carStartPos;
	
	void Start () 
	{
		
	}
	
	
	void Update ()
	{
		Move();
	}

	void Move()
	{
		if (moveRight)
		{
			transform.position = transform.position + Vector3.right * speed;
		}
		else if (!moveRight)
		{
			transform.position = transform.position + Vector3.left * speed;
		}
	}
	
	

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "End")
		{
			Destroy(gameObject);
		}
	}
		
}
