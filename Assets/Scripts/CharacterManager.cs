using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;

public class CharacterManager : MonoBehaviour {

	public enum dirList
	{
		Left,
		Right
	};
	public enum StateList
	{
		Running,
		Walking,
		Stopped
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
	public StateList State;
	public ParticleSystem Parts;
	public bool Recovering;

	public int stopTimer;
	public int Energy;
	public int VisuSpeed;

	public CharacterManager Setup(LevelManager _man)
	{
		Anims ["walk"].wrapMode = WrapMode.Loop;
		Anims ["run"].wrapMode = WrapMode.Loop;
		Anims ["idle"].wrapMode = WrapMode.Loop;
		Manager = _man;
		Path = _man.Path;
		CurrentMarker = Path.LineList[1];
		Anims.Play("walk");
		Parts = GetComponentInChildren<ParticleSystem>();
		return this;
	}

	void Update () {

		lookKeys ();
		applyForces ();
		//Debug.Log(CurrentMarker.transform.position);
		transform.position = Vector3.MoveTowards(transform.position, CurrentMarker.transform.position, maxSpeed * Time.deltaTime);
		Vector3 dir = Vector3.RotateTowards(transform.position, CurrentMarker.transform.position - transform.position, 30f, 0f);

		Vector3 targetPoint = CurrentMarker.transform.position;
		Quaternion targetRotation = Quaternion.LookRotation(targetPoint - transform.position, Vector3.up);
		transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 1.50f); 
	}

	// Every waypoints gives a whole new direction.
	void OnTriggerEnter(Collider _coll)
	{
		if (CurrentMarker.Colli == _coll)
		{

			//transform.DOLookAt(dir, 1f, AxisConstraint.Y);
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
			switchState();
		}

		if (Input.GetKey(KeyCode.Space) && State != StateList.Stopped && Recovering == false)
		{
			stopTimer++;
			if (stopTimer == 10)
			{
				setStop();
			}
		}
		else if (Input.GetKey(KeyCode.Space) && State == StateList.Stopped && Recovering == false)
		{
			stopTimer++;
			if (stopTimer == 10)
			{
				setWalk();
			}
		}

		if (Input.GetKeyUp(KeyCode.Space))
		{
			Recovering = false;
		}

	}

	void switchState()
	{
		switch (State)
		{
		case StateList.Walking : 
		{
			Debug.Log ("Do" + StateList.Running);
			GameEventManager.TriggerRun();
		break;
		}
		case StateList.Running : 
		{
			setRun();
			break;
		}
		}
	}
	
	void setWalk()
	{
		stopTimer = 0;
		State = StateList.Walking;
		maxSpeed = 2f;
		Anims.Play("walk");
		Parts.Play();
	}
	
	void setRun()
	{
		stopTimer = 0;
		State = StateList.Running;
		maxSpeed = 4f;
		Anims.Play("run");
		Parts.Play();
	}

	void setStop()
	{
		stopTimer = 0;
		State = StateList.Stopped;
		maxSpeed = 0f;
		Anims.Play("idle");
		Parts.Stop();
		Recovering = true;
	}
			
	/// <summary>
	/// Apply the forces after all input have been registered.
	/// </summary>
	void applyForces()
	{
		switch (Direction) 
		{
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
