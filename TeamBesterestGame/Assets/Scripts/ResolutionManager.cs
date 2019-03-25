using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.IO;


public class ResolutionManager : MonoBehaviour {

    public Dropdown resolutionDropdown;
    Resolution[] resolutions;

	// Use this for initialization
	void Start ()
    {

        resolutions = Screen.resolutions;

        for (int i = 0; i < resolutions.Length; i++)
        {
            resolutionDropdown.options.Add(new Dropdown.OptionData(ResToString(resolutions[i])));

            resolutionDropdown.value = i;

            resolutionDropdown.onValueChanged.AddListener(delegate { Screen.SetResolution(resolutions[resolutionDropdown.value].width, resolutions[resolutionDropdown.value].height, Screen.fullScreen); });

        }
    }

    string ResToString(Resolution res)
    {
        return res.width + " x " + res.height;
    }
}
