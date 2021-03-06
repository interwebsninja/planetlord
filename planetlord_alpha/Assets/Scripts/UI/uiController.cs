﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class uiController : MonoBehaviour 
{
	public float miniMapRelDistance;

	public string sunTag;
	public string playerTag;
	public string planetTag;
	public string warpTag;

	public bool planetButtonActive;
	public bool planetMenuActive;
	public bool isInteractingWithPlanet;
	public GameObject planetToInteract;

	public GameObject planetImage;
	public GameObject warpImage;
	public GameObject sunImage;
	public GameObject miniMapContainer;
	public string gameController;
	public GameObject planetInteractionButton;
	public GameObject planetInteractionMenu;
	public List<GameObject> solarSystemObjects;
	public GameObject warpGate;

	void Start()
	{
		solarSystemObjects = GameObject.FindGameObjectWithTag(gameController).GetComponent<gameController>().solarSystemObjects;
		warpGate = GameObject.FindGameObjectWithTag(gameController).GetComponent<gameController>().warpGateContainer;
		miniMapGeneration();
		isInteractingWithPlanet = false;
	}

	void Update()
	{
		planetInteractionController();
		playerListener();
	}

	void playerListener()
	{
		miniMapContainer.SetActive(GameObject.FindGameObjectWithTag(playerTag).GetComponent<playerController>().toggleMap);
	}

	void miniMapGeneration()
	{
		for (int i = 0; i <= solarSystemObjects.Count - 1; i++)
		{
			generateMiniMapObjects(solarSystemObjects[i]);
		}
		generateMiniMapObjects(warpGate);
	}

	void generateMiniMapObjects(GameObject mapObject)
	{
		if (mapObject.tag == sunTag)
		{
			GameObject miniMapObject;
			miniMapObject = Instantiate(sunImage, Vector3.zero, Quaternion.identity) as GameObject; 
			miniMapObject.transform.parent = miniMapContainer.transform;
			miniMapObject.transform.localPosition = new Vector3 (mapObject.transform.position.x/miniMapRelDistance, mapObject.transform.position.z/miniMapRelDistance, 0);
			miniMapObject.transform.localRotation = new Quaternion(0,0,0,1);
		}
		else if (mapObject.tag == planetTag) 
		{
			GameObject miniMapObject;
			miniMapObject = Instantiate(planetImage, Vector3.zero, Quaternion.identity) as GameObject; 
			miniMapObject.transform.parent = miniMapContainer.transform;
			miniMapObject.transform.localPosition = new Vector3 (mapObject.transform.position.x/miniMapRelDistance, mapObject.transform.position.z/miniMapRelDistance, 0);
			miniMapObject.transform.localRotation = new Quaternion(0,0,0,1);
		}
		else if(mapObject.tag == warpTag)
		{
			GameObject miniMapObject;
			miniMapObject = Instantiate(warpImage, Vector3.zero, Quaternion.identity) as GameObject; 
			miniMapObject.transform.parent = miniMapContainer.transform;
			miniMapObject.transform.localPosition = new Vector3 (mapObject.transform.position.x/miniMapRelDistance, mapObject.transform.position.z/miniMapRelDistance, 0);
			miniMapObject.transform.localRotation = new Quaternion(0,0,0,1);
		}
	}

	void planetInteractionController()
	{
		planetInteractionButton.SetActive(planetButtonActive);
	}

	public void createPlanetMenu()
	{
		GameObject.FindGameObjectWithTag(gameController).GetComponent<gameController>().pauseGame(true);
		GameObject.FindGameObjectWithTag(gameController).GetComponent<gameController>().toggleCursor(false);
		planetButtonActive = false;

		GameObject menu;
		menu = Instantiate(planetInteractionMenu) as GameObject;
		menu.GetComponent<planetInteractionMenuController>().planetInventory = planetToInteract.GetComponent<planetProperties>().planetInventory;
		menu.GetComponent<planetInteractionMenuController>().planetToInteract = planetToInteract;
	}	
}
