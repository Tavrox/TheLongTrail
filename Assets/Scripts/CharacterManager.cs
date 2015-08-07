using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class CharacterManager : MonoBehaviour {

	public enum dirList
	{
		Left,
		Right
	};
	public LevelManager Manager;
	public dirList Direction;
	public Animation Anims;

	public PathMaker Path;
	public LineMarker CurrentMarker;

	public float targetSpeed;
	public float currentSpeed;
	public float maxSpeed;
	public float dirModifier;
	public Vector3 Target;

	public int Energy;
	public int VisuSpeed;
	public bool OnStairs; // See if waypoint was before a stair.

	public CharacterManager Setup(LevelManager _man)
	{
		Anims ["walk"].wrapMode = WrapMode.Loop;
		Anims ["run"].wrapMode = WrapMode.Loop;
		Manager = _man;
		Path = _man.Path;
		CurrentMarker = Path.LineList[1];
		Anims.Play("run");
		return this;
	}

	void Update () {

		lookKeys ();
		applyForces ();
		Debug.Log(CurrentMarker.transform.position);
		transform.position = Vector3.MoveTowards(transform.position, CurrentMarker.transform.position, maxSpeed * Time.deltaTime);
		if (!OnStairs)
		{
			Vector3 dir = Vector3.RotateTowards(transform.position, CurrentMarker.transform.position - transform.position, 30f, 0f);
			transform.rotation = Quaternion.LookRotation(dir);
			Debug.DrawRay(transform.position, dir);
		}
	}

	// Every waypoints gives a whole new direction.
	void OnTriggerEnter(Collider _coll)
	{
		if (_coll.tag == "Stairs")
		{
			OnStairs = true;
		}
		if (CurrentMarker.Colli == _coll)
		{
			seekNextPoint();
		}
	}

	/// <summary>
	/// Will seek the next point on line. Beware TODO : check if point is the last >> stop the guy. 
	/// </summary>
	void seekNextPoint()
	{
		CurrentMarker = Path.LineList[Path.LineList.IndexOf(CurrentMarker)+1];
	}

	/// <summary>
	/// Check if keys are pressed and act according to it.
	/// </summary>
	void lookKeys()
	{
		if (Input.GetKeyDown (KeyCode.LeftArrow)) {
			Direction = dirList.Left;
		}
		else if (Input.GetKeyDown (KeyCode.RightArrow)) {
			Direction = dirList.Right;
		}

		if (Input.GetKeyDown (KeyCode.Space))
		{
			Anims.Play("walk");
			seekNextPoint();
		}
	}

	/// <summary>
	/// Apply the forces after all input have been registered.
	/// </summary>
	void applyForces()
	{
		switch (Direction) {
		case dirList.Left : 
		{
			dirModifier = -1f;
			break;
		}
		case dirList.Right : 
		{
			dirModifier = 1f;
			currentSpeed = 1f;
			break;
		}
		}
	}
}
