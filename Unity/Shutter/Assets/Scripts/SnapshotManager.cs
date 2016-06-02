using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SnapshotManager : MonoBehaviour {


	public void LoadSnap (int snapNum) 
	{
		SceneManager.LoadScene ("Snapshot0" + snapNum, LoadSceneMode.Additive);
	}
	
}
