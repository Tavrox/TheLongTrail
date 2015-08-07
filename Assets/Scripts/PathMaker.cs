using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class PathMaker : MonoBehaviour 
{
	public List<LineMarker> LineList;
	LevelManager Manager;

	public PathMaker Setup (LevelManager _man)
	{
		Manager = _man;
		LineList =  GetComponentsInChildren<LineMarker>().ToList();
		return this;
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
