using UnityEngine;
using System.Collections;

public class FormationController : MonoBehaviour {
	public GameObject enemyPrefab;
	public float width = 10;
	public float height = 5;
	public float speed;
	public float padding;
	public float spawnDelay = 1f;
	private float xmin;
	private float xmax;
	
	private bool movingRight = true;
	
	
	// Use this for initialization
	void Start () {
			
		// calculate the boundaries from teh camera
		float distance = transform.position.z - Camera.main.transform.position.z;
		Vector3 leftMost = Camera.main.ViewportToWorldPoint(new Vector3( 0,0,distance));
		Vector3 rightMost = Camera.main.ViewportToWorldPoint(new Vector3( 1,0,distance));
		xmin = leftMost.x + padding;
		xmax = rightMost.x - padding;
		
		SpawnUntilFull();
		
	}
		
	
	
	// Update is called once per frame
	void Update () {
			
		// move the formation
		if (movingRight) { 
			transform.position += Vector3.right * speed * Time.deltaTime;
		} else {
			transform.position += Vector3.left * speed * Time.deltaTime;
		}
		
		// flip the direction if we're outside the boundaries.
		if (transform.position.x < xmin || transform.position.x > xmax){
			movingRight = !movingRight;
		}
		if (AllMembersAreDead()){
			SpawnUntilFull();
		}
		
	}
		

	void SpawnUntilFull() {
		Transform freePosition =  NextFreePosition();
		if (freePosition){
			GameObject enemy = Instantiate(enemyPrefab, freePosition.position, Quaternion.identity) as GameObject;
			enemy.transform.parent = freePosition;
		}
		if (NextFreePosition()){
			Invoke("SpawnUntilFull", spawnDelay);
		}	
	}
	
	
	Transform  NextFreePosition() {
		foreach(Transform childPositionGameObject in transform) {
			if (childPositionGameObject.childCount == 0) {
				return childPositionGameObject;
			}
		}
		return null;
	}
	
	bool AllMembersAreDead() {
		foreach(Transform childPostionGameObject in transform) {
	 		if (childPostionGameObject.childCount >0) {
	 			return false;
	 		}
	 	}
	 return true;
	}
	
	//Draws wire frame.
	void OnDrawGizmos(){
		float gizmoWidth, gizmoHeight;
		
		gizmoWidth = transform.position.x + 0.5f * width;
		gizmoHeight = transform.position.y + 0.5f * height;
		Gizmos.DrawWireCube(new Vector3(0,5,0), new Vector3(gizmoWidth, gizmoHeight,0 ));
	}
}
