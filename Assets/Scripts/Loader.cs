using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Loader 
{
    // STATIC Class has its functions and variables STATICS

    // Variable which saves a function with no outputs or inputs
    private static Action loaderCallbackAction; // DELEGATE

    // Lists of game's scenes
    public enum Scene
    {
        Game,
        LoadingScene,
        MainMenu,
        TimerLevel
    }

    public static void Load(Scene scene)
    {
        // Assign the loaderCallbackAction to a function that takes no parameters and execute line 26 (LoadScene)
        loaderCallbackAction = () =>
        {
            SceneManager.LoadScene(scene.ToString());
        };

        // Call of the load scene
        SceneManager.LoadScene(Scene.LoadingScene.ToString());
    }

    public static void LoaderCallBack()
    {
        if(loaderCallbackAction != null)
        {
            loaderCallbackAction();
            loaderCallbackAction = null;
        }
    }
}
