using UnityEngine;
using System.Collections;

public class Snapshot : MonoBehaviour {

	[SerializeField] bool activeSnap = false;

	public Transform[] childObject;

	// Use this for initialization
	void Start () 
	{
		childObject = GetComponentsInChildren<Transform>();
		foreach (Transform objects in childObject)
		{
			if (objects == this.transform) 
			{
				continue;
			} 
			else 
			{
				objects.gameObject.SetActive (false);
			}
		}

	}
	
	// Update is called once per frame
	public void ActivateSnap () 
	{
		foreach (Transform objects in childObject) 
		{
			objects.gameObject.SetActive (true);
		}
	}

	public void DeactivateSnap () 
	{
		foreach (Transform objects in childObject)
		{
			if (objects.gameObject == this.gameObject) 
			{
				continue;
			} 
			else 
			{
				objects.gameObject.SetActive (false);
			}
		}

	}
}
