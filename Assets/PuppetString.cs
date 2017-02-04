using UnityEngine;
using System.Collections;

[RequireComponent(typeof (LineRenderer))]
public class PuppetString : MonoBehaviour {
	private LineRenderer lr;
	public GameObject attachedObject;
	private Rigidbody rb;
	public float speed = 4;
	public ForceMode mode;
	public float length {
		get;
		private set;
	}
	[SerializeField]
	[Range(10000f, 100000f)]
	float factor = 10000f;
	private float relaxedLength;

	void Start () {
		rb = attachedObject.GetComponent<Rigidbody> ();
		lr = GetComponent<LineRenderer> ();
		relaxedLength = Vector3.SqrMagnitude (transform.position - attachedObject.transform.position);
	}

	void Update () {
		lr.SetPosition (0, transform.position);
		lr.SetPosition (1, attachedObject.transform.position);
		if (Vector3.SqrMagnitude (transform.position - attachedObject.transform.position) > relaxedLength) {
			lr.SetColors (Color.red, Color.red);
		} else {
			lr.SetColors (Color.green, Color.green);
		}
	}
	
	void FixedUpdate () {	
		transform.position += new Vector3 (Input.GetAxis ("Horizontal") * speed * Time.deltaTime, Input.GetAxis ("Vertical") * speed * Time.deltaTime, 0f);
		if (rb) {
			//rb.velocity = Vector3.zero;
			rb.AddForce ((transform.position - attachedObject.transform.position) * Time.fixedDeltaTime * factor, mode);
		}
	}
}
