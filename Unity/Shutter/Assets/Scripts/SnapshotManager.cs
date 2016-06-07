using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SnapshotManager : MonoBehaviour 
{

	[SerializeField] public Snapshot[] snapShots;
	//private int snapNum;


	void Awake ()
	{
		snapShots = GetComponentsInChildren<Snapshot> ();
	}
		
	/*public void LoadSnap (int snapNum) 
	{
		print (snapNum);
		snapShots [snapNum].ActivateSnap ();
		if (snapNum > 0) 
		{
			snapShots [snapNum-1].DeactivateSnap();
		}

	}*/
	
}
