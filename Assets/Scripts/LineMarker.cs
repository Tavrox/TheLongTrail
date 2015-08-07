using UnityEngine;
using System.Collections;

[RequireComponent (typeof(BoxCollider))]
public class LineMarker : MonoBehaviour 
{
	public BoxCollider Colli;

	void Start()
	{
		Colli = GetComponent<BoxCollider>();
		Debug.Log ("start test git");
	}

	void OnTriggerEnter(Collider _coll)
	{

	}
}
