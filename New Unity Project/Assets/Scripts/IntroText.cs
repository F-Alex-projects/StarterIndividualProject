using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroText : MonoBehaviour {

	public AudioSource soundSource;
	public AudioClip announcement;
	// Use this for initialization
	void Start () {
		soundSource.clip = announcement;
        soundSource.Play();
        Destroy(gameObject, 2);
        }
	
	// Update is called once per frame
	void Update () {
		
	}
}