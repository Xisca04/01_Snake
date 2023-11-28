using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Loader 
{
    // Una clase STATIC tiene todas sus variables y funciones también STATIC

    // Variable que guarda una función sin INPUTS ni OUTPUTS
    private static Action loaderCallbackAction; // Es un DELEGADO

    // Lista de nuestras escenas
    public enum Scene
    {
        Game,
        LoadingScene,
        MainMenu,
        TimerLevel
    }

    public static void Load(Scene scene)
    {
        // Asignas el loaderCallbackAction una función que no recibe parámetros y ejecuta la linea 26 (LoadScene)
        loaderCallbackAction = () =>
        {
            SceneManager.LoadScene(scene.ToString());
        };

        // Llamamos a la escena de carga
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
