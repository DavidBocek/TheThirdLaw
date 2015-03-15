using UnityEngine;
using System.Collections;

public class PlayerManager : MonoBehaviour {

	//identifier
	public int playerNumber = 0;

	// input abstraction
	public bool Fire1 { get { return Input.GetButtonDown("Player"+playerNumber+"-Fire1");}}
	public float HorizontalAxis { get { return Input.GetAxis("Player"+playerNumber+"-Horizontal");}}
	public float VerticalAxis { get { return Input.GetAxis("Player"+playerNumber+"-Vertical");}}

	//reference abstraction
	public GameObject PlayerObj {get; set;}
	public Rigidbody2D rb { get; set;}


	// Use this for initialization
	void Start () {
		PlayerObj = GameObject.Find("Player"+playerNumber);
		rb = PlayerObj.GetComponent<Rigidbody2D>();
	}
}
