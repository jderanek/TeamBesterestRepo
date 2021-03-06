﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Text;
using System.Collections.Generic;
using Yarn.Unity;
using UnityEngine.EventSystems;

namespace Yarn.Unity
{
    /// Displays dialogue lines to the player, and sends
    /// user choices back to the dialogue system.

    /** Note that this is just one way of presenting the
     * dialogue to the user. The only hard requirement
     * is that you provide the RunLine, RunOptions, RunCommand
     * and DialogueComplete coroutines; what they do is up to you.
     */
    public class InterviewUI : Yarn.Unity.DialogueUIBehaviour
    {

        /// The object that contains the dialogue and the options.
        /** This object will be enabled when conversation starts, and 
         * disabled when it ends.
         */
        public GameObject dialogueContainer;

        /// The UI element that displays lines
        public Text responseText;

        /// A UI element that appears after lines have finished appearing
        public GameObject continuePrompt;

        /// A delegate (ie a function-stored-in-a-variable) that
        /// we call to tell the dialogue system about what option
        /// the user selected
        private Yarn.OptionChooser SetSelectedOption;

        /// How quickly to show the text, in seconds per character
        [Tooltip("How quickly to show the text, in seconds per character")]
        public float textSpeed = 0.025f;

        private float textSpeedConstant; 

        /// The buttons that let the user choose an option
        public List<Button> optionButtons;

        /// Make it possible to temporarily disable the controls when
        /// dialogue is active and to restore them when dialogue ends
        public RectTransform gameControlsContainer;
        private float timer;

        public static bool voicePlaying = false;

        public EventSystem eventSystem;
    

        private void Awake()
        {
            // Start by hiding the container, line and option buttons
            if (dialogueContainer != null)
                dialogueContainer.SetActive(false);

            responseText.gameObject.SetActive(false);

            foreach (var button in optionButtons)
            {
                button.gameObject.SetActive(false);
            }

            // Hide the continue prompt if it exists
            if (continuePrompt != null)
                continuePrompt.SetActive(false);

            eventSystem = FindObjectOfType<EventSystem>();
        }

        private void Update()
        {
            timer += Time.deltaTime;
        }


        /// Show a line of dialogue, gradually
        public override IEnumerator RunLine(Yarn.Line line)
        {
            while (GameManager.paused)
            {
                AkSoundEngine.PostEvent("Stop_Voices", gameObject);
                yield return null;
            }
            // Show the text
            responseText.gameObject.SetActive(true);
            yield return new WaitForSeconds(.1f);
            if (textSpeed > 0.0f)
            {
                //Calls the voice activation if it isn't playing
                if (voicePlaying == false)
                {
                    SoundManager.SetSoundBank();
                    voicePlaying = true;
                }
                // Display the line one character at a time
                var stringBuilder = new StringBuilder();

                foreach (char c in line.text)
                {
                    stringBuilder.Append(c);
                    responseText.text = stringBuilder.ToString();
                    
                    
                    if ((Input.GetMouseButtonDown(0) || Input.GetKeyDown("j")) && !GameManager.paused && UIManager.eligibleForClick)
                    {
                        responseText.text = line.text;
                        yield return new WaitForSeconds(.025f);
                        break;
                    }
                    timer = 0f;
                    yield return new WaitUntil(() => (((Input.GetMouseButtonDown(0) || Input.GetKeyDown("j")) && UIManager.eligibleForClick) || (timer >= textSpeed)) && !GameManager.paused); //
                }
            }
            else
            {
                //Display the line immediately if textSpeed == 0
                responseText.text = line.text;
            }

            //sound voice to stop/ make voice check work
            SoundManager.StopDialogue();
            voicePlaying = false;

            // Show the 'press any key' prompt when done, if we have one
            if (continuePrompt != null)
                continuePrompt.SetActive(true);

            // Wait for any user input
            yield return new WaitUntil(() => (Input.anyKeyDown && !GameManager.paused && UIManager.eligibleForClick));

            // Hide the text and prompt
            //responseText.gameObject.SetActive(false); //Commented out so that player can see the dialogue while choosing answer

            if (continuePrompt != null)
                continuePrompt.SetActive(false);

        }

        /// Show a list of options, and wait for the player to make a selection.
        public override IEnumerator RunOptions(Yarn.Options optionsCollection,
                                                Yarn.OptionChooser optionChooser)
        {
            // Do a little bit of safety checking
            if (optionsCollection.options.Count > optionButtons.Count)
            {
                Debug.LogWarning("There are more options to present than there are" +
                                 "buttons to present them in. This will cause problems.");
            }

            // Display each option in a button, and make it visible
            int i = 0;
            foreach (var optionString in optionsCollection.options)
            {
                optionButtons[i].gameObject.SetActive(true);
                optionButtons[i].GetComponentInChildren<Text>().text = optionString;
                i++;
            }

            // Record that we're using it
            SetSelectedOption = optionChooser;

            // Wait until the chooser has been used and then removed (see SetOption below)
            while (SetSelectedOption != null)
            {
                yield return null;
            }

            // Hide all the buttons
            foreach (var button in optionButtons)
            {
                button.gameObject.SetActive(false);
            }
        }

        /// Called by buttons to make a selection.
        public void SetOption(int selectedOption)
        {

            // Call the delegate to tell the dialogue system that we've
            // selected an option.
            SetSelectedOption(selectedOption);

            // Now remove the delegate so that the loop in RunOptions will exit
            SetSelectedOption = null;
        }

        /// Run an internal command.
        public override IEnumerator RunCommand(Yarn.Command command)
        {
            // "Perform" the command
            Debug.Log("Command: " + command.text);

            yield break;
        }

        /// Called when the dialogue system has started running.
        public override IEnumerator DialogueStarted()
        {
            Debug.Log("Dialogue starting!");

            // Enable the dialogue controls.
            if (dialogueContainer != null)
                dialogueContainer.SetActive(true);

            // Hide the game controls.
            if (gameControlsContainer != null)
            {
                gameControlsContainer.gameObject.SetActive(false);
            }

            yield break;
        }

        /// Called when the dialogue system has finished running.
        public override IEnumerator DialogueComplete()
        {
            Debug.Log("Complete!");

            // Hide the dialogue interface.
            if (dialogueContainer != null)
                dialogueContainer.SetActive(false);

            // Show the game controls.
            if (gameControlsContainer != null)
            {
                gameControlsContainer.gameObject.SetActive(true);
            }

            yield break;
        }

    }

}
