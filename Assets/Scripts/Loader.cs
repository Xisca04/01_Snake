using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Loader 
{
    // Una clase STATIC tiene todas sus variables y funciones tambi�n STATIC

    // Variable que guarda una funci�n sin INPUTS ni OUTPUTS
    private static Action loaderCallbackAction; // Es un DELEGADO

    // Lista de nuestras escenas
    public enum Scene
    {
        Game,
        LoadingScene
    }

    public static void Load(Scene scene)
    {
        // Asignas el loaderCallbackAction una funci�n que no recibe par�metros y ejecuta la linea 26 (LoadScene)
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
