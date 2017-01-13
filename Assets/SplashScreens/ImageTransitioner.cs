using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;

[RequireComponent (typeof (Image))]

public class ImageTransitioner : MonoBehaviour {

	public Sprite[] Images; //Array of Images we're transitioning through
	public float StayTime; //How long should each image stay for?
	public float TransitionTime; //How long should the transitions between each image take?
	public UnityEvent DoesAfter; //Events to do after the Slideshow is over

	Image image; //Compoenent reference
	
	
	enum States { Starting, Staying, FadingOut, FadingIn, Finished };
	States state; //
	int index;
	float alpha;
	float initTime;

	// Use this for initialization
	void Start () {
		image = GetComponent<Image>(); //Get component reference at start
		StartSlideshow();
	}

	// Update is called once per frame
	void Update () {
		HandleTransitionState();
	}

	//Public call to start slideshow at first image
	public void StartSlideshow(){
		index = 0; //Reset index
		alpha = 0; //Reset alpha
		image.sprite = Images[0]; //Set image to first image
		initTime = Time.time; //Set initial time
		state = States.Starting; //We're starting now
	}

	//Public call to stop slideshow
	public void StopSlideshow(){
		index = -1;
		alpha = 0;
		initTime = -1;
		state = States.Finished;
	}

	//Called every frame
	void HandleTransitionState(){
		switch(state){
			case States.Starting:
				state = States.FadingIn; //Start fading in
			break;
			
			case States.FadingIn:
				alpha = (Time.time - initTime) / (TransitionTime); // alpha = [0, 1]
				if(alpha >= 1){ //If alpha >= 1, we're done transitioning
					initTime = Time.time; //Set initial time
					alpha = 1; //Set to fully opaque
					state = States.Staying; //Set to "staying" state
				}
				image.material.color = new Color(1, 1, 1, alpha); //Update color of image
			break;

			case States.Staying:
				float t = (Time.time - initTime); //Time from when we started staying and now
				if(t >= StayTime){ //If we're stayed for long enough
					initTime = Time.time; //Set initial time
					state = States.FadingOut; //Start fading out
				}
			break;

			case States.FadingOut:
				alpha = (Time.time - initTime) / (TransitionTime); // alpha = [0, 1]
				alpha = 1 - alpha;
				
				if(alpha <= 0){ //If alpha <= 0, we're done transitioning
					index ++; //Increment index
					if(index >= Images.Length){ //If we've run out of Images,
						DoesAfter.Invoke(); //Invoke/do what we're supposed to do after we're done the slideshow
						StopSlideshow(); //Stop the slideshow
					} else { //Otherwise, we still have Images to show
						initTime = Time.time; //Set initial time
						alpha = 0; //Set to fully opaque
						image.sprite = Images[index]; //Set the next image
						state = States.FadingIn; //Start fading in next image
					}
				}

				image.material.color = new Color(1, 1, 1, alpha); //Update color of image
			break;
			
			case States.Finished:
				//Do nothing
			break;

			default: //ERROR
				Debug.Log("ImageTransitioner State Error");
			break;
		}
	}
}
