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

	void OnTriggerExit2D(Collider2D other)
	{
		if (other.gameObject.tag == "LeftBounds")
		{
			if (!moveRight)
			{
				Debug.Log("Exit Left");
				Reset();
			}
		}
		else if (other.gameObject.tag == "RightBounds")
		{
			if (moveRight)
			{
				Debug.Log("Exit Right");
				Reset();
			}
		}
	}


	void Reset()
	{
		transform.position = carStartPos;
	}
		
		
}
