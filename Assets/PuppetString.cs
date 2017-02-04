using UnityEngine;
using System.Collections;

[RequireComponent(typeof (LineRenderer))]
public class PuppetString : MonoBehaviour {
	private LineRenderer lr;
	public GameObject attachedObject;
	private Rigidbody rb;
	public float speed = 4;
	public float length {
		get;
		private set;
	}
	[SerializeField]
	[Range(10000f, 100000f)]
	float springConstant = 10000f;
	private float relaxedLength;
	private Vector3 lastVelocity;

	public string controllerExtension = "L";

	void Start () {
		if (!attachedObject)
			return;
		rb = attachedObject.GetComponent<Rigidbody> ();
		lr = GetComponent<LineRenderer> ();
		relaxedLength = Vector3.SqrMagnitude (transform.position - attachedObject.transform.position);
	}

	void Update () {
		if (!attachedObject)
			return;
		lr.SetPosition (0, transform.position);
		lr.SetPosition (1, attachedObject.transform.position);
	}
	
	void FixedUpdate () {
		if (controllerExtension.Length > 0)
			transform.position += Vector3.Normalize (new Vector3 (Input.GetAxis ("Horizontal" + controllerExtension), 0f, Input.GetAxis ("Vertical" + controllerExtension))) * speed * Time.deltaTime;
		if (rb) {
			//rb.velocity = Vector3.zero;
			/*
			acceleration = (rb.velocity - lastVelocity) / Time.fixedDeltaTime;
			lastVelocity = rb.velocity;//*/
			if (Vector3.SqrMagnitude (transform.position - attachedObject.transform.position) > relaxedLength) {
				rb.AddForce ((transform.position - attachedObject.transform.position) * Time.fixedDeltaTime * springConstant, ForceMode.Force);
				lr.SetColors (Color.red, Color.red);
			} else {
				lr.SetColors (Color.green, Color.green);
			}
		}
	}
}
