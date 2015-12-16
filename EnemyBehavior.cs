using UnityEngine;
using System.Collections;

public class EnemyBehavior : MonoBehaviour {
	public float health;
	public GameObject projectile;
	public float projectileSpeed;
	public float fireRate;
	public int scoreValue = 150;
	public AudioClip explosion;
	public AudioClip EnemyLazer;
	public GameObject Smoke;
	public GameObject fireFlare;
	
	private ScoreKeeper scoreKeeper; 
	
 	// Use this for initialization
	void Start () {
		scoreKeeper = GameObject.Find("Score").GetComponent<ScoreKeeper>();
	}
	
	// Update is called once per frame
	void Update () {
		float fireTime = fireRate * Time.deltaTime;
		if(Random.value < fireTime) {
			Fire ();
			
		}
	
	}

	void Fire(){
		Vector3 lazerOffset = transform.position + new Vector3(0, -.5f, 0);
		GameObject missile = Instantiate(projectile, lazerOffset, Quaternion.identity) as GameObject;
		missile.rigidbody2D.velocity = new Vector2 (0, -projectileSpeed);
		AudioSource.PlayClipAtPoint(EnemyLazer, transform.position);
	}
	
	void OnTriggerEnter2D (Collider2D collider) {
		Projectile missile = collider.gameObject.GetComponent<Projectile>();	
		if (missile) {
			health -= missile.GetDamage();
			missile.Hit();
			if (health <= 0) { 
				Die();	
			}
		}		
	}
	void Die(){
		PuffSmoke();
		FireFlare();
		AudioSource.PlayClipAtPoint (explosion, transform.position);
		scoreKeeper.Score(scoreValue);
		Destroy(gameObject);
	}
	
	void PuffSmoke() {
		var SmokeClone = Instantiate (Smoke, transform.position, Quaternion.identity);		
		Destroy (SmokeClone, 2f);
	}
	void FireFlare() { 
		var fireFlareClone = Instantiate (fireFlare, transform.position, Quaternion.identity);		
		Destroy (fireFlareClone, 2f);
	}
}
