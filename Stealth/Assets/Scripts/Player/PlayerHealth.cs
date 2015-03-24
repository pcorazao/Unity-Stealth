using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour {

	public float health = 100f;
	public float resetAfterDeathTime = 5f;
	public AudioClip deathClip;

	private Animator anim;
	private PlayerMovement playerMovement;
	private HashIDs hash;
	private SceneFadeInOut sceneFadeInOut;
	private LastPlayerSighting lastPlayerSighting;
	private float timer;
	private bool playerDead;

	void Awake()
	{
		anim = this.gameObject.GetComponent<Animator> ();
		playerMovement = this.gameObject.GetComponent<PlayerMovement> ();
		hash = GameObject.FindGameObjectWithTag (Tags.gameController).GetComponent<HashIDs> ();
		sceneFadeInOut = GameObject.FindGameObjectWithTag (Tags.fader).GetComponent<SceneFadeInOut> ();
		lastPlayerSighting = GameObject.FindGameObjectWithTag (Tags.gameController).GetComponent<LastPlayerSighting> ();
	}

	void Update()
	{
		if (health <= 0f) {
			if(!playerDead)
			{
				PlayerDying();
			}else{
				PlayerDead();
				LevelReset();
			}
		}
	}

	void PlayerDying()
	{
		playerDead = true;
		anim.SetBool (hash.deadBool, true);
		AudioSource.PlayClipAtPoint (deathClip, transform.position);
	}

	void PlayerDead()
	{
		var audio = this.gameObject.GetComponent<AudioSource> ();

		if (anim.GetCurrentAnimatorStateInfo (0).nameHash == hash.dyingState) {
			anim.SetBool(hash.deadBool, false);
		}

		anim.SetFloat (hash.speedFloat, 0f);
		playerMovement.enabled = false;
		lastPlayerSighting.position = lastPlayerSighting.resetPosition;
		audio.Stop ();
	}

	void LevelReset()
	{
		timer += Time.deltaTime;

		if (timer >= resetAfterDeathTime) {
			sceneFadeInOut.EndScene();
		}
	}

	public void TakeDamage(float amount)
	{
		health -= amount;
	}
}
