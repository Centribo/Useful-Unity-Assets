using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent (typeof (Image))]

public class ImageTransitioner : MonoBehaviour {

	public Sprite[] images; //Array of images we're transitioning through
	public float stayTime; //How long should each image stay for?
	public float transitionTime; //How long should the transitions between each image take?

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
		image.sprite = images[0]; //Set image to first image
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
				alpha = (Time.time - initTime) / (transitionTime); // alpha = [0, 1]
				if(alpha >= 1){ //If alpha >= 1, we're done transitioning
					initTime = Time.time; //Set initial time
					alpha = 1; //Set to fully opaque
					state = States.Staying; //Set to "staying" state
				}
				image.material.color = new Color(1, 1, 1, alpha); //Update color of image
			break;

			case States.Staying:
				float t = (Time.time - initTime); //Time from when we started staying and now
				if(t >= stayTime){ //If we're stayed for long enough
					initTime = Time.time; //Set initial time
					state = States.FadingOut; //Start fading out
				}
			break;

			case States.FadingOut:
				alpha = (Time.time - initTime) / (transitionTime); // alpha = [0, 1]
				alpha = 1 - alpha;
				
				if(alpha <= 0){ //If alpha <= 0, we're done transitioning
					index ++; //Increment index
					if(index >= images.Length){ //If we've run out of images,
						StopSlideshow(); //Stop the slideshow
					} else { //Otherwise, we still have images to show
						initTime = Time.time; //Set initial time
						alpha = 0; //Set to fully opaque
						image.sprite = images[index]; //Set the next image
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
