using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSpawner : MonoBehaviour
{

	public float spawnDelay;

	public GameObject car;

	public Transform spawnPoint1;
	public Transform spawnPoint2;
	public Transform spawnPoint3;
	
	private float nextTimeToSpawn;

	void Awake()
	{
		nextTimeToSpawn = spawnDelay;
	}
	
	void Start()
	{
		SpawnCar();
		
	}
	
	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "End")
		{
			Debug.Log("destroy dat shit");
		}
	}
	
	void FixedUpdate ()
	{
		
		if (nextTimeToSpawn <= 0)
		{
			nextTimeToSpawn = spawnDelay;
			Debug.Log(nextTimeToSpawn);
			SpawnCar();
			
		}
		else
		{
			nextTimeToSpawn = nextTimeToSpawn - 1;
		}
	}

	void SpawnCar()
	{
		Instantiate(car, spawnPoint1.position, spawnPoint1.rotation);
		Instantiate(car, spawnPoint2.position, spawnPoint2.rotation);
		Instantiate(car, spawnPoint3.position, spawnPoint3.rotation);
	}

	

	
}
