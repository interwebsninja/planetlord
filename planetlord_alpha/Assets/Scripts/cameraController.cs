﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
public class cameraController : MonoBehaviour 
{
	public GameObject playerShip;

	public GameObject gameController;
	
	public float defaultCamFOV;
	public float maxCamFOV;
	public float zoomSmooth;

	private int idVal;

	public string[] objectsToTrack;

	private float yVal;

	private float focusedObjDistance;

	private Vector3 playerPos;

	public List<GameObject> currentlyTracking;

	private GameObject objCurrentlyFocused;

	void Awake()
	{
		yVal = transform.position.y;

		currentlyTracking = new List<GameObject>();

		idVal = 0;
	}

	void Update () 
	{
		playerPos = playerShip.transform.position;

		gameController.GetComponent<gameController>().trackableTargets = currentlyTracking;

		followPlayer();
		cameraScalingController();
	}

	void followPlayer()
	{
		transform.position = new Vector3(playerShip.transform.position.x, yVal, playerShip.transform.position.z);
	}

	void cameraScalingController()
	{
		Camera.main.orthographicSize = camFOVScale(Camera.main.orthographicSize);
	}

	void distanceChecker(GameObject checkedObject)
	{
		if (objCurrentlyFocused != null)
		{
			float newDistance = Vector3.Distance(checkedObject.transform.position, playerPos);
			float currentDistance = Vector3.Distance(objCurrentlyFocused.transform.position, playerPos);
			if (newDistance > currentDistance)
			{
				objCurrentlyFocused = checkedObject;
			}
		}
	}
	
	float camFOVScale(float viewDistance)
	{
		float tempDistance = viewDistance;
		
		if(viewDistance >= defaultCamFOV && viewDistance <= maxCamFOV && focusedObjDistance >= defaultCamFOV && focusedObjDistance <= maxCamFOV && currentlyTracking.Count > 0)
		{
			return Mathf.Lerp(tempDistance, focusedObjDistance, (zoomSmooth * Time.deltaTime));
		}
		else if (currentlyTracking.Count == 0)
		{
			return Mathf.Lerp(tempDistance, defaultCamFOV, (zoomSmooth * Time.deltaTime));
		}
		else
		{
			return tempDistance;
		}

		if(viewDistance < defaultCamFOV + 1)
		{
			return defaultCamFOV;
		}
	}

	/// <summary>
	/// Raises the trigger enter event.
	/// </summary>
	/// <param name="other">Other.</param>

	void OnTriggerEnter(Collider other)
	{
		foreach (string tag in objectsToTrack)
		{
			if (other.gameObject.tag == tag)
			{

				if (!currentlyTracking.Contains(other.gameObject))
				{
					if (currentlyTracking.Count == 0)
					{
						other.gameObject.GetComponent<enemyProperties>().id = idVal;
						idVal ++;
						currentlyTracking.Add(other.gameObject);

						objCurrentlyFocused = other.gameObject;
					}
					else
					{
						other.gameObject.GetComponent<enemyProperties>().id = idVal;
						idVal ++;
						currentlyTracking.Add(other.gameObject);
					}
				}
			}
		}
	}
	
	void OnTriggerStay(Collider other)
	{
		foreach (string tag in objectsToTrack)
		{
			if (other.gameObject.tag == tag)
			{
				distanceChecker(other.gameObject);
				if (objCurrentlyFocused != null)
				{
					focusedObjDistance = Vector3.Distance(objCurrentlyFocused.transform.position, playerPos);
				}
			}
		}
	}
	
	void OnTriggerExit(Collider other)
	{
		foreach (string tag in objectsToTrack)
		{
			if (other.gameObject.tag == tag)
			{
				currentlyTracking.Remove(other.gameObject);
			}
		}
	}
}