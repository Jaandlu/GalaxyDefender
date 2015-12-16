using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ShipController : MonoBehaviour {
	public GameObject projectile;
	public float speed;
	public float padding;
	public float projectileSpeed;
	public float fireRate = 0.2f;
	public float health = 3000;
	public float xmin;
	public float xmax;
	public AudioClip lazerFire;
	public AudioClip explosion;
	public GameObject Smoke;
	public GameObject fireFlare;
	public Slider healthSlider;
	
	
	// Use this for initialization
	void Start () {
	
		
		float distance = transform.position.z - Camera.main.transform.position.z;
		Vector3 leftMost = Camera.main.ViewportToWorldPoint(new Vector3( 0,0,distance));
		Vector3 rightMost = Camera.main.ViewportToWorldPoint(new Vector3( 1,0,distance));
		
		
	xmin = leftMost.x + padding;
	xmax = rightMost.x - padding;
		
	}
	
	// Update is called once per frame
	void Update () {
		
		if (Input.GetKey(KeyCode.LeftArrow)) {
			transform.position += Vector3.left * speed * Time.deltaTime;
		} 
		if 
			(Input.GetKey(KeyCode.RightArrow)) {
			transform.position += Vector3.right * speed * Time.deltaTime;
			}
			
		if (Input.GetKeyUp(KeyCode.Space)) {
			CancelInvoke("Fire");
		}		
		if (Input.GetKeyDown(KeyCode.Space)) {
			InvokeRepeating ("Fire", 0.0001f, fireRate);  	
		}
		
		float newX = Mathf.Clamp (transform.position.x, xmin, xmax);
		transform.position = new Vector3(newX, transform.position.y, transform.position.z);
		
		
	}
	
	void OnTriggerEnter2D (Collider2D collider) {
		
		Projectile missile = collider.gameObject.GetComponent<Projectile>();
		if (missile) {
			health -= missile.GetDamage();
			healthSlider.value = health;
			missile.Hit();
		
			
			if (health <= 0) {
				Die();
			}
		}
	}	
	
	void Die(){
		LevelManager man = GameObject.Find("LevelManager").GetComponent<LevelManager>();
		man.LoadLevel("Win");
		PuffSmoke();
		FireFlare();
		AudioSource.PlayClipAtPoint (explosion, transform.position);
		Destroy(gameObject);
	
	}
	
	void Fire(){
		
		GameObject missile = Instantiate(projectile, transform.position, Quaternion.identity) as GameObject;
		missile.rigidbody2D.velocity = new Vector3 (0, projectileSpeed,0);
		AudioSource.PlayClipAtPoint (lazerFire, transform.position);
	}
	void PuffSmoke() {
		Instantiate (Smoke, transform.position, Quaternion.identity);
	}
	
	void FireFlare() {
		Instantiate (fireFlare, transform.position, Quaternion.identity);
	}
	
	
}



