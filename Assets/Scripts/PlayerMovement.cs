using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour {

	public float speed = 2;
	public float gravity = -20;
	public float jumpHeight = 1;
	GameObject LoosePanel;
	float currentSpeed;
	float velocityY;
	static Animator anim;
	CharacterController controller;
	public GameObject LosePanel;
	public GameObject InGamePanel;

	void Start () {
		controller = GetComponent<CharacterController> ();
		anim = GetComponent<Animator> ();
	}

	void Update () {
		Vector2 input=new Vector2(0,1);
		if (controller.isGrounded) {
			input.x = Input.GetAxisRaw("Horizontal");
		}
        Move (input);
		if (Input.GetKeyDown (KeyCode.Space)) {
			Jump ();
		}

	}
	void Move(Vector2 inputDir) {
        velocityY += Time.deltaTime * gravity;
		Vector3 velocity = Vector3.zero;
        velocity.x = inputDir.x *10.0f;
        //print(velocity.x);
        velocity.y = velocityY;
        velocity.z = speed;
        controller.Move (velocity * Time.deltaTime);
		currentSpeed = new Vector2 (controller.velocity.x, controller.velocity.z).magnitude;
	
		if (controller.isGrounded) {
			velocityY = 0;
			anim.SetBool ("isDown",true);
		}

	}

	void Jump() {
		if (controller.isGrounded && controller.velocity.z!=0) {
			float jumpVelocity = Mathf.Sqrt (-2 * gravity * jumpHeight);
			velocityY = jumpVelocity;
			anim.SetTrigger("isJumping");
			anim.SetBool ("isDown",false);
		}
	}

	void OnControllerColliderHit(ControllerColliderHit hit){
        
		if(hit.gameObject.tag=="Enemy")
		{
			LosePanel.SetActive(true);
			InGamePanel.SetActive(false);
			print(hit.gameObject.tag);
            gameObject.GetComponent<PlayerMovement>().enabled=false;
			anim.SetTrigger("Collision");
		}
	}
}