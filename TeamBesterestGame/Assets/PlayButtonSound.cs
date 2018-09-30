using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayButtonSound : MonoBehaviour {

	// This Calls the button press event directly
	public void OnClick()
    {
        AkSoundEngine.PostEvent("Button_Press", gameObject);
	}

}
