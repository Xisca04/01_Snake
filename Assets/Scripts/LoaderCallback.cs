using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoaderCallback : MonoBehaviour
{
    private bool firstUpdate = true;
    private float sliderTimeLeft = 1.85f; // Time it takes for the slider to complete

    private void Update()
    {
        if (firstUpdate)
        {
            firstUpdate = false;
            StartCoroutine("LoadChargerSlider");
        }
    }

    // Couroutine that visually lengthens the loading scene 

    private IEnumerator LoadChargerSlider()
    {
        yield return new WaitForSeconds(sliderTimeLeft);
        Loader.LoaderCallBack();
    }
}
