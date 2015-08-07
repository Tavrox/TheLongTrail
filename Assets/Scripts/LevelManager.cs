using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class LevelManager : MonoBehaviour {

	public enum KillerList
	{
		Energy,
		Cheat
	};
	public static GameEventManager.GameState GAMESTATE;

	public CharacterManager Charac;
	public PathMaker Path;
	public Camera Cam;
	public Light secondLight;

	public Color primaryColor; // Ambient lighting
	public Color secondaryColor; // Second lighting >> second light

	void Awake () 
	{
		Path =  GetComponentInChildren<PathMaker>().Setup(this);
		Charac =  GetComponentInChildren<CharacterManager>().Setup(this);
		Cam = Camera.main;
		changeLighting();
	}

	public void changeLighting()
	{
		RenderSettings.ambientLight = primaryColor;
		secondLight.color = secondaryColor;
	}

	void Update () {
		Vector3 padding =  Cam.transform.position - Charac.transform.position;
		Cam.transform.position = Charac.transform.position + padding; 
	}

	void OnDrawGizmos()
	{		
		changeLighting();
	}
}
