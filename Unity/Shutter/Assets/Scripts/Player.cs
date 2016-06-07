using UnityEngine;
using System.Collections;
using System.IO;

public class Player : MonoBehaviour {

	private CharacterController playerChar;
	private Camera cam;
	private Transform headTrans;
	private Vector3 moveDir;
	private float headTilt = 0;
	private int photoNum = 0;
	private SnapshotManager snapMan;


	[SerializeField] float walkSpeed = 0.2f;
	[SerializeField] float mouseXSens = 2f;
	[SerializeField] float mouseYSens = 2f;
	[SerializeField,Range(-90,0)] float headDownTilt;
	[SerializeField,Range(0,90)] float headUpTilt;
	[SerializeField] string pictureName;
	[SerializeField] Texture2D[] capturedImages;


	void Start () 
	{
		#if UNITY_STANDALONE
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
		#endif
		#if UNITY_EDITOR
		Cursor.lockState = CursorLockMode.Confined;
		Cursor.visible = true;
		#endif

		playerChar = GetComponent<CharacterController>();
		cam = Camera.main;
		headTrans = cam.transform.parent.GetComponentInParent<Transform> ();
		snapMan = FindObjectOfType<SnapshotManager> ();
		//snapMan.LoadSnap (photoNum);
		StartCoroutine(NextSnap());

	}

	void Update () 
	{
	
		if (Input.GetButton("Horizontal"))
			moveDir.x = (Input.GetAxis("Horizontal"));

		if (Input.GetButton("Vertical"))
			moveDir.z = (Input.GetAxis("Vertical"));

		if (Input.GetAxis ("Mouse X") > 0 || Input.GetAxis ("Mouse X") < 0 ) 
		{
			float playerXRot = Input.GetAxis ("Mouse X") * mouseXSens;
			transform.Rotate (Vector3.up, playerXRot);
		}


		if (Input.GetAxis ("Mouse Y") > 0 || Input.GetAxis ("Mouse Y") < 0 ) 
		{
			float playerYRot = Input.GetAxis ("Mouse Y") * mouseYSens;
			headTrans.Rotate (Vector3.left, playerYRot);

		}

		if (Input.GetButtonDown ("Fire1")) 
		{
			TakePhoto ();
		}


	}

	void FixedUpdate ()
	{
		moveDir = transform.TransformDirection (moveDir);
		moveDir *= walkSpeed;
		playerChar.Move (moveDir);

	}

	void TakePhoto ()
	{
		photoNum++;
		//Texture photo = new Texture;
		//photo.name
		string photo = Application.persistentDataPath+ "//" + pictureName + 00 + photoNum + ".png" as string;
		Application.CaptureScreenshot (photo);


		/*int width = Screen.width;
		int height = Screen.height;

			Texture2D tex = new Texture2D (width, height, TextureFormat.RGB24, false);

			tex.ReadPixels (new Rect (0, 0, width, height), 0, 0);
			tex.Apply ();
			
			byte[] bytes = tex.EncodeToPNG ();
			Destroy (tex);

		File.WriteAllBytes (Application.persistentDataPath+ "//" + pictureName + 00 + photoNum + ".png", bytes);*/

		//TODO: Display picture on screen before showing live scene again
		//		Add Shutter effect
		StartCoroutine(NextSnap());


	}

	IEnumerator NextSnap ()
	{
		for (float t = 0.2f; t >= 0f; t -= 0.1f) 
		{
			if (t == 0f) 
			{
				snapMan.snapShots [photoNum].ActivateSnap ();
				if (photoNum > 0) {
					snapMan.snapShots [photoNum - 1].DeactivateSnap ();
				}
			}

				yield return new WaitForSeconds (0.2f);
		}
	}
		
		
}
