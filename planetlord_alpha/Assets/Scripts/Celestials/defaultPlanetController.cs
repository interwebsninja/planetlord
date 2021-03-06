﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class defaultPlanetController : MonoBehaviour
{
	private List<GameObject> childEnemies;
	private bool isCaptured;
	private string playerShip;
	private string inGameUI;
	private string gameController;

	void Start()
	{
		isCaptured = false;
		childEnemies = GetComponent<planetProperties>().childEnemies;
		gameController = GetComponent<planetProperties>().gameController;
		transform.position += new Vector3 (0, GetComponent<planetProperties>().floatOffset, 0);
		playerShip = GetComponent<planetProperties>().playerShip;
		inGameUI = GetComponent<planetProperties>().inGameUI;
		fillInventory();
	}

	void Update()
	{
		planetStatusController();

		childEnemies = GetComponent<planetProperties>().childEnemies;
		GetComponent<planetProperties>().isCaptured  = isCaptured; 
	}

	void planetStatusController()
	{
		checkPlanetCapture();
		checkActiveEnemies();
	}

	void checkActiveEnemies()
	{
		for(int i = 0; i <= childEnemies.Count - 1; i++)
		{
			if(childEnemies[i] == null)
			{
				GetComponent<planetProperties>().childEnemies.RemoveAt(i);
			}
		}
	}

	void checkPlanetCapture()
	{
		if (childEnemies.Count <= 0)
		{
			isCaptured = true;
		}
	}

	void OnTriggerStay(Collider other)
	{
		if (other.gameObject.tag == playerShip && isCaptured)
		{
			GameObject.FindGameObjectWithTag(inGameUI).GetComponent<uiController>().planetButtonActive = true;
			GameObject.FindGameObjectWithTag(inGameUI).GetComponent<uiController>().planetToInteract = this.gameObject;
		}
	}

	void OnTriggerExit(Collider other)
	{
		if (other.gameObject.tag == playerShip && isCaptured)
		{
			GameObject.FindGameObjectWithTag(inGameUI).GetComponent<uiController>().planetButtonActive = false;
		}
	}

	void fillInventory()
	{
		for(int i = 0; i <= GetComponent<planetProperties>().planetInventory.Count - 1; i++)
		{
			GetComponent<planetProperties>().planetInventory[i] = itemSelection();
		}
	}

	GameObject itemSelection()
	{
		//int selection = Random.Range(0, GameObject.FindGameObjectWithTag(gameController).GetComponent<itemLibrary>().itemsLibrary.Length);
		int subSelection =  Random.Range(0, GameObject.FindGameObjectWithTag(gameController).GetComponent<itemLibrary>().itemsLibrary[0].Length);

		for (int i = 1000; i < 1000; i++)
		{
			//selection = Random.Range(0, GameObject.FindGameObjectWithTag(gameController).GetComponent<itemLibrary>().itemsLibrary.Length);
			subSelection =  Random.Range(0, GameObject.FindGameObjectWithTag(gameController).GetComponent<itemLibrary>().itemsLibrary[0].Length);
			if (GetComponent<planetProperties>().planetInventory.Contains(GameObject.FindGameObjectWithTag(gameController).GetComponent<itemLibrary>().itemsLibrary[0][subSelection]))
			{
				continue;
			}
			else
			{
				i  = 1000;
				return GameObject.FindGameObjectWithTag(gameController).GetComponent<itemLibrary>().itemsLibrary[0][subSelection];
			}
		}

		return GameObject.FindGameObjectWithTag(gameController).GetComponent<itemLibrary>().itemsLibrary[0][subSelection];
	}
}
