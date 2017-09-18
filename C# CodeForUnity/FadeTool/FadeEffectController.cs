using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeEffectController : MonoBehaviour {

    [Header("All Fade Group")]
    public Transform AllFadeGroup;

     List<FadeEffectTool> ListFade = new List<FadeEffectTool>();

    private void Awake()
    {
        if(AllFadeGroup == null)
        {
            AllFadeGroup = gameObject.transform;
        }

        foreach (Transform Obj in AllFadeGroup.GetComponentInChildren<Transform>())
        {
            if (Obj.GetComponent<FadeEffectTool>() != null)
            {
                ListFade.Add(Obj.GetComponent<FadeEffectTool>());
            }
        }
    }

    private void OnTriggerEnter(Collider Col)
    {
        if (Col.tag == "Player")
            { 
                StartCoroutine("FadeModeOn");
            }
            
   }

    private void OnTriggerExit(Collider Col)
    {
        if (Col.tag == "Player")
            StartCoroutine("FadeModeOff");
    }

    IEnumerator FadeModeOn()
    {
        foreach (FadeEffectTool Fade in ListFade)
        {
           if (Fade.tag != "FirstFloor")
           {
                {
                     Fade.GetComponent<FadeEffectTool>().StartCoroutine("FadeOut");
                }
           yield return true;
           }
        }
    }

    IEnumerator FadeModeOff()
    {
        foreach (FadeEffectTool Fade in ListFade)
        {
            Fade.GetComponent<FadeEffectTool>().StartCoroutine("FadeIn");

            yield return true;
        }
        
    }
}