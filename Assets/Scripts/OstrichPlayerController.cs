using UnityEngine;
using System.Collections;

public class OstrichPlayerController : MonoBehaviour
{
	//AudioSource files for player movements
	public AudioSource flySound;
	public AudioSource walkSound;
	
	//Keys for Player Control
	public KeyCode leftControl;
	public KeyCode rightControl;
	public KeyCode flapControl;

	[SerializeField] private int player;

    Rigidbody2D playerRB;
    public float speed = 1f;
    Animator anim;
    bool facingRight;
    public LayerMask groundLayer;
    public float jumpForce = 20f;
	public bool vulnerable = true;


    // Use this for initialization
    void Start()
    {
        facingRight = true;
        playerRB = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        anim.SetBool("Flying", false); //set to not fly at first
        anim.SetBool("Walking", false); //not walking at first

		//get all audio's for ostrich
		AudioSource[] allMyAudioSources = GetComponents<AudioSource>();
		walkSound = allMyAudioSources[0];
		flySound = allMyAudioSources [1];

    }
		

    // Update is called once per frame
	void Update(){
		//Only move player if in GameState.inGame
		if (GameManager.instance.currentGameState == GameState.inGame) {
			if (Input.GetKeyDown (flapControl)) {
				flySound.Play ();
				//play sound when spacebar is pressed
				if (anim.GetBool ("Flying")) {
					walkSound.Stop ();
				}
			} else if (Input.GetKeyDown (leftControl)) {
				if (anim.GetBool ("Walking")) {
					walkSound.Play ();
				}
			} else if (Input.GetKeyDown (rightControl)) {
				if (anim.GetBool ("Walking")) {
					walkSound.Play ();
				}
			} else if (!anim.GetBool ("Walking") && !anim.GetBool ("Flying")) {
				walkSound.Stop ();
			}
		}
	}

    void FixedUpdate()
	{
		if (GameManager.instance.currentGameState == GameState.inGame) {
			//horizontal movement
			float move = 0;
			if (Input.GetKey (leftControl)) {
				move = -1;
			}
			if (Input.GetKey (rightControl)) {
				move = 1;
			}
			playerRB.velocity = new Vector2 (move * speed, 0);

			if (IsGrounded ()) {
				anim.SetBool ("Walking", true);
				anim.SetBool ("Flying", false);
			}

			//flap movement
			if (Input.GetKeyDown (flapControl)) {
				Flap ();
			}

			//if horizontal movement is zero and not flying, go idle
			if (move == 0 && !anim.GetBool ("Flying")) {
				anim.SetBool ("Walking", false);
			}

			//determine direction player is facing 
			if (move > 0 && !facingRight) {
				Flip ();
			} else if (move < 0 && facingRight) {
				Flip ();
			}
		}

    }

    void Flip()
    {
        //flips player in the direction its facing
        facingRight = !facingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    void Flap()
    {
      //  Debug.Log("flapping");
        playerRB.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        anim.SetBool("Walking", false);
        anim.SetBool("Flying", true);
    }

    bool IsGrounded()
    {

        if (Physics2D.Raycast(this.transform.position, Vector2.down, 0.2f, groundLayer.value))
        {
            return true;
        }
        else
        {
            return false;
        }

    }

	public static void RespawnPlayer(){
		Debug.Log ("Player Respawn");
	}

	//When player gets hit in head (vulnerable spot) by enemy
	public IEnumerator Hit(){
		if (GameManager.instance.currentGameState == GameState.inGame) {
			GameManager.instance.ReduceLives (player);

			GetComponent<SpriteRenderer> ().enabled = false;
			yield return new WaitForSeconds (0.5f);
			GetComponent<SpriteRenderer> ().enabled = true;
			yield return new WaitForSeconds (0.5f);
			GetComponent<SpriteRenderer> ().enabled = false;
			yield return new WaitForSeconds (0.5f);
			GetComponent<SpriteRenderer> ().enabled = true;
			yield return new WaitForSeconds (0.5f);
			GetComponent<SpriteRenderer> ().enabled = false;
			yield return new WaitForSeconds (0.5f);
			GetComponent<SpriteRenderer> ().enabled = true;

			vulnerable = true;
		}
	}
	
	public void KilledOtherPlayer(){
		StartCoroutine(Hit());
		GameManager.instance.IncreaseScore(player, 5);
	}
	
	public void KilledEnemy(){
		GameManager.instance.IncreaseScore(player, 5);
	}
	
	public void CollectedEgg(){
		GameManager.instance.IncreaseScore(player, 2);
	}
		
}
