using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoaderCallback : MonoBehaviour
{
    private bool firstUpdate = true;
    private float sliderTimeLeft = 1.85f;

    private void Update()  // Poner contador o corrutina si queremos que dure más la escena de carga
    {
        if (firstUpdate)
        {
            firstUpdate = false;
            StartCoroutine("LoadChargerSlider");
        }
    }

    // corruitina más tiempo visual escena de carga

    private IEnumerator LoadChargerSlider()
    {
        yield return new WaitForSeconds(sliderTimeLeft);
        Loader.LoaderCallBack();
    }
}
