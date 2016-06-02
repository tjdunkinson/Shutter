using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	private CharacterController playerChar;
	private Camera cam;
	private Transform headTrans;
	private Vector3 moveDir;
	private float headTilt = 0;
	private int photoNum = 1;
	private SnapshotManager snapMan;

	[SerializeField] float walkSpeed = 0.2f;
	[SerializeField] float mouseXSens = 2f;
	[SerializeField] float mouseYSens = 2f;
	[SerializeField,Range(-90,0)] float headDownTilt;
	[SerializeField,Range(0,90)] float headUpTilt;
	[SerializeField] string pictureName;
	[SerializeField] Texture[] capturedImages;


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
		snapMan.LoadSnap (photoNum);

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
		//Texture photo = new Texture;
		//photo.name
		string photo = pictureName + 00 + photoNum+".png" as string;
		Application.CaptureScreenshot (photo);
		photoNum++;
		snapMan.LoadSnap (photoNum);

	}
}
