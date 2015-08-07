using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public static class GameExtensions
{
	public static void addMany<T>(this List<T> list, params T[] elements)
	{
		list.AddRange(elements);
	}
	public static T findChild<T>(this GameObject _obj, string _name)
	{
		return(_obj.GetComponentsInChildren<MonoBehaviour>().ToList().Find((MonoBehaviour obj) => obj.name == _name).GetComponent<T>());
	}
	public static T findUIChild<T>(this MonoBehaviour _obj, string _name)
	{
		return(_obj.GetComponentsInChildren<RectTransform>().ToList().Find((RectTransform obj) => obj.name == _name).GetComponent<T>());
	}
	public static void InString<T>(this List<T> list, params T[] elements)
	{
//		list.ForEach((T obj) => Debug.Log(obj.ToString()));
	}


}
