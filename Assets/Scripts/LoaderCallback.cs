using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoaderCallback : MonoBehaviour
{
    private bool firstUpdate = true;

    private void Update()  // Poner contador o corrutina si queremos que dure más la escena de carga
    {
        if (firstUpdate)
        {
            firstUpdate = false;
            Loader.LoaderCallBack();
        }
    }
}
