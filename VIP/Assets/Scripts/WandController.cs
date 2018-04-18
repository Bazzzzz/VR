using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WandController : MonoBehaviour
{
	private SteamVR_TrackedController controller;
	//public GameObject pickup;
	HashSet<InteractableItem> objectsHoveringOver = new HashSet<InteractableItem> ();
	
	private InteractableItem closestItem;
	private InteractableItem interactingItem;
	
	public Camera camera;
	private bool throwing;

	// Use this for initialization
	void Start ()
	{
		controller = GetComponent<SteamVR_TrackedController> ();
		controller.TriggerClicked += HandleTriggerClicked;
		controller.Gripped += HandleGripClicked;
		controller.Ungripped += HandleGripUnclicked;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (controller == null) {
			Debug.Log ("Controller not initialized.");
			return;
		}
//		if (pickup == null) {
//			Debug.Log ("pickup null");
//		}
	}
	//	void FixedUpdate() {
	//		if (throwing) {
	//			Transform origin;
	//			if (pickup.origin != null) {
	//				origin = pickup.origin;
	//			} else {
	//				origin = pickup.transform.parent;
	//			}
	//
	//			if (origin != null) {
	//				pickup.GetComponent<Rigidbody> ().velocity = origin.TransformVector (controller.velocity);
	//				pickup.GetComponent<Rigidbody> ().angularVelocity = origin.TransformVector (controller.angularVelocity * 0.25f);
	//
	//			} else {
	//				pickup.GetComponent<Rigidbody> ().velocity = (controller.velocity);
	//				pickup.GetComponent<Rigidbody> ().angularVelocity =  (controller.angularVelocity * 0.25f);
	//
	//			}
	//			pickup.GetComponent<Rigidbody> ().maxAngularVelocity = pickup.GetComponent<Rigidbody> ().angularVelocity.magnitude;
	//
	//			throwing = false;
	//		}
	//	}
	private void HandleTriggerClicked (object sender, ClickedEventArgs e)
	{
		Debug.Log ("Trigger clicked.");
		RaycastHit hit;
		Ray ray = camera.ScreenPointToRay (Input.mousePosition);
		if (Physics.Raycast (ray, out hit)) {
			print ("Hit: " + hit.transform.gameObject.tag);
			Transform objectHit = hit.transform;
			if (objectHit.gameObject.tag.Equals ("SpikedBall")) {
//				pickup = objectHit.gameObject;
//				pickup.GetComponent<Renderer> ().material.shader = Shader.Find ("Self-Illumin/Outlined Diffuse");
//				pickup.GetComponent<Rigidbody> ().useGravity = false;
//				pickup.GetComponent<Rigidbody> ().isKinematic = false;
				InteractableItem collidedItem = objectHit.gameObject.GetComponent<Collider>().GetComponent<InteractableItem> ();
				if (collidedItem) {
					objectsHoveringOver.Add (collidedItem);
				}
				Debug.Log ("Count:" + objectsHoveringOver.Count);
			}
		}
	}

	public void OnCollisionEnter (Collision other)
	{
		print ("OnCollisionEnter: " + other.gameObject.name);
//		if ("TriggerBall".Equals (other.gameObject.name)) {
//			pickup = other.gameObject;
//		}
//		if ("Goblin".Equals (other.gameObject.tag)) {
//			GoblinController gc = other.gameObject.GetComponent<GoblinController> ();
//			gc.PlayIsDying ();
//		}
	}

	public void OnCollisionExit (Collision other)
	{
//		pickup = null;
	}

	public void OnTriggerEnter (Collider other)
	{
		print ("OnTriggerEnter: " + other.gameObject.name);
		if ("TriggerBall".Equals (other.gameObject.name)) {
			InteractableItem collidedItem = other.GetComponent<Collider>().GetComponent<InteractableItem> ();
			if (collidedItem) {
				objectsHoveringOver.Add (collidedItem);
			}
			Debug.Log ("Count:" + objectsHoveringOver.Count);
		}
		if ("Goblin".Equals (other.gameObject.tag)) {
			GoblinController gc = other.gameObject.GetComponent<GoblinController> ();
			gc.PlayIsDying ();
		}
	}

	public void OnTriggerExit (Collider other)
	{
		InteractableItem collidedItem = other.GetComponent<Collider>().GetComponent<InteractableItem> ();
		if (collidedItem) {
			objectsHoveringOver.Remove (collidedItem);
		}
		Debug.Log ("Count:" + objectsHoveringOver.Count);

	}

	private void HandleGripClicked (object sender, ClickedEventArgs e)
	{
		Debug.Log (" Grip button down.");
		
		float minDistance = float.MaxValue;

		float distance;
		foreach (InteractableItem item in objectsHoveringOver) {
			distance = (item.transform.position - transform.position).sqrMagnitude;

			if (distance < minDistance) {                   
				minDistance = distance;
				closestItem = item;
			}
		}

		interactingItem = closestItem;
		if (interactingItem) {
			if (interactingItem.IsInteracting ()) {
				interactingItem.EndInteraction (this);
			}
			interactingItem.BeginInteraction (this);
		}
		
/*         RaycastHit hit;
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            print("Hit: " + hit.transform.gameObject.tag);
            Transform objectHit = hit.transform;
            if (objectHit.gameObject.tag.Equals("TriggerBall"))
            {
                pickup = objectHit.gameObject;
                pickup.GetComponent<Renderer>().material.shader = Shader.Find("Self-Illumin/Outlined Diffuse");
				pickup.GetComponent<Rigidbody> ().useGravity = false;
				pickup.GetComponent<Rigidbody> ().isKinematic = true;
            }
        }
        if (pickup != null)
        {
			Debug.Log ("Pickup not null");

        	pickup.transform.parent = this.transform;
            //pickup.GetComponent<Rigidbody>().useGravity = true;    
        } */
	}

	private void HandleGripUnclicked (object sender, ClickedEventArgs e)
	{
		Debug.Log ("Grip button up");
		
		if (interactingItem != null) {
			interactingItem.EndInteraction (this);
		}
		
/* 		if (pickup != null) {
			Debug.Log ("Grip button up | " + pickup.tag);
			pickup.GetComponent<Renderer> ().material.shader = null;
			pickup.GetComponent<Rigidbody> ().useGravity = true;
			pickup.GetComponent<Rigidbody> ().isKinematic = false;

			pickup = null;
		} */
	}
}
