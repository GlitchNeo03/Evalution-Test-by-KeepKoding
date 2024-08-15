

// This single MonoScript does all the work


using System;
using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Defining Variables to be used
    public Animator _TransitionAnimator, _PopUpAnimator;                  // Using Animators for Smooth Transitions, Transition one handles the panel that loads in and out,
                                                                          // Pop Up One handles the Pop up message one
    public bool _isPopUpOpen = false;                                     // Using bool to check if pop is in open or closed state
    public GameObject _PopUpElements;                                     // Using a gameobject to enable and disable it while pop up appears as it prevents the below message button to be clicked

    private void Awake()                                                  // Awake ensures that the transition plays properly in the start
    {
        _TransitionAnimator.Play("Load Out");                             // Animator Plays the transition animation
    }
    
    public void Load(int levelindex)                                      // public function to load level ad using an integer to pass the level index, this can be changed in each scene
    {
        StartCoroutine(LevelLoader(levelindex));                          // Starting the coroutine with the index value passed along
    }
    IEnumerator LevelLoader(int index)                                    // This coroutine is used to ensure it plays the transition before next scene is loaded and after 0.5s it loads the other scene
    {
        _TransitionAnimator.Play("Load In");
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(index);
    }

    public void PopUp()                                                   // Using another function to kick start a coroutine
    {
        StartCoroutine(PopUpHandler());
    }

    IEnumerator PopUpHandler()                                            // This coroutine checks if the pop up is open or closed, its made this way so same code can be used to open and close pop on 2 buttons
    {
        if (!_isPopUpOpen)
        {
            _PopUpElements.SetActive(true);
            _PopUpAnimator.Play("Pop In");
            _isPopUpOpen = true;
        }
        else
        {
            _PopUpAnimator.Play("Pop Out");
            _isPopUpOpen = false;
            yield return new WaitForSeconds(0.5f);
            _PopUpElements.SetActive(false);
        }
    }

    public void Quit()                                                    // Application quits but using coroutine to play loading one last time to ensure smooth exit
    {
        StartCoroutine(AppQuit());
    }

    IEnumerator AppQuit()
    {
        _TransitionAnimator.Play("Load In");
        yield return new WaitForSeconds(0.5f);
        Application.Quit();
    }
}
